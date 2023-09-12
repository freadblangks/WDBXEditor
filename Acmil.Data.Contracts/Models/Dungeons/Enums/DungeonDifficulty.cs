using Acmil.Data.Contracts.Attributes;

namespace Acmil.Data.Contracts.Models.Dungeons.Enums
{
	/// <summary>
	/// Different levels of Dungeon difficulty.
	/// </summary>
	public enum DungeonDifficulty
	{
		/// <summary>
		/// Normal difficulty.
		/// </summary>
		Normal = 0,

		/// <summary>
		/// Heroic difficulty.
		/// </summary>
		Heroic = 1,

		/// <summary>
		/// (NOT IMPLEMENTED) Epic difficulty.
		/// </summary>
		[NotImplemented]
		Epic = 2,
	}
}
