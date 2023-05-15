using Acmil.Data.Contracts.Models.Items.Enums;

namespace Acmil.Data.Contracts.Models.Items.Enums.SubClasses
{
	/// <summary>
	/// Subclasses of the <see cref="ItemClass.Consumable"/> item class.
	/// </summary>
	public enum ItemSubClassConsumable : byte
	{
		/// <summary>
		/// Subclass indicating a Generic Consumable.
		/// </summary>
		Consumable = 0,

		/// <summary>
		/// Subclass indicating a Potion.
		/// </summary>
		/// <remarks>
		/// Potions are similar to Elixirs, but their effects tend to be instantaneous (e.g. Health Potions) or have short durations.
		/// </remarks>
		Potion = 1,

		/// <summary>
		/// Subclass indicating an Elixir.
		/// </summary>
		/// <remarks>
		/// Elixirs are similar to Potions, but they tend to have longer durations.
		/// </remarks>
		Elixir = 2,

		/// <summary>
		/// Subclass indicating a Flask.
		/// </summary>
		/// <remarks>
		/// Flasks are different from Potions and Elixirs in that their effect does not disappear upon death.
		/// </remarks>
		Flask = 3,

		/// <summary>
		/// Subclass indicating a Scroll.
		/// </summary>
		Scroll = 4,

		/// <summary>
		/// Subclass indicating a Food or Drink item.
		/// </summary>
		FoodAndDrink = 5,

		/// <summary>
		/// Subclass indicating an item used to apply an enchantment to a player's gear
		/// (e.g. Scroll of Enchant x, Earthen Leg Armor).
		/// </summary>
		ItemEnhancement = 6,

		/// <summary>
		/// Subclass indicating a Bandage.
		/// </summary>
		Bandage = 7,

		/// <summary>
		/// Subclass indicating a consumable that doesn't fit into the other categories, I guess.
		/// </summary>
		Other = 8
	}
}
