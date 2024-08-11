using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Criteria.Enums;
using Acmil.Data.Contracts.Models.Spells.Enums;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria.CriteriaData
{
	/// <summary>
	/// Requirement data where the target needs to have a specific aura effect on them.
	/// </summary>
	public class TargetHasAuraEffectAchievementCriteriaData : BaseAchievementCriteria
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaDataType.TargetHasAuraEffect;

		/// <summary>
		/// Spell ID of the aura the target should have.
		/// </summary>
		/// <remarks>
		/// `ID` column from `Spell.dbc`.
		/// </remarks>
		[MySqlColumnName("value1")]
		public uint AuraSpellId { get; set; }

		/// <summary>
		/// The index of the spell effect to check for on the target.
		/// </summary>
		/// <remarks>
		/// Possible values are 0 (Effect 1), 1 (Effect 2), or 2 (Effect 3).
		/// </remarks>
		[MySqlColumnName("value2")]
		[EnumType(typeof(SpellEffectIndex))]
		[AllowEnumConversionOverride(false)]
		public byte SpellEffectIndex { get; set; }
	}
}
