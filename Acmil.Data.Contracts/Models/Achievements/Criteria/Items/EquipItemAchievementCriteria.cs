using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Enums;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria.Items
{
	/// <summary>
	/// A requirement where the character must equip an item.
	/// </summary>
	public class EquipItemAchievementCriteria : BaseAchievementCriteria
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaType.EquipItem;

		/// <summary>
		/// The ID of the item template that defines the item.
		/// </summary>
		/// <remarks>
		/// Taken from the `entry` column of the `item_template` table.
		/// </remarks>
		[MySqlColumnName("Asset_Id")]
		public uint ItemTemplateId { get; set; }

		/// <summary>
		/// The amount of the item that the character needs to have equipped.
		/// </summary>
		[MySqlColumnName("Quantity")]
		public uint Count { get; set; } = 1;
	}
}
