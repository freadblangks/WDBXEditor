using Acmil.Data.Contracts.Models.Achievements;
using Acmil.Data.Contracts.Models.Achievements.Criteria;
using Acmil.Data.Contracts.Models.Achievements.Rewards;

namespace Acmil.Data.Repositories.Interfaces
{
	/// <summary>
	/// Interface describing a repository class for interacting with Achievements.
	/// </summary>
	public interface IAchievementRepository
	{
		public Achievement CreateAchievement(Achievement achievement);

		public AchievementCategory CreateAchievementCategory(AchievementCategory category);

		/// <summary>
		/// Reads an Achievement from the database.
		/// </summary>
		/// <param name="achievementId">The ID of the Achievement.</param>
		/// <returns>An object representing the Achievement.</returns>
		public Achievement ReadAchievement(ushort achievementId);

		public IReadOnlyList<Achievement> ReadAchievements();

		/// <summary>
		/// Reads an Achievement category from the database.
		/// </summary>
		/// <param name="categoryId">The ID of the category.</param>
		/// <returns>The Achievement category with the specified ID.</returns>
		public AchievementCategory ReadAchievementCategory(ushort categoryId);

		public List<BaseAchievementCriteria> ReadAchievementCriteria(ushort achievementId);

		public BaseAchievementReward ReadAchievementReward(ushort achievementId, string localeCode = "");

		public Achievement UpdateAchievementCategory(ushort achievementId, short categoryId);

		public bool DeleteAchievement(ushort achievementId);

	}
}
