namespace Acmil.Common.Utility.Configuration.SettingsModels.Logging
{
	/// <summary>
	/// An object representing the contents of the "Logging.LogLevel" property in appsettings.json.
	/// </summary>
	public class LogLevel
	{
		/// <summary>
		/// The default logging level to use if no value has been provided for <see cref="Override"/>.
		/// </summary>
		public string Default { get; set; }

		/// <summary>
		/// The logging level to use in place of the one specified by <see cref="Default"/>.
		/// </summary>
		public string Override { get; set; }
	}
}
