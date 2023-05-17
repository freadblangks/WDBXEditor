namespace Acmil.Common.Utility.Logging.Enums
{
	/// <summary>
	/// Different levels of logging verbosity.
	/// </summary>
	/// <remarks>
	/// When the logging level is set to one of these values, messages of that level and above will be logged.
	/// For example, if the logging level is <see cref="Warning"/>, messages of levels <see cref="Warning"/>,
	/// <see cref="Error"/>, and <see cref="Critical"/> will be logged.
	/// </remarks>
	public enum LoggingLevel
	{
		/// <summary>
		/// Log all messages types.
		/// </summary>
		Verbose = 0,

		/// <summary>
		/// Only log informational messages, debug messages, warnings, errors, and critical (fatal) errors.
		/// </summary>
		Information = 1,

		/// <summary>
		/// Only log debug messages, warnings, errors, and critical (fatal) errors.
		/// </summary>
		Debug = 2,

		/// <summary>
		/// Only log warnings, errors, and critical (fatal) errors.
		/// </summary>
		Warning = 3,

		/// <summary>
		/// Only log errors and critical (fatal) errors.
		/// </summary>
		Error = 4,

		/// <summary>
		/// Only log critical (fatal) errors.
		/// </summary>
		Critical = 5
	}
}
