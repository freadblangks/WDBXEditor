namespace WDBXEditor.Data.Contracts.Models.Items.Enums.SubClasses
{
	/// <summary>
	/// Subclasses of the <see cref="ItemClass.Container"/> item class.
	/// </summary>
	public enum ItemSubClassContainer : byte
	{
		/// <summary>
		/// Subclass indicating a generic Bag.
		/// </summary>
		Bag = 0,

		/// <summary>
		/// Subclass indicating a Bag that can only be used to hold a Warlock's Soul Shards.
		/// </summary>
		SoulBag = 1,

		/// <summary>
		/// Subclass indicating a Bag that can only hold Herbs.
		/// </summary>
		HerbBag = 2,

		/// <summary>
		/// Subclass indicating a Bag that can only hold Enchanting Reagents, Recipes, Rods, etc.
		/// </summary>
		EnchantingBag = 3,

		/// <summary>
		/// Subclass indicating a Bag that can only hold Engineering Parts, Schematics, Trinkets, etc.
		/// </summary>
		EngineeringBag = 4,

		/// <summary>
		/// Subclass indicating a Bag that can only hold Gems (cut or uncut).
		/// </summary>
		GemBag = 5,

		/// <summary>
		/// Subclass indicating a Bag that can only hold Mining tools, Ore, Bars, etc.
		/// </summary>
		MiningBag = 6,

		/// <summary>
		/// Subclass indicating a Bag that can only hold Leather, Skinning Knives, etc.
		/// </summary>
		LeatherworkingBag = 7,

		/// <summary>
		/// Subclass indicating a Bag that can only hold Inscription supplies and tools.
		/// </summary>
		InscriptionBag = 8
	}
}
