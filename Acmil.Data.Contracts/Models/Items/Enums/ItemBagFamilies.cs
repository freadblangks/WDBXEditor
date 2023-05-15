using System;

namespace Acmil.Data.Contracts.Models.Items.Enums
{
	/// <summary>
	/// The different types of Bag Families.
	/// These determine which types of bags an item can be placed in.
	/// </summary>
	[Flags]
	public enum ItemBagFamilies
	{
		/// <summary>
		/// The item can be placed in any non-specialized bag.
		/// </summary>
		None = 0,

		/// <summary>
		/// The item can be placed in a Quiver.
		/// </summary>
		Arrows = 1,

		/// <summary>
		/// The item can be placed in an Ammo Pouch
		/// </summary>
		Bullets = 2,

		/// <summary>
		/// The item can be placed in a Soul Bag.
		/// </summary>
		SoulShards = 4,

		/// <summary>
		/// The item can be placed in a Leatherworking Bag.
		/// </summary>
		LeatherworkingSupplies = 8,

		/// <summary>
		/// The item can be placed in an Inscription Bag.
		/// </summary>
		InscriptionSupplies = 16,

		/// <summary>
		/// Herbs can be placed inside the bag.
		/// </summary>
		Herbs = 32,

		/// <summary>
		/// Enchanting supplies can be placed inside the bag.
		/// </summary>
		EnchantingSupplies = 64,

		/// <summary>
		/// Engineering supplies can be placed inside the bag.
		/// </summary>
		EngineeringSupplies = 128,

		/// <summary>
		/// Keys can be placed inside the bag.
		/// </summary>
		Keys = 256,

		/// <summary>
		/// Gems can be placed inside the bag.
		/// </summary>
		Gems = 512,

		/// <summary>
		/// Mining supplies can be placed inside the bag.
		/// </summary>
		MiningSupplies = 1024,

		/// <summary>
		/// (UNUSED) The item is soulbound equipment.
		/// </summary>
		SoulboundEquipment = 2048,

		/// <summary>
		/// The item is a vanity pet.
		/// </summary>
		VanityPets = 4096,

		/// <summary>
		/// The item is a currency token.
		/// </summary>
		CurrencyTokens = 8192,

		/// <summary>
		/// The item is a quest item.
		/// </summary>
		QuestItems = 16384
	}
}
