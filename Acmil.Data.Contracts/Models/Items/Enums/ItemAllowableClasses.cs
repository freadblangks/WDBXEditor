using System;
using Acmil.Data.Contracts.Attributes;

namespace Acmil.Data.Contracts.Models.Items.Enums
{
	/// <summary>
	/// The character class(es) an item is usable by.
	/// </summary>
	[Flags]
	public enum ItemAllowableClasses : int
	{
		// TODO: Definitely verify that these are correct.

		/// <summary>
		/// The item is usable by all character classes.
		/// </summary>
		All = -1,

		/// <summary>
		/// The item is usable by Warriors.
		/// </summary>
		Warrior = 1,

		/// <summary>
		/// The item is usable by Paladins.
		/// </summary>
		Paladin = 2,

		/// <summary>
		/// The item is usable by Hunters.
		/// </summary>
		Hunter = 4,

		/// <summary>
		/// The item is usable by Rogues.
		/// </summary>
		Rogue = 8,

		/// <summary>
		/// The item is usable by Priests.
		/// </summary>
		Priest = 16,

		/// <summary>
		/// The item is usable by Death Knights.
		/// </summary>
		DeathKnight = 32,

		/// <summary>
		/// The item is usable by Shamans.
		/// </summary>
		Shaman = 64,

		/// <summary>
		/// The item is usable by Mages.
		/// </summary>
		Mage = 128,

		/// <summary>
		/// The item is usable by Warlocks.
		/// </summary>
		Warlock = 256,

		/// <summary>
		/// (NOT IMPLEMENTED IN 3.3.5a) The item is usable by Monks.
		/// </summary>
		[NotImplemented]
		Monk = 512,

		/// <summary>
		/// The item is usable by Druids.
		/// </summary>
		Druid = 1024,

		/// <summary>
		/// (NOT IMPLEMENTED IN 3.3.5a) The item is usably by Demon Hunters.
		/// </summary>
		[NotImplemented]
		DemonHunter = 2048
	}
}
