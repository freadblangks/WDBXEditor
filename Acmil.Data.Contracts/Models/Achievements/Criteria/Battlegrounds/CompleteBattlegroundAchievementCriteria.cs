using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Enums;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria.Battlegrounds
{
	/// <summary>
	/// A requirement where the character must complete a particular battleground.
	/// </summary>
	/// <remarks>
	/// Used by Statistics.
	/// </remarks>
	public class CompleteBattlegroundAchievementCriteria : BaseAchievementCriteria
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaType.CompleteBattleground;

		/// <summary>
		/// The Map ID of the battleground.
		/// </summary>
		[MySqlColumnName("Asset_Id")]
		public uint MapId { get; set; }
	}
}
