using Acmil.Data.Contracts.Models.Items.Enums;
using System;

namespace Acmil.Data.Contracts.Models.Items.Enums.SubClasses
{
	/// <summary>
	/// Subclasses of the <see cref="ItemClass.Projectile"/> item class.
	/// </summary>
	public enum ItemSubClassProjectile : byte
	{
		[Obsolete]
		Wand = 0,

		[Obsolete]
		Bolt = 1,

		/// <summary>
		/// Subclass indicating an Arrow-type Projectile.
		/// </summary>
		Arrow = 2,

		/// <summary>
		/// Subclass indicating a Bullet-type Projectile.
		/// </summary>
		Bullet = 3,

		[Obsolete]
		Thrown = 4,
	}
}
