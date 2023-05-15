namespace Acmil.Data.Contracts.Models.Items.Enums
{
	/// <summary>
	/// Different faction reputation ranks an item can require.
	/// </summary>
	public enum ItemRequiredReputationRank : ushort
	{
		/// <summary>
		/// The item requires at least a Hated standing with the faction.
		/// </summary>
		Hated = 0,

		/// <summary>
		/// The item requires at least a Hostile standing with the faction.
		/// </summary>
		Hostile = 1,

		/// <summary>
		/// The item requires at least an Unfriendly standing with the faction.
		/// </summary>
		Unfriendly = 2,

		/// <summary>
		/// The item requires at least a Neutral standing with the faction.
		/// </summary>
		Neutral = 3,

		/// <summary>
		/// The item requires at least a Friendly standing with the faction.
		/// </summary>
		Friendly = 4,

		/// <summary>
		/// The item requires at least an Honored standing with the faction.
		/// </summary>
		Honored = 5,

		/// <summary>
		/// The item requires at least a Revered standing with the faction.
		/// </summary>
		Revered = 6,

		/// <summary>
		/// The item requires at least an Exalted standing with the faction.
		/// </summary>
		Exalted = 7
	}
}
