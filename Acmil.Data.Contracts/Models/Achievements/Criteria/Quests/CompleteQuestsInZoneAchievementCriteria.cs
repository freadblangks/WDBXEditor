using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Enums;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria.Quests
{
	/// <summary>
	/// A requirement where the character must complete a certain number of quests in a particular zone.
	/// </summary>
	public class CompleteQuestsInZoneAchievementCriteria : BaseAchievementCriteria
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaType.CompleteQuestsInZone;

		/// <summary>
		/// The ID of the Zone where the quests must be completed.
		/// </summary>
		/// <remarks>
		/// Taken from the `ID` column of `AreaTable.dbc`.
		/// </remarks>
		[MySqlColumnName("Asset_Id")]
		public uint ZoneId { get; set; }

		/// <summary>
		/// The number of quests to be completed.
		/// </summary>
		[MySqlColumnName("Quantity")]
		public uint Count { get; set; }
	}
}
