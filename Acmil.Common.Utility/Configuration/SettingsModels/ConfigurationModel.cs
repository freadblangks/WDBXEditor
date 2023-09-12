using Acmil.Common.Utility.Configuration.SettingsModels.Locale;
using Acmil.Common.Utility.Configuration.SettingsModels.Logging;
using Acmil.Common.Utility.Configuration.SettingsModels.MySql;

namespace Acmil.Common.Utility.Configuration.SettingsModels
{
	/// <summary>
	/// An object representing the contents of config.json.
	/// </summary>
	public class ConfigurationModel
	{
		/// <summary>
		/// An object containing settings relating to logging.
		/// </summary>
		/// <remarks>
		/// Represents the "logging" property in config.json.
		/// </remarks>
		public LoggingConfiguration Logging { get; set; }

		/// <summary>
		/// An object containing settings relating to MySQL.
		/// </summary>
		/// <remarks>
		/// Represents the "mySql" property in config.json.
		/// </remarks>
		public MySqlConfiguration MySql { get; set; }

		/// <summary>
		/// An object containing settings relating to locale.
		/// </summary>
		/// <remarks>
		/// Represents the "locale" property in config.json.
		/// </remarks>
		public LocaleConfiguration Locale { get; set; }
	}
}
