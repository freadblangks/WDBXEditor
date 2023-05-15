using Acmil.Data.Contracts.Models.Items.Enums;

namespace Acmil.Data.Contracts.Models.Items.Enums.SubClasses
{
	/// <summary>
	/// Subclasses of the <see cref="ItemClass.Recipe"/> item class.
	/// </summary>
	public enum ItemSubClassRecipe : byte
	{
		/// <summary>
		/// Subclass indicating a Book-type recipe. These are for Inscription recipes (Techniques),
		/// books that teach spells, and anything that doesn't fit into any of the other subclasses.
		/// </summary>
		Book = 0,

		/// <summary>
		/// Subclass indicating a Leatherworking recipe (Pattern).
		/// </summary>
		Leatherworking = 1,

		/// <summary>
		/// Subclass indicating a Tailoring recipe (Pattern).
		/// </summary>
		Tailoring = 2,

		/// <summary>
		/// Subclass indicating an Engineering recipe (Schematic).
		/// </summary>
		Engineering = 3,

		/// <summary>
		/// Subclass indicating a Blacksmithing recipe (Plans).
		/// </summary>
		Blacksmithing = 4,

		/// <summary>
		/// Subclass indicating a Cooking recipe.
		/// </summary>
		Cooking = 5,

		/// <summary>
		/// Subclass indicating an Alchemy recipe.
		/// </summary>
		Alchemy = 6,

		/// <summary>
		/// Subclass indicating a First Aid recipe (Manual).
		/// </summary>
		FirstAid = 7,

		/// <summary>
		/// Subclass indicating an Enchanting recipe (Formula).
		/// </summary>
		Enchanting = 8,

		/// <summary>
		/// Subclass indicating a Fishing recipe.
		/// </summary>
		Fishing = 9,

		/// <summary>
		/// Subclass indicating a Jewelcrafting recipe (Design).
		/// </summary>
		Jewelcrafting = 10,
	}
}
