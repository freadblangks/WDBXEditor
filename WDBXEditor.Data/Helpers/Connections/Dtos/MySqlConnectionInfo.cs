using MySql.Data.MySqlClient;
using System;

namespace WDBXEditor.Data.Helpers.Connections.Dtos
{
	/// <summary>
	/// A class representing parsed information for connecting to a MySQL database.
	/// </summary>
	[Serializable]
	public class MySqlConnectionInfo
	{
		private const string _CONNECTION_DATABASE_PROPERTY_NAME = "Database";

		/// <summary>
		/// The hostname of the server to connect to.
		/// </summary>
		public string Server { get; }

		/// <summary>
		/// The name of the database to connect to.
		/// </summary>
		public string Database { get; }

		/// <summary>
		/// The full connection string.
		/// </summary>
		public string FullConnectionString { get; }

		/// <summary>
		/// The full connection string without the database name.
		/// </summary>
		public string ServerOnlyConnectionString { get; }

		/// <summary>
		/// Initializes a new instance of <see cref="MySqlConnectionInfo"/>.
		/// </summary>
		/// <param name="connectionStringBuilder"><see cref="MySqlConnectionStringBuilder"/> instance fully populated with all information needed to connect.</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="connectionStringBuilder"/> is null.</exception>
		public MySqlConnectionInfo(MySqlConnectionStringBuilder connectionStringBuilder)
		{
			if (connectionStringBuilder == null)
			{
				throw new ArgumentNullException(nameof(connectionStringBuilder));
			}

			Server = connectionStringBuilder.Server;
			FullConnectionString = connectionStringBuilder.ToString();

			if (!string.IsNullOrWhiteSpace(connectionStringBuilder.Database))
			{
				Database = connectionStringBuilder.Database;
				connectionStringBuilder.Remove(_CONNECTION_DATABASE_PROPERTY_NAME);
			}
			else
			{
				Database = "";
			}

			ServerOnlyConnectionString = connectionStringBuilder.ToString();
		}
	}
}
