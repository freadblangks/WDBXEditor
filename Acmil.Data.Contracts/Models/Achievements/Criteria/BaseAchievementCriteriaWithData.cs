using Acmil.Data.Contracts.Models.Achievements.Criteria.CriteriaData;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria
{
	public abstract class BaseAchievementCriteriaWithData : BaseAchievementCriteria
	{
		public abstract BaseAchievementCriteriaData Data { get; set; }
	}
}
