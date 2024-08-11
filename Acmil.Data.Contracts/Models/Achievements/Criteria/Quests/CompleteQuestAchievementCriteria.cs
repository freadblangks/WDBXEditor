using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Enums;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria.Quests
{
	/// <summary>
	/// A requirement where the character must complete a particular quest a certain number of times.
	/// </summary>
	public class CompleteQuestAchievementCriteria : BaseAchievementCriteria
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaType.CompleteQuest;

		/// <summary>
		/// The ID of the quest that should be completed.
		/// </summary>
		[MySqlColumnName("Asset_Id")]
		public uint QuestId { get; set; }

		// TODO: Figure out why this is sometimes 0.
		/// <summary>
		/// The number of times the quest needs to be completed.
		/// </summary>
		[MySqlColumnName("Quantity")]
		public uint Count { get; set; }
	}
}
