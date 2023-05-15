namespace Acmil.Data.Contracts.Models.Items.Enums
{
	/// <summary>
	/// Different types of ammo that ranged weapons can used.
	/// </summary>
	public enum ItemAmmoType : byte
	{
		/// <summary>
		/// The item does not use ammo.
		/// </summary>
		None = 0,

		/// <summary>
		/// The item uses Arrows.
		/// </summary>
		Arrows = 2,

		/// <summary>
		/// The item uses Bullets.
		/// </summary>
		Bullets = 3
	}
}
