using MySql.Data.MySqlClient;

namespace WDBXEditor.Data.Helpers.Connections.Interfaces
{
	/// <summary>
	/// Provider for obtaining <see cref="MySqlConnection"/> instances.
	/// </summary>
	public interface IMySqlConnectionProvider
	{
		/// <summary>
		/// Gets whether a connection obtained from this provider should have its database updated to match the
		/// associated <see cref="MySqlContext"/> instance's configured database when the connection is re-opened.
		/// </summary>
		bool ChangeDatabaseOnConnectionReOpen { get; }

		/// <summary>
		/// Gets the name of the database that the provider has been configured with.
		/// </summary>
		string DatabaseName { get; }

		/// <summary>
		/// Gets the server hostname that the provider has been configured with.
		/// </summary>
		string ServerName { get; }

		/// <summary>
		/// Gets a new instance of <see cref="MySqlConnection"/> based on the connection info the provider was configured with.
		/// </summary>
		/// <param name="includeDatabase">Whether the connection should be to a specified database on the target server.</param>
		/// <returns>A new instance of <see cref="MySqlConnection"/> based on the connection info the provider was configured with.</returns>
		MySqlConnection GetConnection(bool includeDatabase);
	}
}
