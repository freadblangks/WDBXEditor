using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Enums;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria
{
	/// <summary>
	/// A requirement where the character must reach a specified level.
	/// </summary>
	public class ReachLevelAchievementCriteria : BaseAchievementCriteria
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaType.ReachLevel;

		/// <summary>
		/// The level the character must reach.
		/// </summary>
		[MySqlColumnName("Quantity")]
		public uint Level { get; set; }
	}
}
