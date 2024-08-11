using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Enums;
using Acmil.Data.Contracts.Models.Battlegrounds.Enums;
using Acmil.Data.Contracts.Models.Maps.Enums;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria.Battlegrounds
{
	/// <summary>
	/// A requirement where the character must
	/// </summary>
	public class BattlegroundObjectiveCaptureAchievementCriteria : BaseAchievementCriteria
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaType.BattlegroundObjectiveCapture;

		/// <summary>
		/// An ID referring to a battleground objective that can be achieved.
		/// </summary>
		/// <remarks>
		/// For a list of supported values, see <see cref="BattlegroundObjective"/>.
		/// </remarks>
		[MySqlColumnName("Asset_Id")]
		[EnumType(typeof(BattlegroundObjective))]
		public uint ObjectiveId { get; set; }

		/// <summary>
		/// The number of times the objective must be completed.
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
