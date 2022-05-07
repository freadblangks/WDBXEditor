namespace WDBXEditor.Data.Contracts.Models.Items.Enums.SubClasses
{
	/// <summary>
	/// Subclasses of the <see cref="ItemClass.Miscellaneous"/> item class.
	/// </summary>
	public enum ItemSubClassMiscellaneous : byte
	{
		/// <summary>
		/// Subclass indicating that an item is Junk. This is roughly defined as any item
		/// that can't be disenchanted, used for a quest, or used in any other way. Vendor trash. 
		/// However, this subclass also includes "loot bag"-type items that the player can 
		/// right-click to open.
		/// </summary>
		Junk = 0,

		/// <summary>
		/// Subclass indicating than an item is a Reagent.
		/// </summary>
		Reagent = 1,

		/// <summary>
		/// Subclass indicating that an item teaches the player how to summon a non-Holiday Companion Pet.
		/// </summary>
		Pet = 2,

		/// <summary>
		/// Subclass indicating that an item is associated with a World Event.
		/// </summary>
		Holiday = 3,

		/// <summary>
		/// Subclass indicating that an item does not fit into any other class-subclass combination.
		/// </summary>
		Other = 4,

		/// <summary>
		/// Subclass indicating that an item teaches the player how to summon a Mount.
		/// </summary>
		Mount = 5
	}
}
