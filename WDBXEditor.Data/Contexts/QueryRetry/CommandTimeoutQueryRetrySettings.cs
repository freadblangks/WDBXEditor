using System;
using WDBXEditor.Data.Extensions;

namespace WDBXEditor.Data.Contexts.QueryRetry
{
	/// <summary>
	/// Object containing retry count and wait duration settings for errors encountered executing SQL commands.
	/// </summary>
	public class CommandTimeoutQueryRetrySettings : QueryRetrySettings
	{
		private readonly Func<int> _maximumRetryTimeoutDuration;

		/// <summary>
		/// Initializes a new instance of <see cref="CommandTimeoutQueryRetrySettings"/>.
		/// </summary>
		/// <param name="totalRetryAttempts">A function returning the number of times to re-run the query.</param>
		/// <param name="baseWaitDurationInMilliseconds">A function returning the number of milliseconds to wait before re-running the query.</param>
		/// <param name="maximumTimeoutRetryDuration">A function returning the threshold, in seconds, for determining if the query is too slow to retry.</param>
		public CommandTimeoutQueryRetrySettings(Func<int> totalRetryAttempts, Func<int> baseWaitDurationInMilliseconds, Func<int> maximumTimeoutRetryDuration)
			: base(totalRetryAttempts, baseWaitDurationInMilliseconds)
		{
			_maximumRetryTimeoutDuration = maximumTimeoutRetryDuration;
		}

		/// <summary>
		/// Determines if run duration of a timed out query is short enough for it to be retried.
		/// </summary>
		/// <param name="duration">The duration of the command that has timed out.</param>
		/// <returns>True if the duration is short enough. Otherwise, false.</returns>
		public bool IsValidRetryDuration(TimeSpan duration)
		{
			TimeSpan baseLimit = TimeSpan.FromSeconds(_maximumRetryTimeoutDuration());
			double totalMinutes = baseLimit.TotalMinutes;

			TimeSpan minutesRoundedUp;
			if (totalMinutes < 1)
			{
				minutesRoundedUp = baseLimit.Scale(2);
			}
			else
			{
				minutesRoundedUp = TimeSpan.FromMinutes(Math.Floor(totalMinutes) + 1);
			}

			return minutesRoundedUp > duration;
		}
	}
}
