namespace Acmil.Data.Contracts.Models.Maps.Enums
{
	/// <summary>
	/// The different types of instance maps.
	/// </summary>
	public enum InstanceType
	{
		/// <summary>
		/// Used for continents, transports, and everything else.
		/// </summary>
		Default = 0,

		/// <summary>
		/// The instance is a dungeon.
		/// </summary>
		Dungeon = 1,

		/// <summary>
		/// The instance is a raid.
		/// </summary>
		Raid = 2,

		/// <summary>
		/// The instance is a battleground.
		/// </summary>
		Battleground = 3,

		/// <summary>
		/// The instance is an arena.
		/// </summary>
		Arena = 4
	}
}
