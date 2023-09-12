using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Enums;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria.Deaths
{
	/// <summary>
	/// A requirement where the character must die in a dungeon or raid with a specific group size.
	/// </summary>
	/// <remarks>
	/// Used for Statistics.
	/// </remarks>
	public class DeathInDungeonAchievementCriteria : BaseAchievementCriteria
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaType.DeathInDungeon;

		// NOTE: The primary behavior of this is determined by the criteria ID.
		/// <summary>
		/// The group size of the instance (e.g. 5-man, 25-man)
		/// </summary>
		[MySqlColumnName("Asset_Id")]
		public uint GroupSize { get; set; }
	}
}
