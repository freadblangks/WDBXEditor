using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Enums;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria.Quests
{
	/// <summary>
	/// A requirement where the character must complete a certain number of total quests.
	/// </summary>
	public class CompleteQuestCountAchievementCriteria : BaseAchievementCriteria
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaType.CompleteQuestCount;

		/// <summary>
		/// The total number of quests the character must complete.
		/// </summary>
		[MySqlColumnName("Quantity")]
		public uint TotalQuestCount { get; set; }
	}
}
