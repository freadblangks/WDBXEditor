using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Enums;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria
{
	/// <summary>
	/// A requirement where the character must complete a dungeon or raid of a specific player count.
	/// </summary>
	/// <remarks>
	/// Used for statistics.
	/// </remarks>
	internal class CompleteDungeonOrRaidOfMaxPlayerCountAchievementCriteria : BaseAchievementCriteria
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaType.CompleteDungeonOrRaidOfMaxPlayerCount;

		/// <summary>
		/// The group size of the instance (e.g. 5-man, 25-man).
		/// </summary>
		[MySqlColumnName("Asset_Id")]
		public uint GroupSize { get; set; }
	}
}
