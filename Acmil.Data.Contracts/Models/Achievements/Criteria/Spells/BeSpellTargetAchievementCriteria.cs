using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Enums;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria.Spells
{
	/// <summary>
	/// A requirement where the character must have a spell cast on them.
	/// </summary>
	public class BeSpellTargetAchievementCriteria : BaseAchievementCriteria
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaType.BeSpellTarget;

		/// <summary>
		/// The ID of the spell that needs to be cast on the character.
		/// </summary>
		/// <remarks>
		/// Taken from `Spell.dbc`.
		/// </remarks>
		[MySqlColumnName("Asset_Id")]
		public uint SpellId { get; set; }

		/// <summary>
		/// The number of times the spell must be cast on the character.
		/// </summary>
		/// <remarks>
		/// Defaults to 1.
		/// </remarks>
		[MySqlColumnName("Quantity")]
		public uint Count { get; set; } = 1;
	}
}
