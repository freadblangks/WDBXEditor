using Acmil.Common.Utility;
using Acmil.Core.Managers;
using Acmil.Core.Managers.Interfaces;
using Acmil.Data.Constants;
using Acmil.Data.Contexts;
using Acmil.Data.Helpers.Interfaces;
using Acmil.Data.Repositories.Interfaces;
using System.Security;

namespace Acmil.Data.Repositories
{
	/// <summary>
	/// Repository class for interacting with DBC files.
	/// </summary>
	public class DbcRepository : IDbcRepository
	{
		private IDbContext _worldContext;

		private IDbcManager _worldDbcManager;

		// TODO: Refactor this hacky mess to be DI-friendly.
		public DbcRepository(string hostname, string username, SecureString password, IDbContextFactory dbContextFactory)
		{
			_worldContext = dbContextFactory.GetContext(
				hostname,
				username,
				password,
				AzerothCoreDatabaseConstants.DEFAULT_WORLD_DATABASE_NAME
			);

			_worldDbcManager = new DbcManager(new UtilityHelper(), _worldContext);
		}

		public void LoadDbcIntoWorldDatabase(string dbcPath, string tableName = null)
		{
			_worldDbcManager.LoadDbcIntoSql(dbcPath, tableName);
		}

		public void SaveDbcFromWorldDatabase(string dbcPath)
		{
			throw new System.NotImplementedException();
		}
	}
}
