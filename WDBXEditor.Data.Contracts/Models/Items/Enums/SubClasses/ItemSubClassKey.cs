namespace WDBXEditor.Data.Contracts.Models.Items.Enums.SubClasses
{
	/// <summary>
	/// Subclasses of the <see cref="ItemClass.Key"/> item class.
	/// </summary>
	public enum ItemSubClassKey : byte
	{
		/// <summary>
		/// Subclass indicating a Key-type item. These are items that, when in the player's
		/// inventory, allow them to activate Game Objects (typically doors or chests).
		/// </summary>
		/// <remarks>
		/// However, this subclass also includes any item that's supposed to be a key
		/// and doesn't have a more important subclass applied to it.
		/// </remarks>
		Key = 0,

		/// <summary>
		/// Subclass indicating a Lockpick-type item. These are not normally available to players.
		/// </summary>
		Lockpick = 1
	}
}
