using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Enums;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria.Battlegrounds
{
	/// <summary>
	/// A requirement where the character must win a particular battleground a certain number of times.
	/// </summary>
	public class WinBattlegroundAchievementCriteria : BaseAchievementCriteria
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaType.WinBattleground;

		/// <summary>
		/// The Map ID of the battleground.
		/// </summary>
		[MySqlColumnName("Asset_Id")]
		public uint MapId { get; set; }

		/// <summary>
		/// The number of times the character must win the battleground.
		/// </summary>
		[MySqlColumnName("Quantity")]
		public uint WinCount { get; set; }
	}
}
