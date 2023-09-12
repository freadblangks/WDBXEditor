using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Enums;
using Acmil.Data.Contracts.Models.Arenas.Enums;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria.Arenas
{
	/// <summary>
	/// A requirement where the character must achieve a particular team arena rating.
	/// </summary>
	public class ReachArenaTeamRatingAchievementCriteria : BaseAchievementCriteria
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaType.ReachTeamRating;

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

		/// <summary>
		/// The rating required.
		/// </summary>
		[MySqlColumnName("Quantity")]
		public uint Rating { get; set; } = 0;
	}
}
