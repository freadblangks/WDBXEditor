using Acmil.Data.Contracts.Connections;
using Acmil.Data.Helpers;
using Acmil.Data.Repositories;
using Acmil.Data.Repositories.Interfaces;
using Acmil.Data.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

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
	}
}
