using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Criteria.Enums;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria.CriteriaData
{
	/// <summary>
	/// Requirement data where the player's team must win a battleground with a score within a defined range.
	/// </summary>
	public class BattlegroundTeamScoreIsWithinRangeAchievementCriteriaData : BaseAchievementCriteriaData
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaDataType.BattlegroundTeamScoreIsWithinRange;

		/// <summary>
		/// The value that the player's team's score must be greater than or equal to.
		/// </summary>
		[MySqlColumnName("value1")]
		public uint MinScore { get; set; }

		/// <summary>
		/// The value that the player's team's score must be less than or equal to.
		/// </summary>
		[MySqlColumnName("value2")]
		public uint MaxScore { get; set; }
	}
}