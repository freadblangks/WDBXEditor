using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Criteria.Enums;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria.CriteriaData
{
	/// <summary>
	/// Requirement data where the target needs to be at or above a certain level.
	/// </summary>
	public class TargetIsAtOrAboveLevelAchievementCriteriaData : BaseAchievementCriteriaData
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaDataType.TargetIsAtOrAboveLevel;

		/// <summary>
		/// The level the target needs to be at or above.
		/// </summary>
		[MySqlColumnName("value1")]
		public uint Level { get; set; }
	}
}
