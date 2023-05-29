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

		public DbcService(string hostname, string username, SecureString password)
		{
			_dbcRepository = new DbcRepository(hostname, username, password, new MySqlDbContextFactory());
		}

		public void LoadDbcIntoWorldDatabase(string dbcPath, string tableName = null)
		{
			_dbcRepository.LoadDbcIntoWorldDatabase(dbcPath, tableName);
		}
	}
}
