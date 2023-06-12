using Acmil.Data.Helpers.Connections.Dtos;
using Acmil.Data.Helpers.Connections.Interfaces;
using MySql.Data.MySqlClient;
using System;

namespace Acmil.Data.Helpers.Connections
{
	/// <summary>
	/// Provider class for obtaining <see cref="MySqlConnection"/> instances.
	/// </summary>
	public class MySqlConnectionProvider : IMySqlConnectionProvider
	{
		private MySqlConnectionInfoInternal _connectionInfo;
		private bool _useServerConnections;

		/// <summary>
		/// Initializes a new instance of <see cref="MySqlConnectionProvider"/>.
		/// </summary>
		/// <param name="connectionInfo">Fully populated <see cref="MySqlConnectioninfo"/> instance to configure the provider with.</param>
		/// <param name="useServerConnections">Whether this instance of <see cref="MySqlConnectionProvider"/> should provide server-only (non-database) connections.</param>
		internal MySqlConnectionProvider(MySqlConnectionInfoInternal connectionInfo, bool useServerConnections)
		{
			_connectionInfo = connectionInfo ?? throw new ArgumentNullException(nameof(connectionInfo));
			_useServerConnections = useServerConnections;
		}

		public bool ChangeDatabaseOnConnectionReOpen => _useServerConnections && !string.IsNullOrWhiteSpace(_connectionInfo.Database);

		public string DatabaseName => _connectionInfo.Database;

		public string ServerName => _connectionInfo.Server;

		public MySqlConnection GetConnection(bool includeDatabase)
		{
			string connectionString = _connectionInfo.FullConnectionString;
			if (_useServerConnections && !includeDatabase)
			{
				connectionString = _connectionInfo.ServerOnlyConnectionString;
			}

			return new MySqlConnection(connectionString);
		}
	}
}
