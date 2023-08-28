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

		public void WriteToDbcFromDatabase(MySqlConnectionInfo connectionInfo, string database, string dbcPath, string tableName);

		public void DeleteDbcFromDatabase(MySqlConnectionInfo connectionInfo, string database, string tableName);

		/// <summary>
		/// Ensures that the specified DBC has been loaded into a tracked table in the database.
		/// </summary>
		/// <param name="connectionInfo">A <see cref="MySqlConnectionInfo"/> object for connecting to SQL.</param>
		/// <param name="database">The name of the database to check.</param>
		/// <param name="dbcName">The name of the DBC (no file extension).</param>
		public void EnsureDbcIsLoadedAndTracked(MySqlConnectionInfo connectionInfo, string database, string dbcName);

		public void IsDbcLoadedAndTracked(MySqlConnectionInfo connectionInfo, string database, string dbcName);

		public void HasDbcDataBeenModified(MySqlConnectionInfo conectionInfo, string database, string dbcName);

		public void CleanAllDbcAndTrackingTables(MySqlConnectionInfo connectionInfo);
	}
}
