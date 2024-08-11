using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Enums;
using Acmil.Data.Contracts.Models.Maps.Enums;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria.Arenas
{
	// TODO: Figure out what the Quantity column does.
	/// <summary>
	/// A requirement where the character must win an Arena match.
	/// </summary>
	public class WinArenaAchievementCriteria : BaseAchievementCriteria
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaType.WinArena;

		/// <summary>
		/// The Map ID of the Arena where the win must take place.
		/// </summary>
		/// <remarks>
		/// For a list of supported values, see <see cref="ArenaMap"/>.
		/// </remarks>
		[MySqlColumnName("Asset_Id")]
		[EnumType(typeof(ArenaMap))]
		[AllowEnumConversionOverride(true)]
		public uint ArenaMapId { get; set; }
	}
}
