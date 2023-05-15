namespace Acmil.Data.Contracts.Models.Items.Enums.SubClasses
{
	/// <summary>
	/// Subclasses of the <see cref="ItemClass.TradeGoods"/> item class.
	/// </summary>
	public enum ItemSubClassTradeGoods : byte
	{
		/// <summary>
		/// (DO NOT USE) The default subclass only used by Trade Goods not available to players.
		/// For Trade Goods that don't fit into other categories, use <see cref="Other"/>.
		/// </summary>
		TradeGoods = 0,

		/// <summary>
		/// Subclass indicating a Parts-type item. These are items made by crafting professions for their
		/// own recipes (e.g. parts for Engineering, inks for Inscription).
		/// </summary>
		Parts = 1,

		/// <summary>
		/// Subclass indicating a Explosive-type item, made by Engineers.
		/// </summary>
		Explosives = 2,

		/// <summary>
		/// Subclass indicating a Device-type item, made by Engineers.
		/// </summary>
		Devices = 3,

		/// <summary>
		/// Subclass indicating a Jewelcrafting-type trade good. These are items made by Jewelcrafters 
		/// for use in their own recipes.
		/// </summary>
		Jewelcrafting = 4,

		/// <summary>
		/// Subclass indicating a Cloth-type trade good. These are items made by Tailors for
		/// use in their own recipes.
		/// </summary>
		Cloth = 5,

		/// <summary>
		/// Subclass indicating a Leather-type trade good. These are items acquired through skinning
		/// or made by a Leatherworker for use in their recipes.
		/// </summary>
		Leather = 6,

		/// <summary>
		/// Subclass indicating Metal or Stone trade good. These are stones and ore acquired through
		/// mining as well as smelted metal bars.
		/// </summary>
		MetalAndStone = 7,

		/// <summary>
		/// Subclass indicating a Meat-type trade good.
		/// </summary>
		Meat = 8,

		/// <summary>
		/// Subclass indicating an herb-type trade good.
		/// </summary>
		Herb = 9,

		/// <summary>
		/// Subclass indicating an Elemental-type trade good (e.g. Elemental Fire, Crystallized Water, Mote of Life).
		/// </summary>
		Elemental = 10,

		/// <summary>
		/// Subclass indicating a trade good that doesn't fit into the other categories. These are usually purchased
		/// from vendors, looted from enemies, or acquired from some other non-crafting source. However, it does include
		/// Darkmoon Cards.
		/// </summary>
		Other = 11,

		/// <summary>
		/// Subclass indicating an item created by an Enchanter
		/// </summary>
		Enchanting = 12,

		/// <summary>
		/// Subclass indicating a Materials-type trade good.
		/// </summary>
		/// <remarks>
		/// The only obtainable items in this subclass are Nether Vortex and Primal Nether.
		/// </remarks>
		Materials = 13,

		/// <summary>
		/// Subclass indicating an item used to enchant armor. NOTE: This seems to be exclusively used for
		/// Armor Vellum I-III. If you're adding an item that enchants armor, use <see cref="ItemSubClassConsumable.ItemEnhancement"/>.
		/// </summary>
		ArmorEnchantment = 14,

		/// <summary>
		/// Subclass indicating an item used to enchant weapons. NOTE: This seems to be exclusively used for
		/// Armor Vellum I-III. If you're adding an item that enchants weapons, use <see cref="ItemSubClassConsumable.ItemEnhancement"/>.
		/// </summary>
		WeaponEnchantment = 15
	}
}
