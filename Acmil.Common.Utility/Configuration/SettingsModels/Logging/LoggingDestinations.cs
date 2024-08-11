namespace Acmil.Common.Utility.Configuration.SettingsModels.Logging
{
	/// <summary>
	/// An object representing the contents of the "logging.destinations" property in config.json.
	/// </summary>
	public class LoggingDestinations
	{
		/// <summary>
		/// Whether logging is configured to write to the console.
		/// </summary>
		/// <remarks>
		/// Represents the "logging.destinations.console" property in config.json.
		/// </remarks>
		public bool Console { get; set; } = true;

		/// <summary>
		/// Whether logging is configured to write to a log file.
		/// </summary>
		/// <remarks>
		/// Represents the "logging.destinations.file" property in config.json.
		/// </remarks>
		public bool File { get; set; } = false;
	}
}
