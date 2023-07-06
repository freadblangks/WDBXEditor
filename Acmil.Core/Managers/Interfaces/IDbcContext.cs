using Acmil.Data.Contracts.Connections;

namespace Acmil.Core.Managers.Interfaces
{
	/// <summary>
	/// Interface describing a manager class for interacting with DBC files.
	/// </summary>
	public interface IDbcContext
	{
		/// <summary>
		/// Loads a DBC into a new SQL table.
		/// </summary>
		/// <param name="connectionInfo">A <see cref="MySqlConnectionInfo"/> object for connecting to SQL.</param>
		/// <param name="database">The name of the database to load the DBC into.</param>
		/// <param name="dbcPath">The path to the DBC file to load.</param>
		/// <param name="tableName">The name the table should be created with. Defaults to 'db_{dbc_name}_{build_number}.</param>
		public void LoadDbcIntoSql(MySqlConnectionInfo connectionInfo, string database, string dbcPath, string tableName = null);
	}
}
