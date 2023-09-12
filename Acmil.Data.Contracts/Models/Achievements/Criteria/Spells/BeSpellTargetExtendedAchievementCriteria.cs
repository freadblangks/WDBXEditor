using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Enums;
using Acmil.Data.Contracts.Models.General.Enums;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria.Spells
{
	// TODO: Finish figuring out what all the fields do on this.
	/// <summary>
	/// A requirement where the character must have a spell cast on them but with support for more parameters.
	/// </summary>
	public class BeSpellTargetExtendedAchievementCriteria : BeSpellTargetAchievementCriteria
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaType.BeSpellTargetExtended;

		/// <summary>
		/// A numerical field with an unknown purpose.
		/// Might refer to `ConditionTypeOrReference` in `conditions` table.
		/// </summary>
		[MySqlColumnName("Start_Event")]
		[EnumType(typeof(ConditionType))]
		public uint ConditionTypeIdPossibly { get; set; }

		/// <summary>
		/// A numerical field with an unknown purpose. Might be used for flags.
		/// </summary>
		[MySqlColumnName("Start_Asset")]
		public uint UnknownStartAssetPossiblyFlags { get; set; }
	}
}
