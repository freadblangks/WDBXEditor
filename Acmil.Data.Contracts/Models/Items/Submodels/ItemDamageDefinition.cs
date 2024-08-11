using Acmil.Data.Contracts.Models.Items.Enums;
using Acmil.Data.Contracts.Attributes;

namespace Acmil.Data.Contracts.Models.Items.Submodels
{
	/// <summary>
	/// Object that defines the damage a weapon can do on hit.
	/// </summary>
	public class ItemDamageDefinition
	{
		/// <summary>
		/// The minimum possible damage on hit.
		/// </summary>
		[MySqlColumnNameTemplate("dmg_min[1:2]")]
		public float MinValue { get; set; } = 0;

		/// <summary>
		/// The maximum possible damage on hit.
		/// </summary>
		[MySqlColumnNameTemplate("dmg_max[1:2]")]
		public float MaxValue { get; set; } = 0;

		/// <summary>
		/// An <see cref="ItemDamageType"/> value indicating the type of damage done.
		/// </summary>
		[MySqlColumnNameTemplate("dmg_type[1:2]")]
		[EnumType(typeof(ItemDamageType))]
		[AllowEnumConversionOverride(false)]
		public byte DamageType { get; set; } = 0;
	}
}
