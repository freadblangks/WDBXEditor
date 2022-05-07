using System;

namespace WDBXEditor.Data.Contracts.Models.Items.Enums.SubClasses
{
	/// <summary>
	/// Subclasses of the <see cref="ItemClass.Quiver"/> item class.
	/// </summary>
	public enum ItemSubClassQuiver : byte
	{
		[Obsolete]
		QuiverObsolete = 0,

		[Obsolete]
		QuiverObsolete2 = 1,

		/// <summary>
		/// Subclass indicating a Quiver-type item, a bag that can hold Arrows.
		/// </summary>
		Quiver = 2,

		/// <summary>
		/// Suclass indicating an Ammo-Pouch-type item, a bag that can hold Bullets.
		/// </summary>
		AmmoPouch = 3
	}
}
