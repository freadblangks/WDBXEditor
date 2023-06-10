using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Acmil.Common
{
	/// <summary>
	/// Class for creating and managing connections to a MySQL server instance.
	/// </summary>
	public static class MySqlConnectionManager
	{
		private static Dictionary<string, MySqlConnection> _connections = new Dictionary<string, MySqlConnection>();

		/// <summary>
		/// Gets a connection to the specified database. If one doesn't already exist, it will be created.
		/// </summary>
		/// <param name="databaseName">The name of the database to connect to.</param>
		/// <returns>A connection object for the database.</returns>
		public static MySqlConnection GetConnection(string databaseName)
		{
			if (!_connections.ContainsKey(databaseName))
			{
				_connections[databaseName] = CreateNewConnection(databaseName);
			}

			return _connections[databaseName];
		}

		/// <summary>
		/// Closes all non-closed connections managed by the class.
		/// </summary>
		public static void CloseAllConnections()
		{
			foreach (KeyValuePair<string, MySqlConnection> connection in _connections)
			{
				if (connection.Value.State != System.Data.ConnectionState.Closed)
				{
					connection.Value.Close();
				}
			}
		}

		private static MySqlConnection CreateNewConnection(string databaseName)
		{
			var connectionStringBuilder = new MySqlConnectionStringBuilder();
			connectionStringBuilder.Server = ConfigurationManager.AppSettings["Hostname"];
			connectionStringBuilder.UserID = ConfigurationManager.AppSettings["User"];
			connectionStringBuilder.Password = ConfigurationManager.AppSettings["Password"];
			connectionStringBuilder.Database = databaseName;

			var connection = new MySqlConnection()
			{
				ConnectionString = connectionStringBuilder.ConnectionString
			};

			return connection;
		}
	}
}
