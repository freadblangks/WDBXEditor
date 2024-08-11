using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Enums;
using Acmil.Data.Contracts.Models.Arenas.Enums;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria.Arenas
{
	/// <summary>
	/// A requirement where the character must reach a highest team rating for Arenas.
	/// </summary>
	public class HighestArenaTeamRatingAchievementCriteria : BaseAchievementCriteria
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaType.HighestTeamRating;

		/// <summary>
		/// The arena team type.
		/// </summary>
		/// <remarks>
		/// For a list of supported values, see <see cref="ArenaTeamType"/>.
		/// </remarks>
		[MySqlColumnName("Asset_Id")]
		[EnumType(typeof(ArenaTeamType))]
		[AllowEnumConversionOverride(true)]
		public uint TeamType { get; set; }
	}
}
