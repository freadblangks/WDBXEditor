using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using WDBXEditor.Common;

namespace Acmil.Api.Helpers
{
	/// <summary>
	/// Helper class for managing MySQL connections.
	/// </summary>
	public class MySqlConnectionHelper
	{
		/// <summary>
		/// Gets a connection to the specified database. If one doesn't already exist, it will be created.
		/// </summary>
		/// <param name="databaseName">The name of the database to connect to.</param>
		/// <returns>A connection object for the database.</returns>
		public MySqlConnection GetConnection(string databaseName) => MySqlConnectionManager.GetConnection(databaseName);
	}
}
