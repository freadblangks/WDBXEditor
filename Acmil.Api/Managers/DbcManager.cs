using Acmil.Api.Managers.Interfaces;
using Acmil.Data.Services;
using Acmil.Data.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace Acmil.Api.Managers
{
	public class DbcManager : IDbcManager
	{
		IDbcService _dbcService;

		public DbcManager(string hostname, string username, SecureString password)
		{
			_dbcService = new DbcService(hostname, username, password);
		}

		public void LoadDbcIntoWorldDatabase(string dbcPath, string tableName = null)
		{
			_dbcService.LoadDbcIntoWorldDatabase(dbcPath, tableName);
		}
	}
}
