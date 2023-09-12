using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Enums;
using Acmil.Data.Contracts.Models.Skills.Enums;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria.Skills
{
	/// <summary>
	/// A requirement where the character must reach a particular level in a Profession or Secondary Skill.
	/// </summary>
	public class LearnSkillLevelAchievementCriteria : BaseAchievementCriteria
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaType.LearnSkillLevel;

		/// <summary>
		/// The Skill ID of the Profession or Secondary Skill.
		/// </summary>
		/// <remarks>
		/// For a list of supported values, see <see cref="ProfessionOrSecondarySkill"/>.
		/// </remarks>
		[MySqlColumnName("Asset_Id")]
		[EnumType(typeof(ProfessionOrSecondarySkill))]
		[AllowEnumConversionOverride(true)]
		public uint SkillId { get; set; }

		/// <summary>
		/// The required rank for the Profession or Secondary Skill.
		/// </summary>
		/// <remarks>
		/// For a list of supported values, see <see cref="ProfessionOrSecondarySkillRank"/>.
		/// </remarks>
		[MySqlColumnName("Quantity")]
		[EnumType(typeof(ProfessionOrSecondarySkillRank))]
		[AllowEnumConversionOverride(true)]
		public uint SkillRank { get; set; }
	}
}
