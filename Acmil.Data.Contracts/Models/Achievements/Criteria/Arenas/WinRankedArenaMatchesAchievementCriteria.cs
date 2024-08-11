using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Criteria.Enums;
using Acmil.Data.Contracts.Models.Achievements.Enums;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria.Arenas
{
	/// <summary>
	/// A requirement where the character must win a certain number of ranked Arena matches.
	/// </summary>
	public class WinRankedArenaMatchesAchievementCriteria : BaseAchievementCriteria
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaType.WinRatedArena;

		/// <summary>
		/// The number of arena wins required.
		/// </summary>
		[MySqlColumnName("Quantity")]
		public uint Count { get; set; }

		/// <summary>
		/// A condition required for the character to be eligible to meet the criteria.
		/// </summary>
		/// <remarks>
		/// For known values, see <see cref="AchievementCriteriaCondition"/>.
		/// It's unknown how many are supported.
		/// </remarks>
		[MySqlColumnName("Start_Event")]
		[EnumType(typeof(AchievementCriteriaCondition))]
		public uint RequirementStartCondition { get; set; } = 0;

		/// <summary>
		/// A condition that causes the character to fail to meet the criteria.
		/// </summary>
		/// <remarks>
		/// For known values, see <see cref="AchievementCriteriaCondition"/>.
		/// It's unknown how many are supported.
		/// </remarks>
		[MySqlColumnName("Fail_Event")]
		[EnumType(typeof(AchievementCriteriaCondition))]
		public uint RequirementFailCondition { get; set; } = 0;
	}
}
