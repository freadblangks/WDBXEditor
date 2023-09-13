using Acmil.Common.Utility.Connections;

namespace Acmil.Data.Services.Interfaces
{
	public interface IDbcService
	{
		public void LoadDbcIntoDatabase(MySqlConnectionInfo connectionInfo, string database, string dbcPath, string tableName = null);

		public void WriteDbcDataFromDatabase(MySqlConnectionInfo connectionInfo, string database, string dbcPath, string tableName);
	}
}
