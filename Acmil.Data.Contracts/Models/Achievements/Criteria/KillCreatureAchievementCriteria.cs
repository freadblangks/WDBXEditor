using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Enums;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria
{
	/// <summary>
	/// A requirement where the character must kill a particular creature a certain number of times.
	/// </summary>
	public class KillCreatureAchievementCriteria : BaseAchievementCriteria
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaType.KillCreature;

		/// <summary>
		/// The Creature Template for the creature that should be killed.
		/// </summary>
		[MySqlColumnName("Asset_Id")]
		public uint CreatureId { get; set; }

		/// <summary>
		/// The number of times the creature should be killed.
		/// </summary>
		[MySqlColumnName("Quantity")]
		public uint Count { get; set; }
	}
}
