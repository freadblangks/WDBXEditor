using Acmil.Api.Managers.Interfaces;
using Acmil.Data.Contracts.Models.Achievements;
using Acmil.Data.Services.Interfaces;

namespace Acmil.Api.Managers
{
	public class AchievementManager : IAchievementManager
	{
		private IAchievementService _achievementService;

		public AchievementManager(IAchievementService achievementService)
		{
			_achievementService = achievementService;
		}

		public Achievement ReadAchievement(ushort achievementId)
		{
			return _achievementService.ReadAchievement(achievementId);
		}
	}
}
