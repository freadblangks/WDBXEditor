using Acmil.Common.Utility.Interfaces;
using Acmil.Data.Contracts.Models.Achievements;
using Acmil.Data.Helpers.Interfaces;
using Acmil.Data.Repositories.Interfaces;
using MySql.Data.MySqlClient;

namespace Acmil.Data.Repositories
{
	public class AchievementRepository : IAchievementRepository
	{
		private IUtilityHelper _utilityHelper;
		private IDbContextFactory _dbContextFactory;

		/// <summary>
		/// Initializes a new instance of <see cref="AchievementRepository"/>.
		/// </summary>
		/// <param name="utilityHelper">An implementation of <see cref="IUtilityHelper"/>.</param>
		/// <param name="dbContextFactory">An implementation of <see cref="IDbContextFactory"/> for interacting with SQL.</param>
		public AchievementRepository(IUtilityHelper utilityHelper, IDbContextFactory dbContextFactory)
		{
			_utilityHelper = utilityHelper;
			_dbContextFactory = dbContextFactory;
		}

		public Achievement ReadAchievement(ushort achievementId)
		{
			throw new NotImplementedException();
		}

		private static Achievement ConvertReadResponseToAchievement(MySqlDataReader dataReader)
		{
			var achievement = new Achievement()
			{
				AchievementId = (ushort)dataReader["ID"]
			};

			return achievement;
		}
	}
}
