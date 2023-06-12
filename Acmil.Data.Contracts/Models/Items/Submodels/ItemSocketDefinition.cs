using Acmil.Data.Contracts.Models.Items.Enums;
using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Types.Primitives;

namespace Acmil.Data.Contracts.Models.Items.Submodels
{
	/// <summary>
	/// Object that defines the number of sockets of a specific color are on an item.
	/// </summary>
	public class ItemSocketDefinition
	{
		/// <summary>
		/// An <see cref="ItemSocketColor"/> value representing the color of gems meant for the socket.
		/// </summary>
		[MySqlColumnNameTemplate("socketColor_[1:3]")]
		[EnumType(typeof(ItemSocketColor))]
		[AllowEnumConversionOverride(false)]
		public sbyte Color { get; set; } = 0;

		/// <summary>
		/// The number of sockets of the color specified by <see cref="Color"/> to include on the item.
		/// </summary>
		[MySqlColumnNameTemplate("socketContent_[1:3]")]
		public Int24 Count { get; set; } = 0;
	}
}
