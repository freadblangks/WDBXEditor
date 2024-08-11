using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Enums;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria.Quests
{
	/// <summary>
	/// A requirement where the character must complete a daily quest once a day for a consecutive number of days.
	/// </summary>
	public class CompleteDailyQuestDailyAchievementCriteria : BaseAchievementCriteria
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaType.CompleteDailyQuestDaily;

		/// <summary>
		/// The number of consecutive days the character must complete a daily quest.
		/// </summary>
		[MySqlColumnName("Quantity")]
		public uint NumberOfDays { get; set; }
	}
}
