using Acmil.Data.Contracts.Models.Achievements;

namespace Acmil.Data.Services.Interfaces
{
	public interface IAchievementService
	{
		Achievement ReadAchievement(ushort achievementId);
	}
}
