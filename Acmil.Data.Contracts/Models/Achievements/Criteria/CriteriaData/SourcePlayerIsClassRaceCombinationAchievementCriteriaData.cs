using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Criteria.Enums;
using Acmil.Data.Contracts.Models.Characters.Enums;
using Acmil.Data.Contracts.Types.Primitives;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria.CriteriaData
{
	/// <summary>
	/// Requirement data where the player needs to be of a specific class-race combination.
	/// </summary>
	public class SourcePlayerIsClassRaceCombinationAchievementCriteriaData : BaseAchievementCriteriaData
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaDataType.SourcePlayerIsClassRaceCombination;

		/// <summary>
		/// The ID for the expected player's class.
		/// </summary>
		/// <remarks>
		/// For a full list of supported values, see <see cref="PlayableClass"/>.
		/// </remarks>
		[MySqlColumnName("value1")]
		[EnumType(typeof(PlayableClass))]
		[AllowEnumConversionOverride(true)]
		public UInt24 ClassId { get; set; }

		/// <summary>
		/// The ID for the expected player's race.
		/// </summary>
		/// <remarks>
		/// For a full list of supported values, see <see cref="PlayableRace"/>.
		/// </remarks>
		[MySqlColumnName("value2")]
		[EnumType(typeof(PlayableRace))]
		[AllowEnumConversionOverride(true)]
		public UInt24 RaceId { get; set; }
	}
}
