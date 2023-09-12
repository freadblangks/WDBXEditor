using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Enums;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria.Reputations
{
	/// <summary>
	/// A requirement where the character needs to have a certain number of Factions at Exalted.
	/// </summary>
	public class HaveExaltedReputationsAchievementCriteria : BaseAchievementCriteria
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaType.GainExaltedReputation;

		/// <summary>
		/// The number of factions the character needs to be at Exalted with.
		/// </summary>
		[MySqlColumnName("Quantity")]
		public uint Count { get; set; }
	}
}
