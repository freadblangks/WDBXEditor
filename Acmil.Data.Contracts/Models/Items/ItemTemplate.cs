using System;
using System.Collections.Generic;

namespace Acmil.Data.Contracts.Models.Items
{
	/// <summary>
	/// An object representing an Item Template.
	/// </summary>
	/// <remarks>
	/// Item Templates are used to define items.
	/// </remarks>
	public class ItemTemplate : BaseItemTemplate
	{
		///// <summary>
		///// An integer uniquely identifying the Item Template in the `item_template` database table.
		///// </summary>
		//public UInt24 Entry { get; internal set; }

		//// TODO: make this and all the other enum properties not enums. They will cause problems for someone wanting to add custom stuff.
		//// Leave the enums themselves though, and link them in the docstrings.
		///// <summary>
		///// The item's class.
		///// </summary>
		//public ItemClass Class { get; set; }

		//public sbyte SubClass { get; set; }

		///// <summary>
		///// The name of the Item.
		///// </summary>
		//public string Name { get; set; }

		///// <summary>
		///// TODO: Add docstring for this. I'm wondering if this actually 100% determines the icon for the item.
		///// </summary>
		//public int DisplayId { get; set; }

		///// <summary>
		///// The quality of the item. This determines its name color.
		///// </summary>
		//public ItemQuality Quality { get; set; }

		///// <summary>
		///// The size of the Item stack when sold by vendors.
		///// Also, if a vendor has limited instances of the Item available, every time the vendor list is refreshed, the number of instances increases by this number.
		///// </summary>
		//public byte BuyCount { get; set; }

		///// <summary>
		///// The price (in copper) required to purchase the Item from a vendor.
		///// </summary>
		//public long BuyPrice { get; set; }

		//// TODO: Determine whether setting this to 0 actually makes something not sellable to a vendor.
		///// <summary>
		///// The amount of copper that a vendor will give you for the Item if you sell it to them. Put 0 here if the Item cannot be sold to a vendor.
		///// </summary>
		//public int SellPrice { get; set; }

		///// <summary>
		///// An <see cref="InventoryType.InventoryType"/> value that dictates in which slot an Item can be equipped.
		///// </summary>
		//public InventoryType InventoryType { get; set; }

		//// TODO: Check if the order of the spells matters for things like learning a spell from the item.

		//public ItemSpell ItemSpell1 { get; set; }

		//public ItemSpell ItemSpell2 { get; set; }

		//public ItemSpell ItemSpell3 { get; set; }

		//public ItemSpell ItemSpell4 { get; set; }

		//public ItemSpell ItemSpell5 { get; set; }

		///// <summary>
		///// A <see cref="BondingType.BondingType"/> value indicating how the Item should become bound to the player, if at all.
		///// </summary>
		//public BondingType Bonding { get; set; }

		///// <summary>
		///// The description that will appear in orange letters at the bottom of the Item tooltip.
		///// </summary>
		//public string Description { get; set; }

		///// <summary>
		///// A field used to determine whether a template has been verified from the WDB files.
		///// 0	=> Not yet parsed
		///// > 0	=> Has been parsed with WDB files from the client with that specific build number.
		///// -1	=> Just a placeholder until proper data is found in the WDBs.
		///// </summary>
		//public short VerifiedBuild { get; internal set; }
	}
}
