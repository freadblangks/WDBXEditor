namespace WDBXEditor.Common.Utility.Configuration.SettingsModels.Logging
{
	/// <summary>
	/// An object representing the contents of the "Logging.LoggingDestinations" property in appsettings.json.
	/// </summary>
	public class LoggingDestinations
	{
		/// <summary>
		/// Whether logging is configured to write to the console.
		/// </summary>
		/// TODO: Figure out what we mean by this.
		public bool Console { get; set; }

		/// <summary>
		/// Whether logging is configured to write to a log file.
		/// </summary>
		public bool File { get; set; }
	}
}
