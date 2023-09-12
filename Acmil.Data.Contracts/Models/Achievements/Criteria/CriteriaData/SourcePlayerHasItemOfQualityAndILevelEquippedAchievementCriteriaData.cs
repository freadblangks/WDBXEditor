using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Criteria.Enums;
using Acmil.Data.Contracts.Models.Characters.Enums;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria.CriteriaData
{
	/// <summary>
	/// Requirement data where the player must equip an item of a certain quality and iLevel in a particular equipment slot.
	/// </summary>
	/// <remarks>
	/// The item quality is specified by the parent criteria's `Asset_Id` column.
	/// </remarks>
	public class SourcePlayerHasItemOfQualityAndILevelEquippedAchievementCriteriaData : BaseAchievementCriteriaData
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaDataType.SourcePlayerHasItemOfQualityAndILevelEquipped;

		/// <summary>
		/// The required iLevel of the item.
		/// </summary>
		[MySqlColumnName("value1")]
		public uint ILevel { get; set; }

		/// <summary>
		/// A value indicating the equipment slot where the item must be equipped.
		/// </summary>
		/// <remarks>
		/// For a full list of supported values, see <see cref="EquipmentSlot"/>.
		/// </remarks>
		[MySqlColumnName("value2")]
		[EnumType(typeof(EquipmentSlot))]
		public uint CharacterEquipSlot { get; set; }
	}
}
