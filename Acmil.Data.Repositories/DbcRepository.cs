using Acmil.Core.Contexts.Interfaces;
using Acmil.Data.Contracts.Connections;
using Acmil.Data.Repositories.Interfaces;

namespace Acmil.Data.Repositories
{
	/// <summary>
	/// Repository class for interacting with DBC files.
	/// </summary>
	public class DbcRepository : IDbcRepository
	{
		private IDbcContext _dbcContext;

		/// <summary>
		/// Initializes an instance of <see cref="DbcRepository"/>.
		/// </summary>
		/// <param name="dbcContext">An implementation of <see cref="IDbcContext"/> for interacting with DBCs.</param>
		public DbcRepository(IDbcContext dbcContext)
		{
			_dbcContext = dbcContext;
		}

		public void LoadDbcIntoDatabase(MySqlConnectionInfo connectionInfo, string database, string dbcPath, string tableName = null)
		{
			_dbcContext.LoadDbcIntoSql(connectionInfo, database, dbcPath, tableName);
		}

		public void WriteToDbcFromDatabase(MySqlConnectionInfo connectionInfo, string database, string dbcPath, string tableName)
		{
			throw new NotImplementedException();
		}

		public void DeleteDbcFromDatabase(MySqlConnectionInfo connectionInfo, string database, string tableName)
		{
			throw new NotImplementedException();
		}

		public void EnsureDbcIsLoadedAndTracked(MySqlConnectionInfo connectionInfo, string database, string dbcName)
		{
			throw new NotImplementedException();
		}

		public void HasDbcDataBeenModified(MySqlConnectionInfo conectionInfo, string database, string dbcName)
		{
			throw new NotImplementedException();
		}

		public void IsDbcLoadedAndTracked(MySqlConnectionInfo connectionInfo, string database, string dbcName)
		{
			throw new NotImplementedException();
		}

		public void CleanAllDbcAndTrackingTables(MySqlConnectionInfo connectionInfo)
		{
			throw new NotImplementedException();
		}
	}
}
