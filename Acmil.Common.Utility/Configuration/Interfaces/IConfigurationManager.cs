namespace Acmil.Common.Utility.Configuration.Interfaces
{
	/// <summary>
	/// Manager for interacting with the session configuration values, defined by appsettings.json.
	/// </summary>
	public interface IConfigurationManager
	{
		/// <summary>
		/// Gets an <see cref="Configuration"/> object containing the configuration values defined in appsettings.json.
		/// </summary>
		/// <returns>An <see cref="Configuration"/> object containing the configuration values defined in appsettings.json.</returns>
		SettingsModels.ConfigurationModel GetConfiguration();
	}
}
