using Acmil.Common.Utility.Connections;
using Acmil.Common.Utility.Exceptions;

namespace Acmil.Common.Utility.Configuration.Interfaces
{
	/// <summary>
	/// Manager for interacting with the session configuration values, defined by appsettings.json.
	/// </summary>
	public interface IConfigurationManager
	{
		/// <summary>
		/// Gets the currently configured <see cref="MySqlConnectionInfo"/> object for connecting to SQL.
		/// </summary>
		/// <returns>The currently configured <see cref="MySqlConnectionInfo"/> object.</returns>
		/// Yes I'm aware this probably be on the implementation, but this will never be referenced that way.
		/// <exception cref="NoConnectionInfoSetException">
		/// Thrown when this method is called without a connection info object having been set.
		/// </exception>
		public MySqlConnectionInfo GetConnectionInfo();

		/// <summary>
		/// Sets the connection info object to be used for connecting to SQL.
		/// </summary>
		/// <param name="connectionInfo">A populated <see cref="MySqlConnectionInfo"/> instance for connecting to SQL.</param>
		public void SetConnectionInfo(MySqlConnectionInfo connectionInfo);

		/// <summary>
		/// Gets an <see cref="Configuration"/> object containing the configuration values defined in appsettings.json.
		/// </summary>
		/// <returns>An <see cref="Configuration"/> object containing the configuration values defined in appsettings.json.</returns>
		SettingsModels.ConfigurationModel GetConfiguration();

		
	}
}
