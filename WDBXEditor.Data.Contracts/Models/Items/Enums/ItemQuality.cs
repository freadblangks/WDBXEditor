namespace WDBXEditor.Data.Contracts.Models.Items.Enums
{
	/// <summary>
	/// A value indicating the quality of an Item.
	/// </summary>
	public enum ItemQuality : byte
	{
		/// <summary>
		/// Poor quality (Gray).
		/// </summary>
		Poor = 0,

		/// <summary>
		/// Common quality (White).
		/// </summary>
		Common = 1,

		/// <summary>
		/// Uncommon quality (Green).
		/// </summary>
		Uncommon = 2,

		/// <summary>
		/// Rare quality (Blue).
		/// </summary>
		Rare = 3,

		/// <summary>
		/// Epic quality (Purple).
		/// </summary>
		Epic = 4,

		/// <summary>
		/// Legendary quality (Orange).
		/// </summary>
		Legendary = 5,

		/// <summary>
		/// Artifact quality (Red). This seems to mostly be for developer Items, not normally available to players.
		/// </summary>
		Artifact = 6,

		/// <summary>
		/// Heirloom quality (Gold).
		/// </summary>
		Heirloom = 7
	}
}
