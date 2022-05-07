namespace WDBXEditor.Data.Contracts.Models.Items.Enums
{
	/// <summary>
	/// The possible damage types for a melee weapon to deal on hit.
	/// </summary>
	public enum ItemDamageType : byte
	{
		/// <summary>
		/// The item does physical damage on hit.
		/// </summary>
		Physical = 0,

		/// <summary>
		/// The item does Holy damage on hit.
		/// </summary>
		Holy = 1,

		/// <summary>
		/// The item does Fire damage on hit.
		/// </summary>
		Fire = 2,

		/// <summary>
		/// The item does Nature damage on hit.
		/// </summary>
		Nature = 3,

		/// <summary>
		/// The item does Frost damage on hit.
		/// </summary>
		Frost = 4,

		/// <summary>
		/// The item does Shadow damage on hit.
		/// </summary>
		Shadow = 5,

		/// <summary>
		/// The item does Arcane damage on hit.
		/// </summary>
		Arcane = 6
	}
}
