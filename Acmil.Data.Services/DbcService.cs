using Acmil.Common.Utility.Connections;
using Acmil.Data.Repositories.Interfaces;
using Acmil.Data.Services.Interfaces;

namespace Acmil.Data.Services
{
	public class DbcService : IDbcService
	{
		private IDbcRepository _dbcRepository;

		public DbcService(IDbcRepository dbcRepository)
		{
			_dbcRepository = dbcRepository;
		}

		public void LoadDbcIntoDatabase(MySqlConnectionInfo connectionInfo, string database, string dbcPath, string tableName = null)
		{
			_dbcRepository.LoadDbcIntoDatabase(connectionInfo, database, dbcPath, tableName);
		}

		public void WriteDbcDataFromDatabase(MySqlConnectionInfo connectionInfo, string database, string dbcPath, string tableName)
		{
			string dbcName = Path.GetFileNameWithoutExtension(dbcPath);
			string dbcDirectoryPath = Path.GetDirectoryName(dbcPath);
			_dbcRepository.WriteDbcDataFromDatabase(connectionInfo, database, dbcDirectoryPath, dbcName, tableName);
		}
	}
}
