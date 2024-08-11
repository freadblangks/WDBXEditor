using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Enums;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria.Arenas
{
	/// <summary>
	/// A requirement where the character must participate in an Arena match.
	/// </summary>
	public class PlayArenaAchievementCriteria : BaseAchievementCriteria
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaType.PlayArena;

		/// <summary>
		/// The Map ID of the Arena where the win must take place.
		/// </summary>
		/// <remarks>
		/// For a list of supported values, see <see cref="ArenaMap"/>.
		/// </remarks>
		[MySqlColumnName("Asset_Id")]
		public uint ArenaMapId { get; set; }
	}
}
