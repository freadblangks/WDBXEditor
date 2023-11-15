using Acmil.Data.Contracts.Models.Achievements.Criteria;

namespace Acmil.Data.Repositories.Helpers.Interfaces
{
	public interface IAchievementCriteriaFactory
	{
		public BaseAchievementCriteria GetAchievementCriteriaInstance(byte typeId, uint assetId = 0, uint quantity = 0, uint startEvent = 0, uint startAsset = 0, uint failEvent = 0, uint failAsset = 0);
	}
}
