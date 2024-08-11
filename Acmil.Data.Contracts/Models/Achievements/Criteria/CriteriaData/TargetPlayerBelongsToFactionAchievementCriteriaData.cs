using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Criteria.Enums;
using Acmil.Data.Contracts.Models.Reputations.Enums;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria.CriteriaData
{
	/// <summary>
	/// Requirement data where the target player needs to belong to a specific faction (Alliance/Horde).
	/// </summary>
	public class TargetPlayerBelongsToFactionAchievementCriteriaData : BaseAchievementCriteriaData
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaDataType.TargetPlayerBelongsToFaction;

		/// <summary>
		/// The ID of the player faction the target player should belong to.
		/// </summary>
		/// <remarks>
		/// This uses the old faction IDs from the `ID` column of `Faction.dbc`.<br/>
		/// 67 : Horde<br/>
		/// 469 : Alliance
		/// </remarks>
		[MySqlColumnName("value1")]
		[EnumType(typeof(PlayerFactionOld))]
		[AllowEnumConversionOverride(false)]
		public uint FactionId { get; set; }
	}
}
