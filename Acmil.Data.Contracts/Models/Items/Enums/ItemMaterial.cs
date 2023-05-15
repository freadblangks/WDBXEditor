namespace Acmil.Data.Contracts.Models.Items.Enums
{
	/// <summary>
	/// Different materials an item can be made of.
	/// </summary>
	/// TODO: Check if this affects the sound of a wearer's walk cycle.
	public enum ItemMaterial
	{
		/// <summary>
		/// Used for consumables like food, reagents, etc.
		/// </summary>
		/// TODO: Check if this makes the item sound a certain way.
		Consumables = -1,

		/// <summary>
		/// The item's material has not been defined
		/// </summary>
		/// TODO: Check if this makes the item sound a certain way.
		NotDefined = 0,

		/// <summary>
		/// The item is made of metal.
		/// </summary>
		Metal = 1,

		/// <summary>
		/// The item is made of wood.
		/// </summary>
		Wood = 2,

		/// <summary>
		/// The item is or contains liquid.
		/// </summary>
		Liquid = 3,

		/// <summary>
		/// The item is jewelry.
		/// </summary>
		Jewelry = 4,

		/// <summary>
		/// The item is a chain or is made of chains.
		/// </summary>
		Chain = 5,

		/// <summary>
		/// The item is made of plate metal.
		/// </summary>
		Plate = 6,

		/// <summary>
		/// The item is made of cloth.
		/// </summary>
		Cloth = 7,

		/// <summary>
		/// The item is made of leather.
		/// </summary>
		Leather = 8
	}
}
