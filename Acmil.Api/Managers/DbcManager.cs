using Acmil.Api.Managers.Interfaces;
using Acmil.Data.Contracts.Connections;
using Acmil.Data.Services.Interfaces;

namespace Acmil.Api.Managers
{
	public class DbcManager : IDbcManager
	{
		IDbcService _dbcService;

		public DbcManager(IDbcService dbcService)
		{
			_dbcService = dbcService;
		}

		public void LoadDbcIntoDatabase(MySqlConnectionInfo connectionInfo, string database, string dbcPath, string tableName = null)
		{
			_dbcService.LoadDbcIntoDatabase(connectionInfo, database, dbcPath, tableName);
		}

		public void WriteDbcDataFromDatabase(MySqlConnectionInfo connectionInfo, string database, string dbcPath, string tableName)
		{
			_dbcService.WriteDbcDataFromDatabase(connectionInfo, database, dbcPath, tableName);
		}
	}
}
