namespace Acmil.Data.Contracts.Models.Items.Enums
{
	/// <summary>
	/// The slot(s) in which an item can be equipped.
	/// </summary>
	public enum ItemInventorySlot : byte
	{
		/// <summary>
		/// The item is not equipable.
		/// </summary>
		NonEquipable = 0,

		/// <summary>
		/// The item can be equipped in the Head slot.
		/// </summary>
		Head = 1,

		/// <summary>
		/// The item can be equipped in the Neck slot.
		/// </summary>
		Neck = 2,

		/// <summary>
		/// The item can be equipped in the Shoulder slot.
		/// </summary>
		Shoulder = 3,

		/// <summary>
		/// The item can be equipped in the Shirt slot.
		/// </summary>
		Shirt = 4,

		/// <summary>
		/// The item can be equipped in the Chest slot.
		/// </summary>
		Chest = 5,

		/// <summary>
		/// The item can be equipped in the Waist slot.
		/// </summary>
		Waist = 6,

		/// <summary>
		/// The item can be equipped in the Legs slot.
		/// </summary>
		Legs = 7,

		/// <summary>
		/// The item can be equipped in the Feet slot.
		/// </summary>
		Feet = 8,

		/// <summary>
		/// The item can be equipped in the Wrists slot.
		/// </summary>
		Wrists = 9,

		/// <summary>
		/// The item can be equipped in the Hands slot.
		/// </summary>
		Hands = 10,

		/// <summary>
		/// The item can be equipped in a Finger slot.
		/// </summary>
		Finger = 11,

		/// <summary>
		/// The item can be equipped in a Trinket slot.
		/// </summary>
		Trinket = 12,

		/// <summary>
		/// The item can be equipped in a Main-Hand or Off-Hand slot.
		/// </summary>
		OneHand = 13,

		/// <summary>
		/// The item can be equipped as a Shield.
		/// </summary>
		Shield = 14,

		/// <summary>
		/// The item can be equipped as a Bow.
		/// </summary>
		RangedBows = 15,

		/// <summary>
		/// The item can be equipped in the Back slot.
		/// </summary>
		Back = 16,

		/// <summary>
		/// The item can be equipped as a Two-Handed weapon.
		/// </summary>
		TwoHand = 17,

		/// <summary>
		/// The item can be equipped as a Bag.
		/// </summary>
		Bag = 18,

		/// <summary>
		/// The item can be equipped in the Tabard slot.
		/// </summary>
		Tabard = 19,

		// TODO: Look into how this is different from Chest.
		Robe = 20,

		/// <summary>
		/// The item can be equipped in the Main-Hand slot.
		/// </summary>
		MainHand = 21,

		/// <summary>
		/// The item can be equipped in the Off-Hand slot.
		/// </summary>
		OffHand = 22,

		/// <summary>
		/// The item is considered armor (instead of a weapon) held in off-hand (e.g. Tomes, Flowers, Orbs, etc.)
		/// </summary>
		HeldInOffHand = 23,

		/// <summary>
		/// The item can be equipped as Ammo.
		/// </summary>
		Ammo = 24,

		/// <summary>
		/// The item can be equipped as a Thrown weapon.
		/// </summary>
		Thrown = 25,

		/// <summary>
		/// The item can be equipped as a Wand/Gun.
		/// </summary>
		RangedWandsGuns = 26,

		/// <summary>
		/// The item can be equipped as a Quiver.
		/// </summary>
		Quiver = 27,

		/// <summary>
		/// The item can be equipped as a Relic.
		/// </summary>
		Relic = 28
	}
}
