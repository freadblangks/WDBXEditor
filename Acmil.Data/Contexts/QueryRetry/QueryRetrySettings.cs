using Acmil.Data.Extensions;
using System;
using System.Linq;

namespace Acmil.Data.Contexts.QueryRetry
{
	/// <summary>
	/// Object containing retry count and wait duration settings for errors encountered executing SQL statements.
	/// </summary>
	public class QueryRetrySettings
	{
		private readonly Func<int> _baseWaitDurationInMilliseconds;
		private readonly Func<int> _totalRetryAttempts;

		/// <summary>
		/// Initializes a new instance of <see cref="QueryRetrySettings"/>.
		/// </summary>
		/// <param name="totalRetryAttempts">A function returning the number of times to re-run the query.</param>
		/// <param name="baseWaitDurationInMilliseconds">A function returning the number of milliseconds to wait before re-running the query.</param>
		public QueryRetrySettings(Func<int> totalRetryAttempts, Func<int> baseWaitDurationInMilliseconds)
		{
			_totalRetryAttempts = totalRetryAttempts;
			_baseWaitDurationInMilliseconds = baseWaitDurationInMilliseconds;
		}

		/// <summary>
		/// Returns the configured number of retry attempts.
		/// </summary>
		/// <returns>The configured number of retry attempts.</returns>
		public int GetRetryCount()
		{
			return _totalRetryAttempts();
		}

		/// <summary>
		/// Checks if the provided configuration is accessible.
		/// </summary>
		/// <returns>True if the provided configuration is accessible. Otherwise, false.</returns>
		public bool CanRetry()
		{
			bool configured = true;
			try
			{
				int maxAttempts = GetRetryCount();
				_ = GetBaseWaitDuration();
			}
			catch
			{
				configured = false;
			}
			return configured;
		}

		/// <summary>
		/// Calculates the total wait duration if all retry attempts are used.
		/// </summary>
		/// <returns>The total wait duration if all retry attempts are used.</returns>
		public TimeSpan GetTotalBackoffDuration() => Enumerable.Range(1, GetRetryCount())
			.Select(attempt => GetBackoffDuration(attempt)).Sum();

		/// <summary>
		/// Gets the calculated backoff duration for the provided attempt number.
		/// </summary>
		/// <param name="currentAttempt">The provided attempt number.</param>
		/// <returns>The calculated backoff duration for the provided attempt number.</returns>
		public TimeSpan GetBackoffDuration(int currentAttempt)
		{
			currentAttempt = Math.Min(currentAttempt, GetRetryCount());
			double attemptFactor = Math.Pow(2, Math.Max(currentAttempt - 1, 0));
			return GetBaseWaitDuration().Scale(attemptFactor);
		}

		private TimeSpan GetBaseWaitDuration() => TimeSpan.FromMilliseconds(_baseWaitDurationInMilliseconds());
	}
}
