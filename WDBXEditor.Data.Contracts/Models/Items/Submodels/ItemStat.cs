using WDBXEditor.Data.Contracts.Attributes;
using WDBXEditor.Data.Contracts.Models.Items.Enums;

namespace WDBXEditor.Data.Contracts.Models.Items.Submodels
{
	/// <summary>
	/// Object representing a stat modification provided by an equipped item.
	/// </summary>
	public class ItemStat
	{
		/// <summary>
		/// A <see cref="ItemStatType"/> value indicating the stat modified by the equipped item.
		/// </summary>
		[MySqlColumnNameTemplate("stat_type[1:10]")]
		[EnumType(typeof(ItemStatType))]
		[AllowEnumConversionOverride(false)]
		public byte StatModified { get; set; } = 0;

		/// <summary>
		/// The amount it's being modified by.
		/// </summary>
		[MySqlColumnNameTemplate("stat_value[1:10]")]
		public short StatValue { get; set; } = 0;
	}
}
