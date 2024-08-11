using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Acmil.Data.Contexts.QueryRetry
{
	/// <summary>
	/// Provides access to methods for retrying a provided delegate with either failover or retry.
	/// </summary>
	public static partial class RetryHelper
	{
		private const int _RETRY_DELAY = 50;

		#region TryWithFailover

		/// <summary>
		/// Attempts to execute the provided action. Conditionally executes a failover action based on the exception thrown.
		/// </summary>
		/// <param name="baseLogic">The logic to execute.</param>
		/// <param name="failoverLogic">The logic to execute if <see param="checkToFailOver"/> results in True.</param>
		/// <param name="checkToFailOver">A function that takes in the exception produced by <see param="baseLogic"/> 
		/// and produces a boolean indicating whether or not to execute <see param="failoverLogic"/>.</param>
		/// <remarks>If <see param="checkToFailOver"/> produces False, the exception thrown by <see param="baseLogic"/> is re-thrown.</remarks>
		public static void TryWithFailover(Action baseLogic, Action failoverLogic, Func<Exception, bool> checkToFailOver)
		{
			try
			{
				baseLogic.Invoke();
			}
			catch (Exception ex) when (checkToFailOver(ex))
			{
				failoverLogic.Invoke();
			}
		}

		/// <summary>
		/// Asynchronously attempts to execute the provided action. Conditionally executes failover logic based on the exception thrown.
		/// </summary>
		/// <param name="baseLogic">The logic to execute asynchronously.</param>
		/// <param name="failoverLogic">The logic to execute asynchronously if <see param="checkToFailOver"/> results in True.</param>
		/// <param name="checkToFailOver">A synchronous function that takes in the exception produced by <see param="baseLogic"/> 
		/// and produces a boolean indicating whether or not to execute <see param="failoverLogic"/>.</param>
		/// <param name="cancellationToken">The cancellation token to respect.</param>
		/// <remarks>If <see param="checkToFailOver"/> produces False, the exception thrown by <see param="baseLogic"/> is re-thrown.</remarks>
		public async static Task TryWithFailoverAsync(Func<Task> baseLogic, Func<Task> failoverLogic, Func<Exception, bool> checkToFailover, CancellationToken cancellationToken)
		{
			bool doFailOverLogic = false;
			try
			{
				await baseLogic.Invoke();
			}
			catch (Exception ex) when (!cancellationToken.IsCancellationRequested && checkToFailover(ex))
			{
				doFailOverLogic = true;
			}

			if (!cancellationToken.IsCancellationRequested && doFailOverLogic)
			{
				await failoverLogic.Invoke();
			}
		}

		/// <summary>
		/// Attempts to execute the provided function. Conditionally executes a failover function based on the exception thrown.
		/// </summary>
		/// <param name="baseLogic">The logic to execute.</param>
		/// <param name="failoverLogic">The logic to execute if <see param="checkToFailOver"/> results in True.</param>
		/// <param name="checkToFailOver">A function that takes in the exception produced by <see param="baseLogic"/> 
		/// and produces a boolean indicating whether or not to execute <see param="failoverLogic"/>.</param>
		/// <remarks>If <see param="checkToFailOver"/> produces False, the exception thrown by <see param="baseLogic"/> is re-thrown.</remarks>
		/// <returns>The return value of <see param="baseLogic"/> if it succeeds; otherwise, the return value of <see param="failoverLogic"/>.</returns>
		public static T TryWithFailover<T>(Func<T> baseLogic, Func<T> failoverLogic, Func<Exception, bool> checkToFailover)
		{
			T retVal;
			try
			{
				retVal = baseLogic.Invoke();
			}
			catch (Exception ex) when (checkToFailover(ex))
			{
				retVal = failoverLogic.Invoke();
			}

			return retVal;
		}

		/// <summary>
		/// Asynchronously attempts to execute the provided function. Conditionally executes failover logic based on the exception thrown.
		/// </summary>
		/// <param name="baseLogic">The logic to execute asynchronously.</param>
		/// <param name="failoverLogic">The logic to execute asynchronously if <see param="checkToFailOver"/> results in True.</param>
		/// <param name="checkToFailOver">A synchronous function that takes in the exception produced by <see param="baseLogic"/> 
		/// and produces a boolean indicating whether or not to execute <see param="failoverLogic"/>.</param>
		/// <remarks>If <see param="checkToFailOver"/> produces False, the exception thrown by <see param="baseLogic"/> is re-thrown.</remarks>
		/// <returns>The return value of <see param="baseLogic"/> if it succeeds; otherwise, the return value of <see param="failoverLogic"/>.</returns>
		public async static Task<T> TryWithFailoverAsync<T>(Func<Task<T>> baseLogic, Func<Task<T>> failoverLogic, Func<Exception, bool> checkToFailover, CancellationToken cancellationToken)
		{
			bool doFailOverLogic = false;
			var retVal = default(T);
			try
			{
				retVal = await baseLogic.Invoke();
			}
			catch (Exception ex) when (!cancellationToken.IsCancellationRequested && checkToFailover(ex))
			{
				doFailOverLogic = true;
			}

			if (!cancellationToken.IsCancellationRequested && doFailOverLogic)
			{
				retVal = await failoverLogic.Invoke();
			}

			return retVal;
		}
		#endregion

		#region RetryLogic

		/// <summary>
		/// Executes the provided action, conditionally retrying the provided number of times.
		/// </summary>
		/// <param name="func">The base action to be invoked.</param>
		/// <param name="retrys">The number of times to retry <see param="baseLogic"/>.</param>
		/// <param name="retryLogic">A function taking in the exception thrown by <see param="baseLogic"/> 
		///		and producing a boolean indicating whether or not to retry the action.</param>
		/// <param name="exceptionAggratator">A function taking in an exception and
		///		IEnumerable of exceptions to aggregate and throw as a single exception.</param>
		public static void BasicRetry(Action func, int retrys, Func<Exception, bool> retryLogic, Func<Exception, IEnumerable<Exception>, Exception> exceptionAggratator)
		{
			BasicRetry(func, () => retrys, retryLogic, exceptionAggratator, tryNumber => TimeSpan.FromMilliseconds(_RETRY_DELAY));
		}

		public static void BasicRetry(Action func, Func<int> retrys, Func<Exception, bool> retryLogic, Func<Exception, IEnumerable<Exception>, Exception> exceptionAggratator, Func<int, TimeSpan> waitLogic)
		{
			int currentRetry = 0;
			var pastExceptions = new List<Exception>();
			while (true)
			{
				try
				{
					// Calling external service.
					func.Invoke();
					// Return Or break.
					break;
				}
				catch (Exception ex) when (retryLogic(ex))
				{
					currentRetry += 1;
					// Check if the exception thrown was a transient exception
					// based on the logic in the error detection strategy.
					// Determine whether to retry the operation, as well as how
					// long to wait, based on the retry strategy.
					if (currentRetry > retrys())
					{
						// If this Is Not a transient error
						throw exceptionAggratator(ex, pastExceptions);
					}

					pastExceptions.Add(ex);
				}
				catch (Exception ex)
				{
					throw exceptionAggratator(ex, pastExceptions);
				}
				// Wait to retry the operation.
				// Consider calculating an exponential delay here And
				// using a strategy best suited for the operation And fault.
				Thread.CurrentThread.Join(waitLogic(currentRetry));
			}
		}

		public static Task BasicRetryAsync(Func<Task> func, int retrys, Func<Exception, bool> retryLogic, Func<Exception, IEnumerable<Exception>, Exception> exceptionAggratator, CancellationToken cancelToken)
		{
			return BasicRetryAsync(func, () => retrys, retryLogic, exceptionAggratator, tryNumber => TimeSpan.FromMilliseconds(_RETRY_DELAY), cancelToken);
		}

		/// <summary>
		/// Asynchronously executes the provided action, conditionally retrying the provided number of times.
		/// </summary>
		/// <param name="func">The base action to be invoked asynchronously.</param>
		/// <param name="retrys">The number of times to retry <see param="baseLogic"/>.</param>
		/// <param name="retryLogic">A synchronous function taking in the exception thrown by <see param="baseLogic"/> 
		///		and producing a boolean indicating whether or not to retry the action.</param>
		/// <param name="exceptionAggratator">A synchronous function taking in an exception and
		///		IEnumerable of exceptions to aggregate and throw as a single exception.</param>
		///	<param name="cancelToken">The cancellation token to be respected.</param>
		///	<exception cref="TaskCanceledException">The task was cancelled prior to completion.</exception>
		public async static Task BasicRetryAsync(Func<Task> func, Func<int> retrys, Func<Exception, bool> retryLogic, Func<Exception, IEnumerable<Exception>, Exception> exceptionAggratator, Func<int, TimeSpan> waitLogic, CancellationToken cancelToken)
		{
			int currentRetry = 0;
			var pastExceptions = new List<Exception>();
			while (!cancelToken.IsCancellationRequested)
			{
				try
				{
					// Calling external service.
					await func.Invoke();
					// Return Or break.
					break;
				}
				catch (Exception ex) when (retryLogic(ex))
				{
					currentRetry += 1;
					// Check if the exception thrown was a transient exception
					// based on the logic in the error detection strategy.
					// Determine whether to retry the operation, as well as how
					// long to wait, based on the retry strategy.
					if (currentRetry > retrys())
					{
						// If this Is Not a transient error
						throw exceptionAggratator(ex, pastExceptions);
					}

					pastExceptions.Add(ex);
				}
				catch (Exception ex)
				{
					throw exceptionAggratator(ex, pastExceptions);
				}

				try
				{
					// Wait to retry the operation.
					// Consider calculating an exponential delay here And
					// using a strategy best suited for the operation And fault.
					await Task.Delay(waitLogic(currentRetry), cancelToken);
				}
				catch (TaskCanceledException)
				{
					// do not throw cancellation exceptions
				}
			}
		}

		/// <summary>
		/// Executes the provided function, conditionally retrying the provided number of times.
		/// </summary>
		/// <param name="func">The base function to be invoked.</param>
		/// <param name="retrys">The number of times to retry <see param="baseLogic"/>.</param>
		/// <param name="retryLogic">A function taking in the exception thrown by <see param="baseLogic"/> 
		///		and producing a boolean indicating whether or not to retry the action.</param>
		/// <param name="exceptionAggratator">A function taking in an exception and
		///		IEnumerable of exceptions to aggregate and throw as a single exception.</param>
		///	<returns>The return value of <see param="func"/>.</returns>
		/// <exception cref="Exception">An aggregate exception containg one or more Exceptions; 
		///		see the inner exception for details.</exception>
		public static T BasicRetry<T>(Func<T> func, int retrys, Func<Exception, bool> retryLogic, Func<Exception, IEnumerable<Exception>, Exception> exceptionAggratator)
		{
			return BasicRetry(func, () => retrys, retryLogic, exceptionAggratator, tryNumber => TimeSpan.FromMilliseconds(_RETRY_DELAY));
		}

		public static T BasicRetry<T>(Func<T> func, Func<int> retrys, Func<Exception, bool> retryLogic, Func<Exception, IEnumerable<Exception>, Exception> exceptionAggratator, Func<int, TimeSpan> waitLogic)
		{
			T retVal;
			int currentRetry = 0;
			var pastExceptions = new List<Exception>();
			while (true)
			{
				try
				{
					// Calling external service.
					retVal = func.Invoke();
					// Return Or break.
					break;
				}
				catch (Exception ex) when (retryLogic(ex))
				{
					currentRetry += 1;
					// Check if the exception thrown was a transient exception
					// based on the logic in the error detection strategy.
					// Determine whether to retry the operation, as well as how
					// long to wait, based on the retry strategy.
					if (currentRetry > retrys())
					{
						// If this Is Not a transient error
						throw exceptionAggratator(ex, pastExceptions);
					}

					pastExceptions.Add(ex);
				}
				catch (Exception ex)
				{
					throw exceptionAggratator(ex, pastExceptions);
				}
				// Wait to retry the operation.
				// Consider calculating an exponential delay here And
				// using a strategy best suited for the operation And fault.
				Thread.CurrentThread.Join(waitLogic(currentRetry));
			}

			return retVal;
		}

		public static Task<T> BasicRetryAsync<T>(Func<Task<T>> func, int retrys, Func<Exception, bool> retryLogic, Func<Exception, IEnumerable<Exception>, Exception> exceptionAggratator, CancellationToken cancelToken)
		{
			return BasicRetryAsync(func, () => retrys, retryLogic, exceptionAggratator, tryNumber => TimeSpan.FromMilliseconds(_RETRY_DELAY), cancelToken);
		}

		/// <summary>
		/// Asynchronously executes the provided function, conditionally retrying the provided number of times.
		/// </summary>
		/// <param name="func">The base function to be invoked.</param>
		/// <param name="retrys">The number of times to retry <see param="baseLogic"/>.</param>
		/// <param name="retryLogic">A synchronous function taking in the exception thrown by <see param="baseLogic"/> 
		///		and producing a boolean indicating whether or not to retry the action.</param>
		/// <param name="exceptionAggratator">A synchronous function taking in an exception and
		///		IEnumerable of exceptions to aggregate and throw as a single exception.</param>
		///	<returns>The return value of <see param="func"/>.</returns>
		/// <exception cref="Exception">An aggregate exception containg one or more Exceptions; 
		///		see the inner exception for details.</exception>
		public async static Task<T> BasicRetryAsync<T>(Func<Task<T>> func, Func<int> retrys, Func<Exception, bool> retryLogic, Func<Exception, IEnumerable<Exception>, Exception> exceptionAggratator, Func<int, TimeSpan> waitLogic, CancellationToken cancelToken)
		{
			var retVal = default(T);
			int currentRetry = 0;
			var pastExceptions = new List<Exception>();
			while (!cancelToken.IsCancellationRequested)
			{
				try
				{
					// Calling external service.
					retVal = await func.Invoke();
					// Return Or break.
					break;
				}
				catch (Exception ex) when (retryLogic(ex))
				{
					currentRetry += 1;
					// Check if the exception thrown was a transient exception
					// based on the logic in the error detection strategy.
					// Determine whether to retry the operation, as well as how
					// long to wait, based on the retry strategy.
					if (currentRetry > retrys())
					{
						// If this Is Not a transient error
						throw exceptionAggratator(ex, pastExceptions);
					}

					pastExceptions.Add(ex);
				}
				catch (Exception ex)
				{
					throw exceptionAggratator(ex, pastExceptions);
				}

				try
				{
					// Wait to retry the operation.
					// Consider calculating an exponential delay here And
					// using a strategy best suited for the operation And fault.
					await Task.Delay(waitLogic(currentRetry), cancelToken);
				}
				catch (TaskCanceledException)
				{
					// do not throw cancellation exceptions
				}
			}

			return retVal;
		}

		#endregion
	}
}
