using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Enums;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria
{
	/// <summary>
	/// A requirement where the character must earn an Achievement.
	/// </summary>
	public class CompleteAchievementAchievementCriteria : BaseAchievementCriteria
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaType.CompleteAchievement;

		/// <summary>
		/// The ID of the Achievement that must be earned.
		/// </summary>
		[MySqlColumnName("Asset_Id")]
		public ushort RequiredAchievementId { get; set; }
	}
}
