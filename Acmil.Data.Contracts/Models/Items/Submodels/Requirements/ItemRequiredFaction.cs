using Acmil.Data.Contracts.Models.Items.Enums;
using Acmil.Data.Contracts.Attributes;

namespace Acmil.Data.Contracts.Models.Items.Submodels.Requirements
{
	/// <summary>
	/// Object representing a reputation requirement for an item.
	/// </summary>
	public class ItemRequiredFaction
	{
		/// <summary>
		/// The ID of the reputation faction.
		/// </summary>
		/// <remarks>
		/// Taken from Faction.dbc.
		/// </remarks>
		[MySqlColumnName("RequiredReputationFaction")]
		public ushort ReputationFactionId { get; set; } = 0;

		/// <summary>
		/// A <see cref="ItemRequiredReputationRank"/> value indicating the required standing
		/// with the reputation faction.
		/// </summary>
		[MySqlColumnName("RequiredReputationRank")]
		[EnumType(typeof(ItemRequiredReputationRank))]
		[AllowEnumConversionOverride(false)]
		public ushort ReputationFactionRank { get; set; } = 0;
	}
}
