using System.Collections.Generic;

namespace WDBXEditor.Data.Constants
{
	/// <summary>
	/// Class for constants relating to MySQL.
	/// </summary>
	public static class MySqlConstants
	{
		/// <summary>
		/// Error message indicating that the operation was cancelled by the user.
		/// </summary>
		/// <remarks>
		/// This is an error that crops up from time to time with MS SQL. No idea
		/// if there's a MySQL equivalent.
		/// </remarks>
		public const string CANCELLATION_ERROR_MESSAGE = "Operation cancelled by user";

		/// <summary>
		/// MySQL error codes that indicate hint errors that should be retried by removing the hints.
		/// </summary>
		public static readonly HashSet<int> QUERY_HINT_ERROR_CODES = new HashSet<int>()
		{
			3123,	// Optimizer hint syntax error
			3124,	// Unsupported MAX_EXECUTION_TIME
			3125,	// MAX_EXECUTION_TIME hint is supported by top-level standalone SELECT statements only
			3126,	// Hint %s is ignored as conflicting/duplicated
			3127,	// Query block name %s is not found for %s hint
			3128,	// Unresolved name %s for %s hint
			3515,	// Hints aren't supported in %s
			3576,	// In recursive query block of Recursive Common Table Expression '%s', the recursive table must neither be in the right argument of a LEFT JOIN, nor be forced to be non-first with join order hints
			3614,	// Invalid number of arguments for hint %s
		};

		/// <summary>
		/// MySQL error codes that indicate a deadlock.
		/// </summary>
		public static readonly HashSet<int> DEADLOCK_ERROR_CODES = new HashSet<int>()
		{
			1213,	// Deadlock found when trying to get lock; try restarting transaction
			1614,	// Transaction branch was rolled back: deadlock was detected
			3058,	// Deadlock found when trying to get user-level lock; try rolling back transaction/releasing locks and restarting lock acquisition
			3132,	// Deadlock found when trying to get locking service lock; try releasing locks and restarting lock acquisition
		};

		/// <summary>
		/// MySql error codes that indicate a command timeout.
		/// </summary>
		public static readonly HashSet<int> COMMAND_TIMEOUT_ERROR_CODES = new HashSet<int>
		{
			3024,	// The wait_timeout period was exceeded, the idle time since last command was too long
			3699,	// Timeout exceeded in regular expression match
			12872,	// Semaphore wait has lasted > %llu seconds. We intentionally crash the server because it appears to be hung
			13417,	// The wait_timeout period was exceeded, the idle time since last command was too long
			13730	// 'wait_timeout' period of %s seconds was exceeded for %s. The idle time since last command was too long
		};

		public static readonly HashSet<int> AUTHENTICATION_FAILURE_ERROR_CODES = new HashSet<int>
		{
			1044,	// User was denied access to a particular database.
			1045,	// User was denied access.
			1698,	// User was set up to authenticate using something like the the "auth_socket" plugin, but a login was attempted using a password.
			3879,	// User was denied access to a particular database.
			3955,	// User was denied access because of too many consecutive failed logins.
		};
	}
}
