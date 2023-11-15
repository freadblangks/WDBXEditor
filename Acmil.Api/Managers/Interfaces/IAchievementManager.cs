using Acmil.Data.Contracts.Models.Achievements;

namespace Acmil.Api.Managers.Interfaces
{
	public interface IAchievementManager
	{
		public Achievement ReadAchievement(ushort achievementId);
	}
}
