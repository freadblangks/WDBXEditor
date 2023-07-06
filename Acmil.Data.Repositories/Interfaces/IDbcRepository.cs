using Acmil.Data.Contracts.Connections;

namespace Acmil.Data.Repositories.Interfaces
{
	/// <summary>
	/// Interface describing a repository class for interacting with DBC files.
	/// </summary>
	public interface IDbcRepository
	{
		/// <summary>
		/// Loads a DBC file into the world database as a new table.
		/// </summary>
		/// <param name="connectionInfo">A <see cref="MySqlConnectionInfo"/> object for connecting to SQL.</param>
		/// <param name="database">The name of the database to load the DBC file into.</param>
		/// <param name="dbcPath">The path to the DBC file.</param>
		/// <param name="tableName">The name of the table to load the DBC into.</param>
		public void LoadDbcIntoDatabase(MySqlConnectionInfo connectionInfo, string database, string dbcPath, string tableName);

		/// <summary>
		/// Saves the contents of a DBC table in SQL to its corresponding DBC file.
		/// </summary>
		/// <param name="dbcPath">The path to the DBC file.</param>
		public void SaveDbcFromWorldDatabase(string dbcPath);
	}
}
