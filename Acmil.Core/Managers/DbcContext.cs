using Acmil.Common.Utility.Interfaces;
using Acmil.Common.Utility.Logging.Interfaces;
using Acmil.Core.Managers.Interfaces;
using Acmil.Core.Storage;
using Acmil.Data.Contexts;
using Acmil.Data.Contracts.Connections;
using Acmil.Data.Helpers.Interfaces;

namespace Acmil.Core.Managers
{
	/// <summary>
	/// A context for interacting with DBC files.
	/// </summary>
	public class DbcContext : IDbcContext
	{
		private ILogger _logger;
		private IDbContextFactory _dbContextFactory;

		/// <summary>
		/// Initializes a new instance of <see cref="DbcContext"/>.
		/// </summary>
		/// <param name="utilityHelper">An implementation of <see cref="IUtilityHelper"/>.</param>
		/// <param name="dbContextFactory">An implementation of <see cref="IDbContext"/>.</param>
		public DbcContext(IUtilityHelper utilityHelper, IDbContextFactory dbContextFactory)
		{
			_logger = utilityHelper.Logger;
			_dbContextFactory = dbContextFactory;
		}

		public void LoadDbcIntoSql(MySqlConnectionInfo connectionInfo, string database, string dbcPath, string tableName = null)
		{
			var dbContext = _dbContextFactory.GetContext(connectionInfo, database);

			// TODO: Add proper error handling here.
			Database.LoadFiles(new string[] { dbcPath }).Result.FirstOrDefault();

			if (!Database.Entries.Any())
			{
				throw new IOException($"Failed to load file '{dbcPath}'");
			}

			_logger.LogInformation($"Successfully loaded file '{Path.GetFileName(dbcPath)}'.");
			DBEntry entry = Database.Entries[0];

			entry.ToSQLTable(dbContext, tableName);
		}
	}
}
