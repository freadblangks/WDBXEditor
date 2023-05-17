using Acmil.Common.Utility.Types.Primitives;
using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.General.Enums;
using Acmil.Data.Contracts.Models.Items.Enums;

namespace Acmil.Data.Contracts.Models.Items
{
	public class CompleteItemTemplate : HeldArmamentEquipableItemTemplate
	{
		/// <summary>
		/// The number of item slots in the container.
		/// </summary>
		[MySqlColumnName("ContainerSlots")]
		public byte ContainerSlots { get; set; } = 0;

		/// <summary>
		/// An ID from GemProperties.dbc indicating the effect the gem provides
		/// when placed in a socket.
		/// </summary>
		[MySqlColumnName("GemProperties")]
		public Int24 GemProperties { get; set; } = 0;

		/// <summary>
		/// An ID taken from the `entry` column of the `page_text` table, indicating
		/// the text the item should display when used.
		/// </summary>
		[MySqlColumnName("PageText")]
		public UInt24 PageTextId { get; set; } = 0;

		/// <summary>
		/// A <see cref="Language"/> value indicating which in-game language the text
		/// of the readable item is written in.
		/// </summary>
		/// <remarks>
		/// The values for this field are sourced from Languages.dbc.
		/// </remarks>
		[MySqlColumnName("LanguageID")]
		[EnumType(typeof(Language))]
		[AllowEnumConversionOverride(true)]
		public byte LanguageId { get; set; } = 0;

		/// <summary>
		/// An <see cref="ItemPageTextMaterial"/> value indicating what material
		/// "theme" the text should be displayed in.
		/// </summary>
		/// <remarks>
		/// The values for this field are sourced from PageTextMaterial.dbc.
		/// </remarks>
		[MySqlColumnName("PageMaterial")]
		[EnumType(typeof(ItemPageTextMaterial))]
		[AllowEnumConversionOverride(false)]
		public byte PageMaterialId { get; set; } = 0;

		/// <summary>
		/// The ID of the quest started by the item.
		/// </summary>
		[MySqlColumnName("startquest")]
		public UInt24 QuestId { get; set; } = 0;

		/// <summary>
		/// The minimum amount of money (in copper) the item can contain.
		/// </summary>
		[MySqlColumnName("minMoneyLoot")]
		public uint MinMoneyLoot { get; set; } = 0;

		/// <summary>
		/// The maximum amount of money (in copper) the item can contain.
		/// </summary>
		[MySqlColumnName("maxMoneyLoot")]
		public uint MaxMoneyLoot { get; set; } = 0;
	}
}
