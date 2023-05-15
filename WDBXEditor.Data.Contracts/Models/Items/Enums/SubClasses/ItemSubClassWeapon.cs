using Acmil.Data.Contracts.Models.Items.Enums;
using System;

namespace Acmil.Data.Contracts.Models.Items.Enums.SubClasses
{
	/// <summary>
	/// Subclasses of the <see cref="ItemClass.Weapon"/> item class.
	/// </summary>
	public enum ItemSubClassWeapon : byte
	{
		/// <summary>
		/// Subclass indicating a one-handed Axe-type weapon.
		/// </summary>
		AxeOneHanded = 0,

		/// <summary>
		/// Subclass indicating a two-handed Axe-type weapon.
		/// </summary>
		AxeTwoHanded = 1,

		/// <summary>
		/// Subclass indicating a Bow-type weapon.
		/// </summary>
		Bow = 2,

		/// <summary>
		/// Subclass indicating a Gun-type weapon.
		/// </summary>
		Gun = 3,

		/// <summary>
		/// Subclass indicating a one-handed Mace-type weapon.
		/// </summary>
		MaceOneHanded = 4,

		/// <summary>
		/// Subclass indicating a two-handed Mace-type weapon.
		/// </summary>
		MaceTwoHanded = 5,

		/// <summary>
		/// Subclass indicating a Polearm-type weapon.
		/// </summary>
		Polearm = 6,

		/// <summary>
		/// Subclass indicating a one-handed Sword-type weapon.
		/// </summary>
		SwordOneHanded = 7,

		/// <summary>
		/// Subclass indicating a two-handed Sword-type weapon.
		/// </summary>
		SwordTwoHanded = 8,

		[Obsolete]
		Obsolete = 9,

		/// <summary>
		/// Subclass indicating a Staff-type weapon.
		/// </summary>
		Staff = 10,

		/// <summary>
		/// Subclass indicating an Exotic-type weapon.
		/// </summary>
		/// <remarks>
		/// All existing items in this subclass are not normally obtainable by players. The word "Exotic" is not shown in the tooltip.
		/// </remarks>
		Exotic = 11,

		// TODO: Figure out if this is 2H or something.
		Exotic2 = 12,

		/// <summary>
		/// Subclass indicating a Fist Weapon.
		/// </summary>
		FistWeapon = 13,

		/// <summary>
		/// Subclass indicating a miscellaneous-type weapon (e.g. Blacksmith Hammer, Mining Pick).
		/// </summary>
		Miscellaneous = 14,

		/// <summary>
		/// Subclass indicating a Dagger-type weapon.
		/// </summary>
		Dagger = 15,

		/// <summary>
		/// Subclass indicating a Thrown-type weapon.
		/// </summary>
		Thrown = 16,

		/// <summary>
		/// Subclass indicating a Spear-type weapon.
		/// </summary>
		Spear = 17,

		/// <summary>
		/// Subclass indicating a Crossbow-type weapon.
		/// </summary>
		Crossbow = 18,

		/// <summary>
		/// Subclass indicating a Wand-type weapon.
		/// </summary>
		Wand = 19,

		/// <summary>
		/// Subclass indicating a Fishing Pole.
		/// </summary>
		FishingPole = 20
	}
}
