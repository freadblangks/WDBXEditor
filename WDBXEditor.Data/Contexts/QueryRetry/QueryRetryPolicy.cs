using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using WDBXEditor.Data.Exceptions;
using WDBXEditor.Data.Extensions;

namespace WDBXEditor.Data.Contexts.QueryRetry
{
	/// <summary>
	/// Utility class for wrapping a SQL query with several retry behaviors.
	/// Each retry behavior is executed in the order it was applied.
	/// </summary>
	/// <typeparam name="T">The return type of the query being run.</typeparam>
	public class QueryRetryPolicy<T>
	{
		private const string _MYSQL_QUERY_HINT_PATTERN = "(?m)(/\\*\\+[\\s\\S]*?\\*/)";

		private Func<T> _queryLogic;

		private readonly QueryRetryConfiguration _retryConfig;
		private readonly Func<double> _random;
		private QueryPolicyTypes _types;

		/// <summary>
		/// Initializes a new instance of <see cref="QueryRetryPolicy"/> using default values for retry count and wait duration.
		/// </summary>
		/// <param name="queryLogic">The query to wrap with retry behavior.</param>
		public QueryRetryPolicy(Func<T> queryLogic) : this(queryLogic, new QueryRetryConfiguration(), GetRandomnessSource())
		{

		}

		/// <summary>
		/// Initializes a new instance of <see cref="QueryRetryPolicy"/> using the provided configuration for retry count and wait duration.
		/// </summary>
		/// <param name="queryLogic">The query to wrap with retry behavior.</param>
		/// <param name="retryConfiguration">The configuration for retrying different error types.</param>
		public QueryRetryPolicy(Func<T> queryLogic, QueryRetryConfiguration retryConfiguration) : this(queryLogic, retryConfiguration, GetRandomnessSource())
		{

		}

		/// <summary>
		/// Initializes a new instance of <see cref="QueryRetryPolicy"/> using the provided configuration for retry count and wait duration.
		/// </summary>
		/// <param name="queryLogic">The query to wrap with retry behavior.</param>
		/// <param name="retryConfiguration">The configuration for retrying different error types.</param>
		/// <param name="randomnessSource">A function returning a value between 0.0 and 1.0 for introducing jitter.</param>
		public QueryRetryPolicy(Func<T> queryLogic, QueryRetryConfiguration retryConfiguration, Func<double> randomnessSource)
		{
			_queryLogic = queryLogic;
			_retryConfig = retryConfiguration;
			_random = randomnessSource;
			_types = QueryPolicyTypes.None;
		}

		#region Enums

		[Flags]
		private enum QueryPolicyTypes
		{
			None = 0,
			HashJoin = 1 << 0,
			Deadlock = 1 << 1,
			CommandTimeout = 1 << 2,
			ConnectTimeout = 1 << 3,
			ConnectionPoolTimeout = 1 << 4,
			Cancellation = 1 << 5,
			PlaceholderError = 1 << 6,
			OnAny = 1 << 7,
			QueryHintError = 1 << 8
		}

		private enum TimeoutType
		{
			ConnectionPoolTimeout = 0,
			ConnectTimeout = 1,
			CommandTimeout = 2
		}

		#endregion

		#region Retry Policy Options

		/// <summary>
		/// Specifies an action to perform when any exception is thrown during query execution.
		/// This does not catch or re-throw the exception in any way.
		/// </summary>
		/// <param name="handler"></param>
		public void OnAnyException(Action handler)
		{
			if (!_types.HasFlag(QueryPolicyTypes.OnAny))
			{
				_queryLogic = Failover(
					_queryLogic,
					() => default,	// Unused.
					ex => { handler.Invoke(); return false; }	// "Always return false, but with side effects."
				);
				_types |= QueryPolicyTypes.OnAny;
			}
		}

		public void ReturnValueOnCancellation(T defaultValue)
		{
			if (!_types.HasFlag(QueryPolicyTypes.Cancellation))
			{
				_queryLogic = Failover(
					_queryLogic,
					() => defaultValue,
					IsCancelledEvent
				);
				_types |= QueryPolicyTypes.Cancellation;
			}
		}

		/// <summary>
		/// Specifies that the query should be retried when it encounters a deadlock.
		/// </summary>
		public void RetryOnDeadlock()
		{
			if (!_types.HasFlag(QueryPolicyTypes.Deadlock))
			{
				int numberOfTries = 0;
				_queryLogic = Retry(
					_queryLogic,
					_retryConfig.DeadlockConfiguration,
					ex => IsDeadlockError(ex, _retryConfig.DeadlockConfiguration, ref numberOfTries),
					WrapAndCombineExceptions);
				_types |= QueryPolicyTypes.Deadlock;
			}
		}

		/// <summary>
		/// Specifies that the query should be retried when the command fails to run within the allowed time.
		/// </summary>
		public void RetryOnCommandTimeout()
		{
			if (!_types.HasFlag(QueryPolicyTypes.CommandTimeout))
			{
				int numberOfTries = 0;
				var timer = new Stopwatch();
				_queryLogic = WrapLogicWithTimer(timer, _queryLogic);
				_queryLogic = Retry(
					_queryLogic,
					_retryConfig.CommandTimeoutConfiguration,
					ex => IsCommandTimeoutError(ex, _retryConfig.CommandTimeoutConfiguration, timer.Elapsed, ref numberOfTries),
					WrapAndCombineExceptions);
				_types |= QueryPolicyTypes.CommandTimeout;
			}
		}

		/// <summary>
		/// Specifies that the query should be retried when it fails to connect to the MySQL Server instance.
		/// </summary>
		public void RetryOnConnectionTimeout()
		{
			if (!_types.HasFlag(QueryPolicyTypes.ConnectTimeout))
			{
				int numberOfTries = 0;
				_queryLogic = Retry(
					_queryLogic,
					_retryConfig.ConnectionTimeoutConfiguration,
					ex => IsConnectionTimeoutError(ex, _retryConfig.ConnectionTimeoutConfiguration, TimeoutType.ConnectTimeout, ref numberOfTries),
					WrapAndCombineExceptions
				);
				_types |= QueryPolicyTypes.ConnectTimeout;
			}
		}

		/// <summary>
		/// Specifies that the query should be retried without query hints if it fails to execute correctly due to a query-hint-related error.
		/// </summary>
		/// <param name="originalQueryText">The original text of the query being retried.</param>
		/// <param name="retryQuery">A function that will re-attempt the query, accepting a new (hint-less) version of the query string as a parameter.</param>
		public void RetryWithoutQueryHints(string originalQueryText, Func<string, T> retryQuery)
		{
			if (!_types.HasFlag(QueryPolicyTypes.QueryHintError))
			{
				string withoutQueryHint = Regex.Replace(
					originalQueryText,
					_MYSQL_QUERY_HINT_PATTERN,
					"",
					RegexOptions.IgnoreCase | RegexOptions.CultureInvariant
				);
				_queryLogic = Failover(
					_queryLogic,
					() => retryQuery(withoutQueryHint),
					IsQueryHintError
				);
			}
		}

		#endregion

		/// <summary>
		/// Executes the SQL query with the specified retry behaviors.
		/// Each retry behavior is executed in the order it was applied.
		/// </summary>
		/// <returns>The result of the SQL query.</returns>
		public T RunQuery()
		{
			return UnwrapRetryException(_queryLogic).Invoke();
		}

		protected Func<T> UnwrapRetryException(Func<T> logic)
		{
			return () =>
			{
				try
				{
					return logic.Invoke();
				}
				catch (RetryPolicyException ex)
				{
					// We're re-throwing the exception to remove RetryHelper nonsense from the stack trace.
					throw ex.InnerException;
				}
			};
		}

		protected Func<T> Failover(Func<T> baseLogic, Func<T> failoverLogic, Func<Exception, bool> checkToFailover)
		{
			return baseLogic.WrapWithFailover(failoverLogic, checkToFailover);
		}

		protected Func<T> Retry(Func<T> baseLogic, QueryRetrySettings config, Func<Exception, bool> retryLogic, Func<Exception, IEnumerable<Exception>, Exception> exceptionAggregator)
		{
			return baseLogic.WrapWithBasicRetry(config.GetRetryCount, retryLogic, exceptionAggregator, WrapBackoffWithJitter(config));
		}

		protected Func<int, TimeSpan> WrapBackoffWithJitter(QueryRetrySettings config)
		{
			return currentTry =>
			{
				TimeSpan baseDuration = config.GetBackoffDuration(currentTry);
				return baseDuration.Scale(_random());
			};
		}

		private Func<T> WrapLogicWithTimer(Stopwatch timer, Func<T> queryLogic)
		{
			return () =>
			{
				try
				{
					timer.Restart();
					return queryLogic.Invoke();
				}
				finally
				{
					timer.Stop();
				}
			};
		}

		private Exception WrapAndCombineExceptions(Exception finalException, IEnumerable<Exception> otherExceptions)
		{
			if (finalException is RetryPolicyException)
			{
				finalException = finalException.InnerException;
			}

			return new RetryPolicyException(ExceptionHelper.CombineExceptions(finalException, otherExceptions));
		}

		private static Func<double> GetRandomnessSource()
		{
			var rand = new Random();
			return () => rand.NextDouble();
		}

		private bool IsCancelledEvent(Exception ex)
		{
			ex = (ex is RetryPolicyException) ? ex.InnerException : ex;
			bool isCancelled = ExceptionHelper.IsCancelledEvent(ex);
			if (isCancelled)
			{
				// TODO: Add cancelled event logging here.
			}

			return isCancelled;
		}

		private bool IsDeadlockError(Exception ex, QueryRetrySettings config, ref int numberOfTries)
		{
			ex = (ex is RetryPolicyException) ? ex.InnerException : ex;
			return ExceptionHelper.IsDeadlockException(ex) && config.CanRetry() && LogDeadlockEvent(ex, config, ref numberOfTries);
		}

		private bool LogDeadlockEvent(Exception ex, QueryRetrySettings config, ref int numberOfTries)
		{
			++numberOfTries;
			// TODO: Add logging here.
			return true;
		}

		private bool IsCommandTimeoutError(Exception ex, CommandTimeoutQueryRetrySettings config, TimeSpan duration, ref int numberOfTries)
		{
			ex = (ex is RetryPolicyException) ? ex.InnerException : ex;
			return ExceptionHelper.IsExecutionTimeoutException(ex)
				&& config.CanRetry()
				&& config.IsValidRetryDuration(duration)
				&& LogTimeoutEvent(ex, config, ref numberOfTries, TimeoutType.CommandTimeout);
		}

		private bool IsConnectionTimeoutError(Exception ex, QueryRetrySettings config, TimeoutType timeoutType, ref int numberOfTries)
		{
			ex = (ex is RetryPolicyException) ? ex.InnerException : ex;
			TimeoutType? type = GetTimeoutType(ex);
			bool isMatchingTimeout = type.HasValue && type.Value == timeoutType;
			return isMatchingTimeout && config.CanRetry() && LogTimeoutEvent(ex, config, ref numberOfTries, timeoutType);
		}

		private bool LogTimeoutEvent(Exception ex, QueryRetrySettings config, ref int numberOfTries, TimeoutType type)
		{
			++numberOfTries;
			// TODO: Add logging here.
			return true;
		}

		private TimeoutType? GetTimeoutType(Exception ex)
		{
			TimeoutType? type = null;
			if (ExceptionHelper.IsExecutionTimeoutException(ex))
			{
				type = TimeoutType.CommandTimeout;
			}
			else if (ExceptionHelper.IsConnectionTimeoutException(ex))
			{
				type = TimeoutType.ConnectTimeout;
			}

			return type;
		}

		private bool IsQueryHintError(Exception ex)
		{
			ex = (ex is RetryPolicyException) ? ex.InnerException : ex;
			bool isQueryHint = ExceptionHelper.IsQueryHintException(ex);
			if (isQueryHint)
			{
				// TODO: Add log statement here to log a query hint retry.
			}

			return isQueryHint;
		}
	}
}
