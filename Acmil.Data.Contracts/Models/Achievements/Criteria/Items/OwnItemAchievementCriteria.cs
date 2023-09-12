using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Enums;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria.Items
{
	/// <summary>
	/// A requirement where the character must have the item in their inventory or bank.
	/// </summary>
	public class OwnItemAchievementCriteria : BaseAchievementCriteria
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaType.OwnItem;

		/// <summary>
		/// The ID of the item template that defines the item.
		/// </summary>
		/// <remarks>
		/// Taken from the `entry` column of the `item_template` table.
		/// </remarks>
		[MySqlColumnName("Asset_Id")]
		public uint ItemTemplateId { get; set; }

		/// <summary>
		/// The number of the item required.
		/// </summary>
		[MySqlColumnName("Quantity")]
		public uint Count { get; set; }
	}
}
