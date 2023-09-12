using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Enums;
using Acmil.Data.Contracts.Models.Maps.Enums;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria.Exploration
{
	/// <summary>
	/// A requirement where the character must discover an area.
	/// </summary>
	public class ExploreAreaAchievementCriteria : BaseAchievementCriteria
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaType.ExploreArea;

		/// <summary>
		/// The World Map Overlay (WMO) area ID of the area that the character needs to explore.
		/// </summary>
		[MySqlColumnName("Asset_Id")]
		[EnumType(typeof(WmoAreaId))]
		public uint WmoAreaId { get; set; }

		// Making this internal because it doesn't actually do anything,
		// but we'd like to always insert 1 when we add rows to the DB table.
		[MySqlColumnName("Quantity")]
		internal uint Quantity { get; set; } = 1;
	}
}
