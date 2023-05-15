using Acmil.Data.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Acmil.Data.Helpers
{
	public class DbConfigHelper
	{
		public const int MINIMUM_COMMAND_TIMEOUT_IN_SECONDS = 15;
		public const int DEFAULT_SQL_COMMAND_TIMEOUT_IN_SECONDS = 30;

		private const int DEFAULT_NUMBER_OF_RETRIES_ON_DEADLOCK = 3;
		private const int DEFAULT_DEADLOCK_WAIT_DURATION_IN_MILLISECONDS = 100;

		private const int DEFAULT_NUMBER_OF_RETRIES_ON_COMMAND_TIMEOUT = 2;
		private const int DEFAULT_COMMAND_TIMEOUT_WAIT_DURATION_IN_MILLISECONDS = 100;

		private const int DEFAULT_NUMBER_OF_RETRIES_ON_CONNECTION_TIMEOUT = 5;
		private const int DEFAULT_CONNECTION_TIMEOUT_WAIT_DURATION_IN_MILLISECONDS = 100;


		private IDbSecurityHelper _securityHelper;
		private IDictionary<string, object> _config;

		internal DbConfigHelper(IDbSecurityHelper securityHelper)
		{
			_securityHelper = securityHelper;
		}

		//internal IDictionary Settings
		//{
		//	get
		//	{
		//		if (_config == null)
		//		{

		//		}
		//	}
		//}

		public static int DefaultCommandTimeout
		{
			get
			{
				// TODO: Make this and the other properties below configurable.
				int timeout = DEFAULT_SQL_COMMAND_TIMEOUT_IN_SECONDS;
				return timeout;
			}
		}

		// TODO: Make sure we're consistent in our terminology of try vs. retry.

		/// <summary>
		/// The number of times to re-attempt executing a particular SQL statement if it fails due to deadlock.
		/// </summary>
		public static int NumberOfTriesOnDeadlock => DEFAULT_NUMBER_OF_RETRIES_ON_DEADLOCK;

		/// <summary>
		/// The number of milliseconds to wait after a deadlock before re-running the query.
		/// </summary>
		public static int DeadlockWaitDurationInMilliseconds => DEFAULT_DEADLOCK_WAIT_DURATION_IN_MILLISECONDS;

		/// <summary>
		/// The number of times to re-attempt executing a particular SQL statement if it fails due to a command timeout.
		/// </summary>
		public static int NumberOfTriesOnCommandTimeout => DEFAULT_NUMBER_OF_RETRIES_ON_COMMAND_TIMEOUT;

		/// <summary>
		/// The number of milliseconds to wait after a command timeout before re-running the query.
		/// </summary>
		public static int CommandTimeoutBaseWaitDurationInMilliseconds => DEFAULT_COMMAND_TIMEOUT_WAIT_DURATION_IN_MILLISECONDS;

		/// <summary>
		/// The number of times to re-attempt executing a particualr SQL statement if it fails due ot a connection timeout.
		/// </summary>
		public static int NumberOfTriesOnConnectionTimeout => DEFAULT_NUMBER_OF_RETRIES_ON_CONNECTION_TIMEOUT;

		/// <summary>
		/// The number of milliseconds to wait after a connection timeout before re-running the query.
		/// </summary>
		public static int ConnectionTimeoutBaseWaitDurationInMilliseconds => DEFAULT_CONNECTION_TIMEOUT_WAIT_DURATION_IN_MILLISECONDS;

	}
}
