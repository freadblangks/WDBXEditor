using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Enums;
using Acmil.Data.Contracts.Models.Skills.Enums;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria.Skills
{
	// TODO: Figure out how this is different from LEARN_SKILL_LEVEL
	/// <summary>
	/// A requirement where the character must reach a certain level in a particular skill.
	/// </summary>
	public class ReachSkillLevelAchievementCriteria : BaseAchievementCriteria
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaType.ReachSkillLevel;

		// TODO: Add enum for weapon skills.
		[MySqlColumnName("Asset_Id")]
		[EnumType(typeof(ProfessionOrSecondarySkill))]
		public uint SkillId { get; set; }

		/// <summary>
		/// The level to reach in the skill.
		/// </summary>
		[MySqlColumnName("Quantity")]
		public uint Level { get; set; }
	}
}
