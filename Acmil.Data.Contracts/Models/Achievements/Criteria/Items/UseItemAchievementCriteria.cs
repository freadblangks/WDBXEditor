using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Enums;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria.Items
{
	/// <summary>
	/// A requirement where the character must use an item a certain number of times.
	/// </summary>
	public class UseItemAchievementCriteria : BaseAchievementCriteria
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaType.UseItem;

		/// <summary>
		/// The ID of the item template that defines the item.
		/// </summary>
		/// <remarks>
		/// Taken from the `entry` column of the `item_template` table.
		/// </remarks>
		[MySqlColumnName("Asset_Id")]
		public uint ItemTemplateId { get; set; }

		// TODO: Check whether this is the number of times the item
		// is used, or the number of items used.
		/// <summary>
		/// The number of times the item needs to be used.
		/// </summary>
		[MySqlColumnName("Quantity")]
		public uint Count { get; set; }
	}
}
