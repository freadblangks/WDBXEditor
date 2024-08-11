using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Criteria.Enums;
using Acmil.Data.Contracts.Types.Primitives;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria.CriteriaData
{
	/// <summary>
	/// Requirement data where a specific creature needs to be targeted.
	/// </summary>
	public class CreatureIsTargetAchievementCriteriaData : BaseAchievementCriteriaData
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaDataType.CreatureIsTarget;

		/// <summary>
		/// The Creature Template entry for the creature that should be the target.
		/// </summary>
		[MySqlColumnName("value1")]
		public UInt24 CreatureTemplateEntry { get; set; }
	}
}
