namespace Acmil.Common.Utility.Configuration.SettingsModels.Logging
{
	/// <summary>
	/// An object representing the contents of the "Logging.LogFileSettings" property in appsettings.json.
	/// </summary>
	public class LogFileSettings
	{
		/// <summary>
		/// The path to the directory where log files should be written to.
		/// </summary>
		public string Directory { get; set; } = "";

		/// <summary>
		/// The string template to use for naming log files.
		/// </summary>
		public string FileNameTemplate { get; set; } = "acmil-{timestamp}.log";
	}
}
