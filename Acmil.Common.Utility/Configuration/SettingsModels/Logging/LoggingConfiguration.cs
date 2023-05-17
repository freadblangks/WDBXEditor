namespace Acmil.Common.Utility.Configuration.SettingsModels.Logging
{
	/// <summary>
	/// An object representing the contents of the "Logging" property in appsettings.json.
	/// </summary>
	public class LoggingConfiguration
	{
		/// <summary>
		/// An object containing settings relating to logging level for the current session.
		/// </summary>
		/// <remarks>
		/// Represents the "Logging.LogLevel" property in appsettings.json.
		/// </remarks>
		public LogLevel LogLevel { get; set; }

		/// <summary>
		/// An object containing settings relating to writing log files. 
		/// </summary>
		/// <remarks>
		/// This represents the "Logging.LogLevel" property in appsettings.json.
		/// </remarks>
		public LogFileSettings LogFileSettings { get; set; }

		/// <summary>
		/// An object containing settings that configure which destinations logs will be written to.
		/// </summary>
		/// <remarks>
		/// This represents the "Logging.LoggingDestinations" property in appsettings.json.
		/// </remarks>
		public LoggingDestinations LoggingDestinations { get; set; }
	}
}
