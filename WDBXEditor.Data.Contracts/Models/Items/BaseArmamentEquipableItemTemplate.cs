using WDBXEditor.Common.Utility.Types.Primitives;
using WDBXEditor.Data.Contracts.Attributes;
using WDBXEditor.Data.Contracts.Models.Items.Submodels;

namespace WDBXEditor.Data.Contracts.Models.Items
{
	/// <summary>
	/// Base class for item templates defining weapons and armor.
	/// </summary>
	public abstract class BaseArmamentEquipableItemTemplate : BaseEquipableItemTemplate
	{
		/// <summary>
		/// The item level of the item.
		/// </summary>
		[MySqlColumnName("ItemLevel")]
		public ushort ItemLevel { get; set; } = 0;

		/// <summary>
		/// The number of stats on the item. This must be accurately set to display the stats in the item tooltip.
		/// </summary>
		[MySqlColumnName("StatsCount")]
		public byte StatsCount { get; set; } = 0;

		/// <summary>
		/// An array of stat modifications made by the item when equipped.
		/// </summary>
		[CompoundField(10)]
		public ItemStat[] Stats { get; set; } = new ItemStat[10];

		// TODO: Figure out these two stats.
		/// <summary>
		/// Id that corresponds to an entry in ScalingStatDistribution.dbc. Used for heirlooms.
		/// </summary>
		[MySqlColumnName("ScalingStatDistribution")]
		public short ScalingStatDistribution { get; set; } = 0;

		/// <summary>
		/// Bitmask that has something to do with ScalingStatValues.dbc. Used for heirlooms.
		/// </summary>
		[MySqlColumnName("ScalingStatValue")]
		public uint ScalingStatValue { get; set; } = 0;

		/// <summary>
		/// The armor value provided by the item when equipped.
		/// </summary>
		[MySqlColumnName("armor")]
		public ushort Armor { get; set; } = 0;

		/// <summary>
		/// The different resistance modifications provided by the item when equipped.
		/// </summary>
		[CompoundField]
		public ItemResistances Resistances { get; set; } = new ItemResistances();

		/// <summary>
		/// ID that defines the chances of having a random property and suffix on the item.
		/// The ID links to the ID in ItemRandomProperties.dbc and the entry column in the item_enchantment_template table.
		/// </summary>
		/// <remarks>
		/// NOTE: This field and <see cref="RandomSuffix"/> cannot both be set for an item.
		/// </remarks>
		[MySqlColumnName("RandomProperty")]
		public Int24 RandomProperty { get; set; } = 0;

		/// <summary>
		/// ID that defines the chances of having a random property and suffix on the item.
		/// The ID links to the ID in ItemRandomSuffix.dbc and the entry column in the item_enchantment_template table.
		/// </summary>
		/// <remarks>
		/// NOTE: This field and <see cref="RandomProperty"/> cannot both be set for an item.
		/// </remarks>
		[MySqlColumnName("RandomSuffix")]
		public UInt24 RandomSuffix { get; set; } = 0;

		/// <summary>
		/// The ID of the item set the item belongs to.
		/// </summary>
		/// TODO: Determine whether you can add new ones through ItemSet.dbc.
		[MySqlColumnName("itemset")]
		public UInt24 ItemSetId { get; set; } = 0;

		/// <summary>
		/// The maximum durability value the item has when fully repaired.
		/// </summary>
		[MySqlColumnName("MaxDurability")]
		public ushort MaxDurability { get; set; } = 0;

		/// <summary>
		/// An array of objects defining the gem socks on the item.
		/// </summary>
		[CompoundField(3)]
		public ItemSocketDefinition[] SocketDefinitions { get; set; } = new ItemSocketDefinition[3];

		/// <summary>
		/// An ID indicating the socket bonus provided by the item if all gem sockets have been filled
		/// with the correct color gems.
		/// </summary>
		/// <remarks>
		/// These IDs are in GemProperties.dbc, but they map to effects in SpellItemEnchantment.dbc.
		/// </remarks>
		[MySqlColumnName("socketBonus")]
		public Int24 SocketBonus { get; set; } = 0;

		/// <summary>
		/// The required skill to disenchant the item. Use -1 if the item is not disenchantable.
		/// </summary>
		[MySqlColumnName("RequiredDisenchantSkill")]
		public short RequiredDisenchantSkill { get; set; } = -1;

		/// <summary>
		/// An ID referring to the entry column in the `disenchant_loot_template` table,
		/// used to determine what is looted upon disenchanting the item.
		/// </summary>
		/// TODO: Figure out if there's a way to determine this automatically based on the
		/// required level to wear the item and which expansion it's from.
		[MySqlColumnName("DisenchantID")]
		public UInt24 DisenchantId { get; set; } = 0;
	}
}
