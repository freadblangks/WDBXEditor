using WDBXEditor.Common.Utility.Types.Primitives;
using WDBXEditor.Data.Contracts.Attributes;
using WDBXEditor.Data.Contracts.Models.Items.Enums;
using WDBXEditor.Data.Contracts.Models.Items.Enums.SubClasses;
using WDBXEditor.Data.Contracts.Models.Items.Submodels;

namespace WDBXEditor.Data.Contracts.Models.Items
{
	/// <summary>
	/// The base class from which all ItemTemplate classes inherit.
	/// </summary>
	public abstract class BaseItemTemplate
	{
		/// <summary>
		/// An ID that uniquely identifies the Item Template in the `item_template` table.
		/// </summary>
		[MySqlColumnName("entry")]
		public UInt24 EntryId { get; internal set; } = 0;

		/// <summary>
		/// An ID that indicates which Item Class the Item Template belongs to.
		/// For possible values see <see cref="ItemClass"/>.
		/// </summary>
		[MySqlColumnName("class")]
		[EnumType(typeof(ItemClass))]
		[AllowEnumConversionOverride(true)]
		public byte Class { get; set; } = 0;

		/// <summary>
		/// An ID that indicates which Subclass of the Item Class in <see cref="Class"/>
		/// the Item Template belongs to.
		/// </summary>
		[MySqlColumnName("subclass")]
		[EnumType(typeof(ItemSubClassArmor))]
		[EnumType(typeof(ItemSubClassConsumable))]
		[EnumType(typeof(ItemSubClassContainer))]
		[EnumType(typeof(ItemSubClassGem))]
		[EnumType(typeof(ItemSubClassGlyph))]
		[EnumType(typeof(ItemSubClassKey))]
		[EnumType(typeof(ItemSubClassMiscellaneous))]
		[EnumType(typeof(ItemSubClassProjectile))]
		[EnumType(typeof(ItemSubClassQuest))]
		[EnumType(typeof(ItemSubClassQuiver))]
		[EnumType(typeof(ItemSubClassReagent))]
		[EnumType(typeof(ItemSubClassRecipe))]
		[EnumType(typeof(ItemSubClassTradeGoods))]
		[EnumType(typeof(ItemSubClassWeapon))]
		[AllowEnumConversionOverride(true)]
		public byte SubClass { get; set; } = 0;

		/// <summary>
		/// The name of the item.
		/// </summary>
		[MySqlColumnName("name")]
		public string Name { get; set; } = "";

		/// <summary>
		/// The Model ID of the item. Each model has its own icon assigned to it. So, this field
		/// controls both the model appearance and the icon
		/// // TODO: Verify this.
		/// </summary>
		[MySqlColumnName("displayid")]
		public UInt24 DisplayId { get; set; } = 0;

		/// <summary>
		/// An <see cref="ItemQuality"/> value indicating the quality/rarity of the item. This determines
		/// the color of its name text.
		/// </summary>
		[MySqlColumnName("Quality")]
		[EnumType(typeof(ItemQuality))]
		[AllowEnumConversionOverride(false)]
		public byte Quality { get; set; } = 0;

		/// <summary>
		/// Bitmask of flags used to define specific aspects of the item's behavior.
		/// </summary>
		/// <remarks>
		/// For a full list of known flags for this field, see <see cref="ItemFlags"/>.
		/// </remarks>
		[MySqlColumnName("Flags")]
		[EnumType(typeof(ItemFlags))]
		[AllowEnumConversionOverride(true)]
		public uint FlagsMask { get; set; } = 0;

		/// <summary>
		/// Bitmask of flags used to define specific aspects of the item's behavior.
		/// </summary>
		/// <remarks>
		/// For a full list of known flags for this field, see <see cref="ItemFlagsExtra"/>.
		/// </remarks>
		[MySqlColumnName("FlagsExtra")]
		[EnumType(typeof(ItemFlagsExtra))]
		[AllowEnumConversionOverride(true)]
		public uint FlagsExtraMask { get; set; } = 0;

		/// <summary>
		/// Bitmask of flags used to define specific aspects of the item's behavior.
		/// </summary>
		/// <remarks>
		/// For a full list of known flags for this field, see <see cref="ItemFlagsCustom"/>.
		/// </remarks>
		[MySqlColumnName("flagsCustom")]
		[EnumType(typeof(ItemFlagsCustom))]
		[AllowEnumConversionOverride(true)]
		public uint FlagsCustomMask { get; set; } = 0;

		/// <summary>
		/// The size of the Item stack when sold by vendors.
		/// Also, if a vendor has limited instances of the Item available, every time the vendor list is refreshed, the number of instances increases by this number.
		/// </summary>
		[MySqlColumnName("BuyCount")]
		public byte BuyCount { get; set; } = 1;

		/// <summary>
		/// The price (in copper) required to purchase the item from a vendor.
		/// </summary>
		[MySqlColumnName("BuyPrice")]
		public long BuyPrice { get; set; } = 0;

		/// <summary>
		/// The amount of copper that a vendor will give you for the Item if you sell it to them. Put 0 here if the Item cannot be sold to a vendor.
		/// </summary>
		/// TODO: Determine whether setting this to 0 actually makes something not sellable to a vendor.
		[MySqlColumnName("SellPrice")]
		public uint SellPrice { get; set; } = 0;

		/// <summary>
		/// A bitmask that defines which character class(es) the item is usable by. -1 indicates that all classes can use the item.
		/// </summary>
		/// <remarks>
		/// For the structure of the bitmask, see <see cref="ItemAllowableClasses"/>.
		/// </remarks>
		[MySqlColumnName("AllowableClass")]
		[EnumType(typeof(ItemAllowableClasses))]
		[AllowEnumConversionOverride(true)]
		public int AllowableClassesMask { get; set; } = -1;

		/// <summary>
		/// A bitmask that defines which race(s) the item is usable by. -1 indicates that all classes can use the item.
		/// </summary>
		/// <remarks>
		/// For the structure of the bitmask, see <see cref="ItemAllowableRaces"/>.
		/// </remarks>
		[MySqlColumnName("AllowableRace")]
		[EnumType(typeof(ItemAllowableRaces))]
		[AllowEnumConversionOverride(true)]
		public int AllowableRacesMask { get; set; } = -1;

		[CompoundField]
		public ItemRequirements Requirements { get; set; } = new ItemRequirements();

		/// <summary>
		/// The maximum amount of the item a player can have. Use 0 for no limit.
		/// </summary>
		[MySqlColumnName("maxcount")]
		public int MaxCount { get; set; } = 0;

		/// <summary>
		/// The maximum amount of the item that can be stacked in one inventory or bank slot.
		/// </summary>
		[MySqlColumnName("stackable")]
		public int MaxStackSize { get; set; } = 1;

		/// <summary>
		/// The spells cast, triggered, or taught by the item.
		/// </summary>
		[CompoundField]
		public ItemSpell[] Spells { get; set; } = new ItemSpell[5];

		/// <summary>
		/// A <see cref="ItemBindingType"/> value indicating the item's binding type. This dictates when and if it becomes soulbound.
		/// </summary>
		[MySqlColumnName("bonding")]
		[EnumType(typeof(ItemBindingType))]
		[AllowEnumConversionOverride(false)]
		public byte Binding { get; set; } = 0;

		/// <summary>
		/// The description that will appear in golden yellow letters at the bottom of the item tooltip.
		/// </summary>
		[MySqlColumnName("description")]
		public string Description { get; set; } = "";

		/// <summary>
		/// The Lock ID of the Game Object that this item allows the player to open or interact with. The IDs
		/// for this field come from Lock.dbc.
		/// </summary>
		/// <remarks>
		/// Used for items that function as keys.
		/// </remarks>
		[MySqlColumnName("lockid")]
		public UInt24 LockId { get; set; } = 0;

		/// <summary>
		/// The <see cref="ItemMaterial"/> value indicating what the item is made of. This determines the sound
		/// the item makes when moved. Use -1 for consumables.
		/// </summary>
		[MySqlColumnName("Material")]
		[EnumType(typeof(ItemMaterial))]
		[AllowEnumConversionOverride(false)]
		public sbyte MaterialId { get; set; } = 0;

		/// <summary>
		/// An object defining usage restrictions based on where the player is.
		/// </summary>
		[CompoundField]
		public ItemGeographicRestrictions GeographicRestrictions { get; set; } = new ItemGeographicRestrictions();

		/// <summary>
		/// Bitmask that determines which bag types the item can be placed in.
		/// </summary>
		/// <remarks>
		/// For the structure of the bitmask, see <see cref="ItemBagFamilies"/>.
		/// </remarks>
		[MySqlColumnName("BagFamily")]
		[EnumType(typeof(ItemBagFamilies))]
		[AllowEnumConversionOverride(false)]
		public Int24 BagFamilyId { get; set; } = 0;

		/// <summary>
		/// The ID of the totem category the item falls under. These are used for spells that require a certain non-consumable item to be
		/// in the player's inventory before it can be cast (e.g. totems for Shaman spells, Runed Copper Rod for enchanting, and even Mining Pick for mining).
		/// These are sourced from TotemCategory.dbc.
		/// </summary>
		[MySqlColumnName("TotemCategory")]
		public Int24 TotemCategory { get; set; } = 0;

		/// <summary>
		/// The amount of in-game time (in seconds) that the item will last before disappearing from the player's inventory or bank.
		/// Set to 0 if the item should not expire.
		/// </summary>
		/// <remarks>
		/// If you want this field to specify the amount of ACTUAL time (not in-game time) it takes for the item to disappear,
		/// set the <see cref="ItemFlagsCustom.DurationRealTime"/> flag in the item's <see cref="FlagsMask"/> field.
		/// </remarks>
		[MySqlColumnName("duration")]
		public uint DurationInSeconds { get; set; } = 0;

		/// <summary>
		/// The ID of the item's item limit category. This value comes from ItemLimitCategory.dbc and it's used to determine
		/// which items should be considered to be in the same category when limiting the number of a type of item the player
		/// can have in their inventory (e.g. Lesser Healthstone and Greater Healthstone are both a part of the Healthstone
		/// category, and therefore if you already have one, and can't have any of the other).
		/// </summary>
		[MySqlColumnName("ItemLimitCategory")]
		public short ItemLimitCategory { get; set; } = 0;

		/// <summary>
		/// The ID of the World Event the item is associated with.
		/// </summary>
		/// <remarks>
		/// This ID is sourced from Holidays.dbc.
		/// </remarks>
		[MySqlColumnName("HolidayId")]
		public uint HolidayId { get; set; } = 0;

		/// <summary>
		/// The name of the script associated with the item. This does not seem to be fully implemented.
		/// </summary>
		/// TODO: Figure out if it's actually implemented.
		[MySqlColumnName("ScriptName")]
		public string ScriptName { get; set; } = "";

		/// <summary>
		/// The type of food the item is considered to be when a Hunter tries to feed it to their pet.
		/// </summary>
		[MySqlColumnName("FoodType")]
		[EnumType(typeof(ItemFoodType))]
		[AllowEnumConversionOverride(false)]
		public byte FoodTypeId { get; set; } = 0;

		/// <summary>
		/// A field used to determine whether an item template has been verified from the WDB files.
		/// 0	=> Not yet parsed
		/// > 0	=> Has been parsed with WDB files from the client with that specific build number.
		/// -1	=> Just a placeholder until proper data is found in the WDBs.
		/// </summary>
		[MySqlColumnName("VerifiedBuild")]
		public short? VerifiedBuild { get; internal set; } = 0;

		/// <summary>
		/// UNKNOWN
		/// </summary>
		/// TODO: Figure out what this does and then make sure it's in the right class.
		[MySqlColumnName("ArmorDamageModifier")]
		public float ArmorDamageModifier { get; internal set; } = 0;


	}
}
