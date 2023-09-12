using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Enums;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria.Deaths
{
	/// <summary>
	/// A requirement where the character must be killed by a creature.
	/// </summary>
	/// <remarks>
	/// Used by Statistics.
	/// </remarks>
	public class KilledByCreatureAchievementCriteria : BaseAchievementCriteria
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaType.KilledByCreature;

		/// <summary>
		/// The ID from the creature's Creature Template.
		/// </summary>
		[MySqlColumnName("Asset_Id")]
		public uint CreatureId { get; set; }
	}
}
