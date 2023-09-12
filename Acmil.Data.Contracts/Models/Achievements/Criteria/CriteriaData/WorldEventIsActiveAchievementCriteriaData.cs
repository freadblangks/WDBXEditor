using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Criteria.Enums;
using Acmil.Data.Contracts.Models.World.WorldEvents.Enums;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria.CriteriaData
{
	/// <summary>
	/// Requirement data where a specific world event needs to be active.
	/// </summary>
	public class WorldEventIsActiveAchievementCriteriaData : BaseAchievementCriteriaData
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaDataType.WorldEventIsActive;

		/// <summary>
		/// The ID of the world event.
		/// </summary>
		/// <remarks>
		/// For a full list of supported values, see <see cref="WorldEvent"/>.<br/>
		/// <br/>
		/// NOTE: This is checked against a hardcoded server-side enum.
		/// New world events added to `Holidays.dbc` won't work with this.
		/// </remarks>
		[MySqlColumnName("")]
		[EnumType(typeof(WorldEvent))]
		[AllowEnumConversionOverride(false)]
		public uint HolidayId { get; set; }
	}
}
