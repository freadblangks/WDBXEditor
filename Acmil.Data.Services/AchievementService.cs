using Acmil.Data.Contracts.Models.Achievements;
using Acmil.Data.Repositories.Interfaces;
using Acmil.Data.Services.Interfaces;

namespace Acmil.Data.Services
{
	public class AchievementService : IAchievementService
	{
		private IAchievementRepository _achievementRepository;

		public AchievementService(IAchievementRepository achievementRepository)
		{
			_achievementRepository = achievementRepository;
		}

		public Achievement ReadAchievement(ushort achievementId)
		{
			// TODO: Add logic to make sure the right DBCs are loaded.
			var achievement = _achievementRepository.ReadAchievement(achievementId);
			if (achievement != null)
			{
				// TODO: See if we can rewrite this to be async.
				achievement.Category = _achievementRepository.ReadAchievementCategory(achievement.CategoryId);
				achievement.Criteria = _achievementRepository.ReadAchievementCriteria(achievement.AchievementId);
				achievement.Reward = _achievementRepository.ReadAchievementReward(achievement.AchievementId);
			}

			return achievement;
		}
	}
}
