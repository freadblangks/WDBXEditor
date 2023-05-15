using System;
using Acmil.Data.Contracts.Attributes;

namespace Acmil.Data.Contracts.Models.Items.Enums
{
	/// <summary>
	/// The race(s) an item is usable by.
	/// </summary>
	/// TODO: Check if some of the "not implemented" ones are used for NPCs and what
	/// items they can have equipped.
	[Flags]
	public enum ItemAllowableRaces : int
	{
		/// <summary>
		/// The item is usable by all races.
		/// </summary>
		All = -1,

		/// <summary>
		/// The item is usable by Humans.
		/// </summary>
		Human = 1,

		/// <summary>
		/// The item is usable by Orcs.
		/// </summary>
		Orc = 2,

		/// <summary>
		/// The item is usable by Dwarves.
		/// </summary>
		Dwarf = 4,

		/// <summary>
		/// The item is usable by Night Elves.
		/// </summary>
		NightElf = 8,

		/// <summary>
		/// The item is usable by Undead.
		/// </summary>
		Undead = 16,

		/// <summary>
		/// The item is usable by Tauren.
		/// </summary>
		Tauren = 32,

		/// <summary>
		/// The item is usable by Gnomes.
		/// </summary>
		Gnome = 64,

		/// <summary>
		/// The item is usable by Trolls.
		/// </summary>
		Troll = 128,

		/// <summary>
		/// (NOT IMPLEMENTED IN 3.3.5a) The item is usable by Goblins.
		/// </summary>
		[NotImplemented]
		Goblin = 256,

		/// <summary>
		/// The item is usable by Blood Elves.
		/// </summary>
		BloodElf = 512,

		/// <summary>
		/// The item is usable by Draenei.
		/// </summary>
		Draenei = 1024,

		/// <summary>
		/// (NOT IMPLEMENTED) The item is usable by Fel Orcs.
		/// </summary>
		[NotImplemented]
		FelOrc = 2048,

		/// <summary>
		/// (NOT IMPLEMENTED) The item is usable by Naga.
		/// </summary>
		[NotImplemented]
		Naga = 4096,

		/// <summary>
		/// (NOT IMPLEMENTED) The item is usable by Broken.
		/// </summary>
		[NotImplemented]
		Broken = 8192,

		/// <summary>
		/// (NOT IMPLEMENTED) The item is usable by Skeletons.
		/// </summary>
		[NotImplemented]
		Skeleton = 16384,

		/// <summary>
		/// (NOT IMPLEMENTED) The item is usable by Vrykul.
		/// </summary>
		[NotImplemented]
		Vrykul = 32768,

		/// <summary>
		/// (NOT IMPLEMENTED) The item is usable by Tuskarr.
		/// </summary>
		[NotImplemented]
		Tuskarr = 65536,

		/// <summary>
		/// (NOT IMPLEMENTED) The item is usable by Forest Trolls.
		/// </summary>
		[NotImplemented]
		ForestTroll = 131072,

		/// <summary>
		/// (NOT IMPLEMENTED) The item is usable by Taunka.
		/// </summary>
		[NotImplemented]
		Taunka = 262144,

		/// <summary>
		/// (NOT IMPLEMENTED) The item is usable by Northrend Skeletons.
		/// </summary>
		[NotImplemented]
		NorthrendSkeleton = 524288,

		/// <summary>
		/// (NOT IMPLEMENTED) The item is usable by Ice Trolls.
		/// </summary>
		[NotImplemented]
		IceTroll = 1048576,

		/// <summary>
		/// (NOT IMPLEMENTED in 3.3.5a) The item is usable by Worgen.
		/// </summary>
		[NotImplemented]
		Worgen = 2097152,

		/// <summary>
		/// (NOT IMPLEMENTED in 3.3.5a) The item is usable by Pandaren who are neither Alliance nor Horde.
		/// </summary>
		[NotImplemented]
		PandarenNeutral = 8388608,

		/// <summary>
		/// (NOT IMPLEMENTED in 3.3.5a) The item is usable by Alliance Pandaren.
		/// </summary>
		[NotImplemented]
		PandarenAlliance = 16777216,

		/// <summary>
		/// (NOT IMPLEMENTED in 3.3.5a) The item is usable by Horde Pandaren.
		/// </summary>
		[NotImplemented]
		PandarenHorde = 33554432
	}
}
