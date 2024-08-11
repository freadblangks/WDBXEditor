using Acmil.Data.Contracts.Models.Items.Enums;
using System;

namespace Acmil.Data.Contracts.Models.Items.Enums.SubClasses
{
	/// <summary>
	/// Subclasses of the <see cref="ItemClass.Armor"/> item class.
	/// </summary>
	public enum ItemSubClassArmor : byte
	{
		/// <summary>
		/// Subclass indicating an uncategorized Armor Item.
		/// </summary>
		Miscellaneous = 0,

		/// <summary>
		/// Subclass indicating a Cloth Armor item.
		/// </summary>
		Cloth = 1,

		/// <summary>
		/// Subclass indicating a Leather Armor item.
		/// </summary>
		Leather = 2,

		/// <summary>
		/// Subclass indicating a Mail Armor item.
		/// </summary>
		Mail = 3,

		/// <summary>
		/// Subclass indicating a Plate Armor item.
		/// </summary>
		Plate = 4,

		[Obsolete]
		Buckler = 5,

		/// <summary>
		/// Subclass indicating a Shield-type item.
		/// </summary>
		Shield = 6,

		/// <summary>
		/// Subclass indicating a Libram-type item.
		/// </summary>
		Libram = 7,

		/// <summary>
		/// Subclass indicating an Idol-type item.
		/// </summary>
		Idol = 8,

		/// <summary>
		/// Subclass indicating a Totem-type item.
		/// </summary>
		Totem = 9,

		/// <summary>
		/// Subclass indicating a Sigil-type item.
		/// </summary>
		Sigil = 10,
	}
}
