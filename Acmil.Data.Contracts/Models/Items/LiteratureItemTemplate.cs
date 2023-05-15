using Acmil.Data.Contracts.Models.General.Enums;
using Acmil.Data.Contracts.Models.Items.Enums;
using WDBXEditor.Common.Utility.Types.Primitives;
using Acmil.Data.Contracts.Attributes;

namespace Acmil.Data.Contracts.Models.Items
{
	/// <summary>
	/// Item template for readable items.
	/// </summary>
	public class LiteratureItemTemplate : BaseItemTemplate
	{
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
	}
}
