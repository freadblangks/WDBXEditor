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

		public DbcRepository(IDbcContext dbcContext)
		{
			_dbcContext = dbcContext;
		}

		public void LoadDbcIntoDatabase(MySqlConnectionInfo connectionInfo, string database, string dbcPath, string tableName = null)
		{
			_dbcContext.LoadDbcIntoSql(connectionInfo, database, dbcPath, tableName);
		}

		public void SaveDbcFromWorldDatabase(string dbcPath)
		{
			throw new System.NotImplementedException();
		}
	}
}
