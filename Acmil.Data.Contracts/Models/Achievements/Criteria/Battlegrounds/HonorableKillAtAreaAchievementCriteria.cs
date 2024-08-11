using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Enums;
using Acmil.Data.Contracts.Models.Battlegrounds.Enums;
using Acmil.Data.Contracts.Models.Maps.Enums;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria.Battlegrounds
{
	// TODO: Test if this works outside battlegrounds.
	/// <summary>
	/// A requirement where the character must get a number of honorable kills in a particular area.
	/// </summary>
	public class HonorableKillAtAreaAchievementCriteria : BaseAchievementCriteria
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaType.HonorableKillAtArea;

		/// <summary>
		/// The ID of the Area where the honorable kill needs to take place.
		/// </summary>
		/// <remarks>
		/// Taken from the `ID` column of `areatable.dbc`.
		/// </remarks>
		[MySqlColumnName("Asset_Id")]
		public uint AreaId { get; set; }

		/// <summary>
		/// The number of honorable kills required.
		/// </summary>
		[MySqlColumnName("Quantity")]
		public uint Count { get; set; }

		/// <summary>
		/// A value indicating the status the battleground instance must be in
		/// for character to be eligiblefor meeting the requirement.
		/// </summary>
		/// <remarks>
		/// For a list of supported values, see <see cref="BattlegroundStatus"/>.
		/// </remarks>
		[MySqlColumnName("Start_Event")]
		[EnumType(typeof(BattlegroundStatus))]
		public uint BattlegroundRequiredStatus { get; set; }

		/// <summary>
		/// The ID of the battleground map where the requirement must be met.
		/// </summary>
		/// <remarks>
		/// For a list of supported values, see <see cref="InstanceMap"/>.
		/// </remarks>
		[MySqlColumnName("Start_Asset")]
		[EnumType(typeof(InstanceMap))]
		public uint BattlegroundRequiredMapId { get; set; }

		/// <summary>
		/// A value indicating the battleground status that, when no longer true
		/// for the current battleground instance, will cause the requirement to be failed.
		/// </summary>
		/// <remarks>
		/// For a list of supported values, see <see cref="BattlegroundStatus"/>.
		/// </remarks>
		[MySqlColumnName("Fail_Event")]
		[EnumType(typeof(BattlegroundStatus))]
		public uint BattlegroundFailureStatus { get; set; }

		/// <summary>
		/// The ID of the battleground map where the requirement must be met.
		/// </summary>
		/// <remarks>
		/// For a list of supported values, see <see cref="InstanceMap"/>.
		/// </remarks>
		[MySqlColumnName("Fail_Asset")]
		[EnumType(typeof(InstanceMap))]
		public uint BattlegroundFailureMapId { get; set; }
	}
}
