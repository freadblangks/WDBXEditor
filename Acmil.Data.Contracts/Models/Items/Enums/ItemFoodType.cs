namespace Acmil.Data.Contracts.Models.Items.Enums
{
	/// <summary>
	/// Different types of food that an item can be. This is used for feeding Hunter pets.
	/// </summary>
	public enum ItemFoodType : byte
	{
		/// <summary>
		/// The item is not food.
		/// </summary>
		Inedible = 0,

		/// <summary>
		/// The item is considered meat.
		/// </summary>
		Meat = 1,

		/// <summary>
		/// The item is considered fish.
		/// </summary>
		Fish = 2,

		/// <summary>
		/// The item is considered cheese.
		/// </summary>
		Cheese = 3,

		/// <summary>
		/// The item is considered bread.
		/// </summary>
		Bread = 4,

		/// <summary>
		/// The item is considered fungus.
		/// </summary>
		Fungus = 5,

		/// <summary>
		/// The item is considered fruit.
		/// </summary>
		Fruit = 6,

		/// <summary>
		/// The item is considered raw meat.
		/// </summary>
		RawMeat = 7,

		/// <summary>
		/// The item is considered raw fish.
		/// </summary>
		RawFish = 8
	}
}
