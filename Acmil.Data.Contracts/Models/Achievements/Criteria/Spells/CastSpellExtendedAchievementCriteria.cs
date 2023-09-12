using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Enums;
using Acmil.Data.Contracts.Models.General.Enums;
using Acmil.Data.Contracts.Types.Primitives;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria.Spells
{
	// TODO: Finish this guy.
	/// <summary>
	/// A requirement where the character must cast a particular spell but with support for more parameters..
	/// </summary>
	public class CastSpellExtendedAchievementCriteria : CastSpellAchievementCriteria
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaType.CastSpellExtended;

		/// <summary>
		/// A value indicating the type of condition that determine's the
		/// character's eligibility to meet the requirement.
		/// Defaults to 0 (None).
		/// </summary>
		/// <remarks>
		/// For supported values, see <see cref="ConditionType"/>.
		/// </remarks>
		[MySqlColumnName("Start_Event")]
		[EnumType(typeof(ConditionType))]
		public Int24 UnknownGuy { get; set; } = 0;

		// TODO: Determine whether this is what this is.
		[MySqlColumnName("Start_Asset")]
		public uint StartConditionMapId { get; set; }

		/// <summary>
		/// A value indicating the type of condition that determines
		/// that the character is no longer eligible to meet the requirement.
		/// Defaults to 0 (None).
		/// </summary>
		/// <remarks>
		/// For supported values, see <see cref="ConditionType"/>.
		/// </remarks>
		[MySqlColumnName("Fail_Event")]
		[EnumType(typeof(ConditionType))]
		public Int24 FailConditionTypeId { get; set; } = 0;

		// TODO: Figure out what this does.
		[MySqlColumnName("Fail_Asset")]
		public uint FailConditionMapId { get; set; }
	}
}
