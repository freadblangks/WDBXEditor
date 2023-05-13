using WDBXEditor.Common.Utility.Configuration.SettingsModels;

namespace WDBXEditor.Common.Utility.Configuration.Interfaces
{
	/// <summary>
	/// Manager for interacting with the session configuration values, defined by appsettings.json.
	/// </summary>
	public interface IConfigurationManager
	{
		/// <summary>
		/// The current instance of <see cref="IConfigurationManager"/>. If one does not exist, it will be instantiated.
		/// </summary>
		/// <exception cref="InvalidOperationException">Thrown when an instance is instantiated from unmanaged code, such as during test execution.</exception>
		static IConfigurationManager Instance { get; }

		/// <summary>
		/// Gets an <see cref="AppSettings"/> object containing the configuration values defined in appsettings.json.
		/// </summary>
		/// <returns>An <see cref="AppSettings"/> object containing the configuration values defined in appsettings.json.</returns>
		AppSettings GetAppSettings();

		/// <summary>
		/// Gets the absolute path to the directory containing the currently running executable.
		/// </summary>
		/// <returns>The absolute path to the directory containing the currently running executable.</returns>
		/// <exception cref="InvalidOperationException">Thrown when called from unmanaged code, such as during test execution.</exception>
		string GetCurrentExecutableDirectoryPath();
	}
}
