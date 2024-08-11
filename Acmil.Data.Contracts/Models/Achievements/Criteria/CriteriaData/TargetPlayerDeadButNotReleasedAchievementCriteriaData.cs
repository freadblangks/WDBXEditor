using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Criteria.Enums;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria.CriteriaData
{
	/// <summary>
	/// Requirement data where a target player must be dead but not released.
	/// </summary>
	public class TargetPlayerDeadButNotReleasedAchievementCriteriaData : BaseAchievementCriteriaData
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaDataType.TargetPlayerDeadButNotReleased;

		/// <summary>
		/// True if the target player should be of the same faction.
		/// False if they should be of the opposite faction.
		/// </summary>
		[MySqlColumnName("value1")]
		public bool TargetIsSameFaction { get; set; }
	}
}
