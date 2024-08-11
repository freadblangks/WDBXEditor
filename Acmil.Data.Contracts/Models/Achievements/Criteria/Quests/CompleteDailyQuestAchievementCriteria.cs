using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Enums;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria.Quests
{
	/// <summary>
	/// A requirement where the character must complete a total number of daily quests.
	/// </summary>
	public class CompleteDailyQuestAchievementCriteria : BaseAchievementCriteria
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaType.CompleteDailyQuest;

		/// <summary>
		/// The number of daily quests the character must complete.
		/// </summary>
		[MySqlColumnName("Quantity")]
		public uint Count { get; set; }
	}
}
