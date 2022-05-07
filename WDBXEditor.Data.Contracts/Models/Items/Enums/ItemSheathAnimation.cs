namespace WDBXEditor.Data.Contracts.Models.Items.Enums
{
	/// <summary>
	/// Different possible animations for when a weapon is sheathed or unsheathed using the 'Z' hotkey.
	/// </summary>
	public enum ItemSheathAnimation : byte
	{
		/// <summary>
		/// The item does not have a sheathing/unsheathing animation.
		/// </summary>
		None = 0,

		/// <summary>
		/// The item has a non-staff two-handed weapon's sheathing/unsheathing animation.
		/// </summary>
		TwoHandedWeapon = 1,

		/// <summary>
		/// The item has a staff's sheathing/unsheathing animation.
		/// </summary>
		Staff = 2,

		/// <summary>
		/// The item has a one-handed weapon's sheathing/unsheathing animation.
		/// </summary>
		OneHanded = 3,

		/// <summary>
		/// The item has a shield's sheathing/unsheathing animation.
		/// </summary>
		Shield = 4,

		// TODO: Figure out what this does.
		EnchantersRod = 5,

		/// <summary>
		/// The item has an off-hand item's sheathing/unsheathing animation.
		/// </summary>
		OffHand = 6
	}
}
