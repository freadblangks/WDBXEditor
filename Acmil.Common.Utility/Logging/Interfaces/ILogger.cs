using Acmil.Common.Utility.Logging.Enums;
using System;

namespace Acmil.Common.Utility.Logging.Interfaces
{
	/// <summary>
	/// A logger for logging messages.
	/// </summary>
	public interface ILogger
	{
		/// <summary>
		/// Logs a critical-level (fatal-level) message.
		/// </summary>
		/// <param name="message">The message to log.</param>
		/// <remarks>
		/// See the <see cref="LoggingLevel"/> enum for the available logging levels.
		/// </remarks>
		void LogCritical(string message);

		/// <summary>
		/// Logs a critical-level (fatal-level) message with an exception.
		/// </summary>
		/// <param name="exception">The exception to log.</param>
		/// <param name="message">The message to log.</param>
		/// <remarks>
		/// See the <see cref="LoggingLevel"/> enum for the available logging levels.
		/// </remarks>
		void LogCritical(Exception exception, string message);

		/// <summary>
		/// Logs an error-level message.
		/// </summary>
		/// <param name="message">The message to log.</param>
		/// <remarks>
		/// See the <see cref="LoggingLevel"/> enum for the available logging levels.
		/// </remarks>
		void LogError(string message);

		/// <summary>
		/// Logs an error-level message with an exception.
		/// </summary>
		/// <param name="exception">The exception to log.</param>
		/// <param name="message">The message to log.</param>
		/// <remarks>
		/// See the <see cref="LoggingLevel"/> enum for the available logging levels.
		/// </remarks>
		void LogError(Exception exception, string message);

		/// <summary>
		/// Logs a warning-level message.
		/// </summary>
		/// <param name="message">The message to log.</param>
		/// <remarks>
		/// See the <see cref="LoggingLevel"/> enum for the available logging levels.
		/// </remarks>
		void LogWarning(string message);

		/// <summary>
		/// Logs a warning-level message with an exception.
		/// </summary>
		/// <param name="exception">The exception to log.</param>
		/// <param name="message">The message to log.</param>
		/// <remarks>
		/// See the <see cref="LoggingLevel"/> enum for the available logging levels.
		/// </remarks>
		void LogWarning(Exception exception, string message);

		/// <summary>
		/// Logs a debug-level message.
		/// </summary>
		/// <param name="message">The message to log.</param>
		/// <remarks>
		/// See the <see cref="LoggingLevel"/> enum for the available logging levels.
		/// </remarks>
		void LogDebug(string message);

		/// <summary>
		/// Logs a debug-level message with an exception.
		/// </summary>
		/// <param name="exception">The exception to log.</param>
		/// <param name="message">The message to log.</param>
		/// <remarks>
		/// See the <see cref="LoggingLevel"/> enum for the available logging levels.
		/// </remarks>
		void LogDebug(Exception exception, string message);

		/// <summary>
		/// Logs an information-level message.
		/// </summary>
		/// <param name="message">The message to log.</param>
		/// <remarks>
		/// See the <see cref="LoggingLevel"/> enum for the available logging levels.
		/// </remarks>
		void LogInformation(string message);

		/// <summary>
		/// Logs an information-level message with an exception.
		/// </summary>
		/// <param name="exception">The exception to log.</param>
		/// <param name="message">The message to log.</param>
		/// <remarks>
		/// See the <see cref="LoggingLevel"/> enum for the available logging levels.
		/// </remarks>
		void LogInformation(Exception exception, string message);

		/// <summary>
		/// Logs a verbose-level message.
		/// </summary>
		/// <param name="message">The message to log.</param>
		/// <remarks>
		/// See the <see cref="LoggingLevel"/> enum for the available logging levels.
		/// </remarks>
		void LogVerbose(string message);

		/// <summary>
		/// Logs a verbose-level message with an exception.
		/// </summary>
		/// <param name="exception">The exception to log.</param>
		/// <param name="message">The message to log.</param>
		/// <remarks>
		/// See the <see cref="LoggingLevel"/> enum for the available logging levels.
		/// </remarks>
		void LogVerbose(Exception exception, string message);
	}
}
