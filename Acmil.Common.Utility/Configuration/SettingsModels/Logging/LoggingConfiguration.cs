using Acmil.Common.Utility.Logging.Enums;

namespace Acmil.Common.Utility.Configuration.SettingsModels.Logging
{
	/// <summary>
	/// An object representing the contents of the "logging" property in config.json.
	/// </summary>
	public class LoggingConfiguration
	{
		/// <summary>
		/// The logging level for the current session.
		/// Defaults to <see cref="LoggingLevel.Warning"/>.
		/// </summary>
		/// <remarks>
		/// Represents the "logging.level" property in config.json.
		/// </remarks>
		public string Level { get; set; } = LoggingLevel.Warning.ToString();

		/// <summary>
		/// An object containing settings relating to writing log files. 
		/// </summary>
		/// <remarks>
		/// This represents the "logging.logFileSettings" property in config.json.
		/// </remarks>
		public LogFileSettings LogFileSettings { get; set; } = new LogFileSettings();

		/// <summary>
		/// An object containing settings that configure which destinations logs will be written to.
		/// </summary>
		/// <remarks>
		/// This represents the "logging.destinations" property in config.json.
		/// </remarks>
		public LoggingDestinations Destinations { get; set; } = new LoggingDestinations();
	}
}
