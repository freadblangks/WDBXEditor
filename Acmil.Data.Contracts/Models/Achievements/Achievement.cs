using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Criteria;
using Acmil.Data.Contracts.Models.Achievements.Enums;
using Acmil.Data.Contracts.Models.Achievements.Rewards;
using Acmil.Data.Contracts.Models.Maps.Enums;
using Acmil.Data.Contracts.Types.Primitives;

namespace Acmil.Data.Contracts.Models.Achievements
{
	/// <summary>
	/// Object representing an Achievement.
	/// </summary>
	public class Achievement
	{
		/// <summary>
		/// An ID that uniquely identifies the Achievement in Achievement.dbc.
		/// </summary>
		[MySqlColumnName("ID")]
		public ushort AchievementId { get; internal set; } = 0;

		/// <summary>
		/// The faction (Alliance, Horde, or Both) whose players can earn the Achievement.
		/// </summary>
		/// <remarks>
		/// Both : -1,
		/// Horde Only : 0,
		/// Alliance Only : 1
		/// </remarks>
		[MySqlColumnName("Faction")]
		[EnumType(typeof(AchievementFaction))]
		public sbyte Faction { get; set; } = -1;

		// TODO: Determine if this actually does something.
		/// <summary>
		/// The Map ID of the instance (if any) that the Achievement is associated with.
		/// </summary>
		/// <remarks>
		/// For a full list of known values for this field, see <see cref="InstanceMap"/>.
		/// </remarks>
		[MySqlColumnName("Instance_Id")]
		[EnumType(typeof(InstanceMap))]
		[AllowEnumConversionOverride(true)]
		public short InstanceId { get; set; } = -1;

		/// <summary>
		/// The ID of the Achievement that must be earned before this one becomes visible in the player's
		/// Achievements list.
		/// </summary>
		[MySqlColumnName("Supercedes")]
		public ushort PreviousAchievementId { get; set; } = 0;

		/// <summary>
		/// The localized name of the Achievement.
		/// </summary>
		[MySqlColumnName("Title", isLocalized: true)]
		public string Name { get; set; }

		/// <summary>
		/// The localized description of the Achievement.
		/// </summary>
		[MySqlColumnName("Description", isLocalized: true)]
		public string Description { get; set; }

		/// <summary>
		/// The ID of the category the Achievement is under.
		/// </summary>
		[MySqlColumnName("Category")]
		public ushort CategoryId { get; set; }

		/// <summary>
		/// The category under which the Achievement can be found in the UI.
		/// </summary>
		public AchievementCategory Category { get; internal set; } = null;

		/// <summary>
		/// The number of Achievement Points the Achievement is worth.
		/// </summary>
		[MySqlColumnName("Points")]
		public sbyte Points { get; set; }

		/// <summary>
		/// A value used to order the Achievement under its parent. The value should be greater than 0.
		/// </summary>
		[MySqlColumnName("Ui_Order")]
		public ushort UiOrder { get; set; }

		/// <summary>
		/// Bitmask of flags used to define specific aspects of the Achievements behavior.
		/// </summary>
		/// <remarks>
		/// For a full list of known flags for this field, see <see cref="AchievementFlags"/>.
		/// </remarks>
		[MySqlColumnName("Flags")]
		[EnumType(typeof(AchievementFlags))]
		public UInt24 FlagsMask { get; set; }

		/// <summary>
		/// The ID of the icon to display for the Achievement. Defaults to 1 (no icon).
		/// </summary>
		[MySqlColumnName("IconID")]
		public UInt24 IconId { get; set; } = 1;

		/// <summary>
		/// The localized text displayed on the Achievement describing the reward for earning it.
		/// </summary>
		/// <remarks>
		/// If no value is provided, but the <see cref="Reward"/> property is set,
		/// a description will be generated automatically for the Achievement reward.
		/// </remarks>
		[MySqlColumnName("Reward", isLocalized: true)]
		public string RewardDescription { get; set; } = null;

		/// <summary>
		/// The reward the player receives for earning the Achievement.
		/// </summary>
		public BaseAchievementReward Reward { get; internal set; } = null;

		/// <summary>
		/// The minimum number of criteria a player needs to meet to earn the Achievement.
		/// </summary>
		[MySqlColumnName("Minimum_Criteria")]
		public sbyte MinimumCriteriaRequired { get; set; }

		/// <summary>
		/// The ID of an Achievement or Statistic whose met criteria count towards the total
		/// met criteria of this Achievement or Statistic.
		/// </summary>
		/// <remarks>
		/// This is used for Achievements like "Frostbitten", where the previous Achievement's
		/// met criteria should be included in the count for the current one.
		/// </remarks>
		[MySqlColumnName("Shares_Criteria")]
		public ushort LinkedAchievementId { get; set; }

		/// <summary>
		/// The criteria that must be met in order for a character to earn the Achievement.
		/// </summary>
		public List<BaseAchievementCriteria> Criteria { get; internal set; } = null;
	}
}
