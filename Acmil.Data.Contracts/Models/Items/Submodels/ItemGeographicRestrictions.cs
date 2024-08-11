using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Types.Primitives;

namespace Acmil.Data.Contracts.Models.Items.Submodels
{
	/// <summary>
	/// Object containing data about geographic areas an item is restricted to.
	/// </summary>
	public class ItemGeographicRestrictions
	{

		/// <summary>
		/// The ID of the Zone in which the item can be used.
		/// If the player leaves the Zone, the item will be deleted from their inventory.
		/// Set to 0 for no restriction.
		/// </summary>
		/// <remarks>
		/// This ID comes from the `ID` column of `AreaTable.dbc`.
		/// </remarks>
		[MySqlColumnName("area")]
		public UInt24 ZoneId { get; set; } = 0;

		/// <summary>
		/// The ID of the Map in which the item can be used.
		/// If the player leaves the map, the item will be deleted from their inventory.
		/// Set to 0 for no restriction.
		/// </summary>
		[MySqlColumnName("Map")]
		public short MapId { get; set; } = 0;
	}
}
