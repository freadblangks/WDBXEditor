namespace WDBXEditor.Data.Contracts.Models.Items.Enums
{
	/// <summary>
	/// How a spell associated with an item should be triggered.
	/// </summary>
	public enum ItemSpellTrigger : byte
	{
		/// <summary>
		/// The spell is cast when the item is used.
		/// </summary>
		Use = 0,

		/// <summary>
		/// The spell is cast when the item is equipped.
		/// </summary>
		OnEquip = 1,

		/// <summary>
		/// The spell has a chance to cast/trigger when the player deals a hit.
		/// </summary>
		ChanceOnHit = 2,

		/// <summary>
		/// UNKNOWN
		/// </summary>
		/// TODO: Figure out if this actually does anything.
		Soulstone = 4,

		/// <summary>
		/// UNKNOWN
		/// </summary>
		/// TODO: Figure out what this does.
		UseWithNoDelay = 5,

		/// <summary>
		/// The spell is taught to the player. NOTE: For this to work, the associated spell must be in
		/// the item's second spell slot, and the first spell slot must be occupied with the "Learn" spell,
		/// whose ID is 483 by default.
		/// </summary>
		LearnSpellId = 6
	}
}
