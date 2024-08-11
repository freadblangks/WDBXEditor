using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Enums;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria.Spells
{
	/// <summary>
	/// A requirement where the character must cast a particular spell.
	/// </summary>
	public class CastSpellAchievementCriteria : BaseAchievementCriteria
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaType.CastSpell;

		/// <summary>
		/// The ID of the spell the character needs to cast.
		/// </summary>
		[MySqlColumnName("Asset_Id")]
		public uint SpellId { get; set; }

		/// <summary>
		/// The number of times the spell needs to be cast.
		/// </summary>
		[MySqlColumnName("Quantity")]
		public uint Count { get; set; }
	}
}
