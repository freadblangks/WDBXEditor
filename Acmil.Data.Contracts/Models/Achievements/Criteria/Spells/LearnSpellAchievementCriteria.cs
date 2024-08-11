using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Enums;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria.Spells
{
	/// <summary>
	/// A requirement where the character must learn a particular spell.
	/// </summary>
	/// <remarks>
	/// This is often used for pet and mount Achievements.
	/// </remarks>
	public class LearnSpellAchievementCriteria : BaseAchievementCriteria
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaType.LearnSpell;

		/// <summary>
		/// The ID of the spell the character must learn.
		/// </summary>
		/// <remarks>
		/// Taken from the `ID` column of `spell.dbc`.
		/// </remarks>
		[MySqlColumnName("Asset_Id")]
		public uint SpellId { get; set; }
	}
}
