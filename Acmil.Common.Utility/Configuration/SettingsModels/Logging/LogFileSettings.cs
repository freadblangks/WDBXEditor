namespace Acmil.Common.Utility.Configuration.SettingsModels.Logging
{
	/// <summary>
	/// An object representing the contents of the "logging.logFileSettings" property in config.json.
	/// </summary>
	public class LogFileSettings
	{
		/// <summary>
		/// The path to the directory where log files should be written to.
		/// </summary>
		/// <remarks>
		/// Represents the "logging.logFileSettings.directory" property in config.json.
		/// </remarks>
		public string Directory { get; set; } = "";

		/// <summary>
		/// The string template to use for naming log files.
		/// </summary>
		/// <remarks>
		/// Represents the "logging.logFileSettings.fileNameTemplate" property in config.json.
		/// </remarks>
		public string FileNameTemplate { get; set; } = "acmil-{timestamp}.log";
	}
}
