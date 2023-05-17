using Acmil.Common.Utility.Configuration.SettingsModels.Logging;
using Acmil.Common.Utility.Configuration.SettingsModels.MySql;

namespace Acmil.Common.Utility.Configuration.SettingsModels
{
	/// <summary>
	/// An object representing the contents of appsettings.json.
	/// </summary>
	public class AppSettings
	{
		/// <summary>
		/// An object containing settings relating to logging.
		/// </summary>
		/// <remarks>
		/// Represents the "Logging" property in appsettings.json.
		/// </remarks>
		public LoggingConfiguration Logging { get; set; }

		/// <summary>
		/// An object containing settings relating to MySQL.
		/// </summary>
		/// <remarks>
		/// Represents the "MySql" property in appsettings.json.
		/// </remarks>
		public MySqlConfiguration MySql { get; set; }
	}
}
