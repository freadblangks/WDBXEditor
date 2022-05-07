using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WDBXEditor.Data.Constants;

namespace WDBXEditor.Data.Exceptions
{
	/// <summary>
	/// Helper class for interacting with exceptions.
	/// </summary>
	public static class ExceptionHelper
	{
		/// <summary>
		/// Combines multiple exceptions into one, sequentially, starting at <paramref name="finalException"/>,
		/// and then working backwards through <paramref name="otherExceptions"/> starting at the last Exception
		/// and ending at the first.
		/// </summary>
		/// <param name="finalException">The most recent exception, which will be listed first.</param>
		/// <param name="otherExceptions">All other exceptions, which will be listed in reverse order after the <paramref name="finalException"/>.</param>
		/// <returns></returns>
		public static Exception CombineExceptions(Exception finalException, IEnumerable<Exception> otherExceptions)
		{
			Exception retVal;

			if (otherExceptions == null || otherExceptions.Count() == 0)
			{
				retVal = finalException;
			}
			else
			{
				var exceptionString = new StringBuilder();
				exceptionString.AppendLine("Final Exception:");
				exceptionString.AppendLine(finalException.ToString());

				for (int i = otherExceptions.Count() - 1; i > 0; --i)
				{
					exceptionString.AppendLine("-----------------");
					exceptionString.AppendLine($"Exception {i + 1}");
					exceptionString.AppendLine(otherExceptions.ElementAt(i).ToString());
				}

				retVal = CreateWrappedException(finalException, exceptionString.ToString());
			}

			return retVal;
		}

		/// <summary>
		/// Checks if an exception is indicative of an operation cancellation.
		/// </summary>
		/// <param name="ex">The exception to check.</param>
		/// <returns>True if the exception is indicative of an operation cancellation. Otherwise, false.</returns>
		public static bool IsCancelledEvent(Exception ex)
		{
			bool retVal = false;
			if (ex is MySqlException sqlException)
			{
				retVal = sqlException.Message.Contains(MySqlConstants.CANCELLATION_ERROR_MESSAGE, StringComparison.OrdinalIgnoreCase);
			}

			return retVal;
		}

		/// <summary>
		/// Checks if an exception is indicative of a MySQL deadlock error.
		/// </summary>
		/// <param name="ex">The exception to check.</param>
		/// <returns>True if the exception is indicative of a MySQL deadlock error. Otherwise, false.</returns>
		public static bool IsDeadlockException(Exception ex)
		{
			bool retVal = false;
			if (ex is MySqlException sqlException)
			{
				retVal = MySqlConstants.DEADLOCK_ERROR_CODES.Contains(sqlException.Number);
			}

			return retVal;
		}

		/// <summary>
		/// Checks if an exception is indicative of a MySQL command timeout.
		/// </summary>
		/// <param name="ex">The exception to check.</param>
		/// <returns>True if the exception indicates a MySQL command timeout. Otherwise, false.</returns>
		public static bool IsExecutionTimeoutException(Exception ex) => IsTimeoutException(ex) && ex.Message.StartsWith("Timeout expired");

		/// <summary>
		/// Checks if an exception is indicative of a MySQL connection timeout.
		/// </summary>
		/// <param name="ex">The exception to check.</param>
		/// <returns>True if the exception indicates a MySQL connection timeout. Otherwise, false.</returns>
		public static bool IsConnectionTimeoutException(Exception ex) => IsTimeoutException(ex) && !IsExecutionTimeoutException(ex);

		/// <summary>
		/// Checks if an exception is indicative of either a MySQL command timeout or MySQL connection timeout.
		/// </summary>
		/// <param name="ex">The exception to check.</param>
		/// <returns>True if the exception indicates either a MySQL command timeout or MySQL connection timeout. Otherwise, false.</returns>
		public static bool IsTimeoutException(Exception ex)
		{
			bool retVal = false;
			if (ex is MySqlException sqlException)
			{
				retVal = MySqlConstants.COMMAND_TIMEOUT_ERROR_CODES.Contains(sqlException.Number);
			}

			return retVal;
		}

		/// <summary>
		/// Checks if an exception is indicative of a MySQL error caused by the use of a query hint.
		/// </summary>
		/// <param name="ex">The exception to check.</param>
		/// <returns>True if an exception is indicative of a MySQL error caused by the use of a query hint. Otherwise, false.</returns>
		public static bool IsQueryHintException(Exception ex)
		{
			bool retVal = false;
			if (ex is MySqlException sqlException)
			{
				retVal = MySqlConstants.QUERY_HINT_ERROR_CODES.Contains(sqlException.Number);
			}

			return retVal;
		}

		/// <summary>
		/// Throws a new more-specific Exception based on the type of exception thrown.
		/// </summary>
		/// <param name="ex">The exception to wrap.</param>
		/// <param name="executedSql">The SQL command whose execution produced the exception.</param>
		/// <exception cref="MySqlDeadlockException">Thrown if the exception is indicative of a deadlock error.</exception>
		/// <exception cref="MySqlTimeoutException">Thrown if the exception is indicative of a command timeout or connection timeout from MySQL.</exception>
		/// <exception cref="ExecuteSqlStatementFailedException">Thrown if there's no more specific exception to throw.</exception>
		public static void ThrowWrappedException(Exception ex, string executedSql) => throw CreateWrappedException(ex, executedSql);

		/// <summary>
		/// Throws a new more-specific Exception based on the type of exception thrown.
		/// </summary>
		/// <param name="ex">The exception to wrap.</param>
		/// <param name="executedSql">The SQL command whose execution produced the exception.</param>
		/// <param name="parameters">The parameters the failed SQL statement was executed with.</param>
		/// <exception cref="MySqlDeadlockException">Thrown if the exception is indicative of a deadlock error.</exception>
		/// <exception cref="MySqlTimeoutException">Thrown if the exception is indicative of a command timeout or connection timeout from MySQL.</exception>
		/// <exception cref="ExecuteSqlStatementFailedException">Thrown if there's no more specific exception to throw.</exception>
		public static void ThrowWrappedException(Exception ex, string executedSql, IEnumerable<MySqlParameter> parameters) => throw CreateWrappedException(ex, executedSql, parameters);

		private static Exception CreateWrappedException(Exception ex, string executedSql) => CreateWrappedException(ex, executedSql, null);

		private static Exception CreateWrappedException(Exception ex, string executedSql, IEnumerable<MySqlParameter> parameters)
		{
			Exception retVal;
			if (IsDeadlockException(ex))
			{
				retVal = new MySqlDeadlockException(ex.Message, ex);
			}
			else if (IsTimeoutException(ex))
			{
				retVal = new MySqlTimeoutException(ex.Message, ex);
			}
			else
			{
				retVal = new ExecuteSqlStatementFailedException(ex, executedSql, parameters);
			}

			return retVal;
		}

		
	}
}
