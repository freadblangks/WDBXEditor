namespace Acmil.Common.Utility.Configuration.SettingsModels.Logging
{
	/// <summary>
	/// An object representing the contents of the "Logging.LoggingDestinations" property in appsettings.json.
	/// </summary>
	public class LoggingDestinations
	{
		/// <summary>
		/// Whether logging is configured to write to the console.
		/// </summary>
		public bool Console { get; set; } = true;

		/// <summary>
		/// Whether logging is configured to write to a log file.
		/// </summary>
		public bool File { get; set; } = false;
	}
}
