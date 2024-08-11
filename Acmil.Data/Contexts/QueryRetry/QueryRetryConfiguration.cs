using Acmil.Data.Helpers;
using System;

namespace Acmil.Data.Contexts.QueryRetry
{
	/// <summary>
	/// Object containing retry count and wait duration settings for common types of errors encountered executing SQL commands.
	/// </summary>
	public class QueryRetryConfiguration
	{
		/// <summary>
		/// Settings for retrying deadlock errors.
		/// </summary>
		public readonly QueryRetrySettings DeadlockConfiguration;

		/// <summary>
		/// Settings for retrying command timeout errors.
		/// </summary>
		public readonly CommandTimeoutQueryRetrySettings CommandTimeoutConfiguration;

		/// <summary>
		/// Settings for retrying connection timeout errors.
		/// </summary>
		public readonly QueryRetrySettings ConnectionTimeoutConfiguration;

		/// <summary>
		/// Initializes a new instance of <see cref="QueryRetryConfiguration"/>.
		/// </summary>
		public QueryRetryConfiguration() : this(
			() => DbConfigHelper.NumberOfTriesOnDeadlock,
			() => DbConfigHelper.DeadlockWaitDurationInMilliseconds,
			() => DbConfigHelper.NumberOfTriesOnCommandTimeout,
			() => DbConfigHelper.CommandTimeoutBaseWaitDurationInMilliseconds,
			() => DbConfigHelper.NumberOfTriesOnConnectionTimeout,
			() => DbConfigHelper.ConnectionTimeoutBaseWaitDurationInMilliseconds,
			() => DbConfigHelper.DefaultCommandTimeout)
		{

		}

		/// <summary>
		/// Initializes a new instance of <see cref="QueryRetryConfiguration"/>.
		/// </summary>
		/// <param name="deadlockRetries">The number of times to re-run the query when it encounters a deadlock.</param>
		/// <param name="deadlockWaitInMilliseconds">The number of milliseconds to wait after a deadlock before re-running the query.</param>
		/// <param name="commandTimeoutRetries">The number of times to re-run the query when it encounters a command timeout.</param>
		/// <param name="commandTimeoutBaseWaitInMilliseconds">The number of milliseconds to wait after a command timeout before re-running the query.</param>
		/// <param name="connectionTimeoutRetries">The number of times to re-run the query when it encounters a connection timeout.</param>
		/// <param name="connectionTimeoutBaseWaitInMilliseconds">The number of milliseconds to wait after a connection timeout before re-running the query.</param>
		/// <param name="maxTimeoutRetryDuration">The threshold, in seconds, for determining if the query is too slow to retry.</param>
		public QueryRetryConfiguration(
			int deadlockRetries,
			int deadlockWaitInMilliseconds,
			int commandTimeoutRetries,
			int commandTimeoutBaseWaitInMilliseconds,
			int connectionTimeoutRetries,
			int connectionTimeoutBaseWaitInMilliseconds,
			int maxTimeoutRetryDuration)
			: this(
			() => deadlockRetries,
			() => deadlockWaitInMilliseconds,
			() => commandTimeoutRetries,
			() => commandTimeoutBaseWaitInMilliseconds,
			() => connectionTimeoutRetries,
			() => connectionTimeoutBaseWaitInMilliseconds,
			() => maxTimeoutRetryDuration
		)
		{

		}

		/// <summary>
		/// Initializes a new instance of <see cref="QueryRetryConfiguration"/>.
		/// </summary>
		/// <param name="deadlockRetries">A function returning the number of times to re-run the query when it encounters a deadlock.</param>
		/// <param name="deadlockWaitInMilliseconds">A function returning the number of milliseconds to wait after a deadlock before re-running the query.</param>
		/// <param name="commandTimeoutRetries">A function returning the number of times to re-run the query when it encounters a command timeout.</param>
		/// <param name="commandTimeoutBaseWaitInMilliseconds">A function returning the number of milliseconds to wait after a command timeout before re-running the query.</param>
		/// <param name="connectionTimeoutRetries">A function returning the number of times to re-run the query when it encounters a connection timeout.</param>
		/// <param name="connectionTimeoutBaseWaitInMilliseconds">A function returning the number of milliseconds to wait after a connection timeout before re-running the query.</param>
		/// <param name="maxTimeoutRetryDuration">A function returning the threshold, in seconds, for determining if the query is too slow to retry.</param>
		public QueryRetryConfiguration(
			Func<int> deadlockRetries,
			Func<int> deadlockWaitInMilliseconds,
			Func<int> commandTimeoutRetries,
			Func<int> commandTimeoutBaseWaitInMilliseconds,
			Func<int> connectionTimeoutRetries,
			Func<int> connectionTimeoutBaseWaitInMilliseconds,
			Func<int> maxTimeoutRetryDuration)
		{
			DeadlockConfiguration = new QueryRetrySettings(deadlockRetries, deadlockWaitInMilliseconds);
			CommandTimeoutConfiguration = new CommandTimeoutQueryRetrySettings(commandTimeoutRetries, commandTimeoutBaseWaitInMilliseconds, maxTimeoutRetryDuration);
			ConnectionTimeoutConfiguration = new QueryRetrySettings(connectionTimeoutRetries, connectionTimeoutBaseWaitInMilliseconds);
		}
	}
}
