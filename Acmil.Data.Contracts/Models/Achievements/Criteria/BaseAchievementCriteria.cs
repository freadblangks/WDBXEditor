using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Criteria.Enums;
using Acmil.Data.Contracts.Models.Achievements.Enums;
using Acmil.Data.Contracts.Types.Primitives;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria
{
	/// <summary>
	/// Base class for AchievementCriteria classes.
	/// </summary>
	public abstract class BaseAchievementCriteria
	{
		// NOTE: If this breaks, it was a ushort before.
		/// <summary>
		/// An ID uniquely idenitifying the criteria.
		/// </summary>
		[MySqlColumnName("ID")]
		public Int24 CriteriaId { get; internal set; } = 0;

		/// <summary>
		/// The ID of the associated Achievement.
		/// </summary>
		[MySqlColumnName("Achievement_Id")]
		public ushort AchievementId { get; internal set; } = 0;

		/// <summary>
		/// A value indicating the type of criteria.
		/// </summary>
		/// <remarks>
		/// For a full list of supported values, see <see cref="AchievementCriteriaType"/>.
		/// </remarks>
		[MySqlColumnName("Type")]
		[EnumType(typeof(AchievementCriteriaType))]
		public abstract byte Type { get; internal set; }

		/// <summary>
		/// A description of the criteria.
		/// </summary>
		[MySqlColumnName("Description", isLocalized: true)]
		public string Description { get; set; }

		/// <summary>
		/// A bitmask for further defining the behavior of the criteria.
		/// </summary>
		/// <remarks>
		/// For possible values, see <see cref="AchievementCriteriaFlags"/>.
		/// </remarks>
		[MySqlColumnName("Flags")]
		[EnumType(typeof(AchievementCriteriaFlags))]
		public uint Flags { get; set; }

		/// <summary>
		/// A value representing the way in which the timer for a timed requirement is started.
		/// </summary>
		/// <remarks>
		/// For a list of supported values, see <see cref="AchievementCriteriaTimerStartEvent"/>.
		/// </remarks>
		[MySqlColumnName("Timer_Start_Event")]
		[EnumType(typeof(AchievementCriteriaTimerStartEvent))]
		public uint TimerStartEvent { get; set; }

		/// <summary>
		/// The ID of the event, quest, spell, creature, or item used to
		/// trigger the start of the timer.
		/// </summary>
		/// <remarks>
		/// For more info, see <see cref="AchievementCriteriaTimerStartEvent"/>.
		/// </remarks>
		[MySqlColumnName("Timer_Asset_Id")]
		public uint TimerAssetId { get; set; }

		/// <summary>
		/// The time (in seconds) that the character has to meet the criteria.
		/// </summary>
		[MySqlColumnName("Timer_Time")]
		public uint TimerTime { get; set; }

		/// <summary>
		/// A value used to order the criterion under its parent. The value should be greater than 0.
		/// </summary>
		[MySqlColumnName("Ui_Order")]
		public ushort UiOrder { get; set; }
	}
}
