using Acmil.Common.Utility.Interfaces;
using Acmil.Common.Utility.Logging.Interfaces;
using Acmil.Core.Managers.Interfaces;
using Acmil.Core.Storage;
using Acmil.Data.Contexts;
using System.IO;
using System.Linq;

namespace Acmil.Core.Managers
{
	/// <summary>
	/// A manager class for interacting with DBC files.
	/// </summary>
	public class DbcManager : IDbcManager
	{
		private ILogger _logger;
		private IDbContext _dbContext;

		/// <summary>
		/// Initializes a new instance of <see cref="DbcManager"/>.
		/// </summary>
		/// <param name="utilityHelper">An implementation of <see cref="IUtilityHelper"/>.</param>
		/// <param name="dbContext">An implementation of <see cref="IDbContext"/>.</param>
		public DbcManager(IUtilityHelper utilityHelper, IDbContext dbContext)
		{
			_logger = utilityHelper.GetLogger();
			_dbContext = dbContext;
		}

		public void LoadDbcIntoSql(string dbcPath, string tableName = null)
		{
			// TODO: Add proper error handling here.
			Database.LoadFiles(new string[] { dbcPath }).Result.FirstOrDefault();

			if (!Database.Entries.Any())
			{
				throw new IOException($"Failed to load file '{dbcPath}'");
			}

			_logger.LogInformation($"Successfully loaded file '{Path.GetFileName(dbcPath)}'.");
			DBEntry entry = Database.Entries[0];

			entry.ToSQLTable(_dbContext, tableName);
		}
	}
}
