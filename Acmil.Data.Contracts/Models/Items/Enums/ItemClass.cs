using Acmil.Data.Contracts.Models.Items.Enums.SubClasses;
using System;

namespace Acmil.Data.Contracts.Models.Items.Enums
{
	/// <summary>
	/// Classes that an item can be categorized as.
	/// </summary>
	public enum ItemClass : byte
	{
		/// <summary>
		/// Class indicating a Consumable-type item.
		/// </summary>
		Consumable = 0,

		/// <summary>
		/// Class indicating a Container-type item.
		/// </summary>
		Container = 1,

		/// <summary>
		/// Class indicating a Weapon-type item.
		/// </summary>
		Weapon = 2,

		/// <summary>
		/// Class indicating a Gem-type item.
		/// </summary>
		Gem = 3,

		/// <summary>
		/// Class indicating an Armor-type item.
		/// </summary>
		Armor = 4,

		/// <summary>
		/// Class indicating a Reagent-type item. Although this is a valid class, the only player-obtainable
		/// item in it is "Ankh". If you're adding a new Reagent, add it as <see cref="ItemSubClassMiscellaneous.Reagent"/>
		/// under the <see cref="Miscellaneous"/> class.
		/// </summary>
		Reagent = 5,

		/// <summary>
		/// Class indicating a Projectile-type item.
		/// </summary>
		Projectile = 6,

		/// <summary>
		/// Class indicating a Trade-Goods-type item. These are various items made by crafting professions.
		/// </summary>
		TradeGoods = 7,

		[Obsolete]
		Generic = 8,

		/// <summary>
		/// Class indicating a Recipe-type item. These are items that teach skills or spells when used.
		/// </summary>
		Recipe = 9,

		[Obsolete]
		Money = 10,

		/// <summary>
		/// Class indicating a Quiver-type item.
		/// </summary>
		Quiver = 11,

		/// <summary>
		/// Class indicating a Quest-type item.
		/// </summary>
		Quest = 12,

		/// <summary>
		/// Class indicating a Key-type item.
		/// </summary>
		Key = 13,

		[Obsolete]
		Permanent = 14,

		/// <summary>
		/// Class indicating a Miscellaneous-type item.
		/// </summary>
		Miscellaneous = 15,

		/// <summary>
		/// Class indicating a Glyph-type item.
		/// </summary>
		Glyph = 16
	}
}
