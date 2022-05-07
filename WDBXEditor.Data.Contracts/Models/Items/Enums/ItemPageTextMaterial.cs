namespace WDBXEditor.Data.Contracts.Models.Items.Enums
{
	/// <summary>
	/// The different materials that the pages of a readable item can be made of.
	/// </summary>
	public enum ItemPageTextMaterial : byte
	{
		// TODO: Figure out what all these are and give good descriptions.

		/// <summary>
		/// The default page material.
		/// </summary>
		Default = 0,

		/// <summary>
		/// A page material that looks like parchment.
		/// </summary>
		Parchment = 1,

		/// <summary>
		/// A page material that looks like stone.
		/// </summary>
		Stone = 2,

		/// <summary>
		/// A page material that looks like marble.
		/// </summary>
		Marble = 3,
		Silver = 4,
		Bronze = 5,
		Valentine = 6,
		Illidan = 7
	}
}
