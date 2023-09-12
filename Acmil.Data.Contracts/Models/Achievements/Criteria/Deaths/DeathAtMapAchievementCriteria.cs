using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Enums;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria.Deaths
{
	/// <summary>
	/// A requirement where the character must die in a particular map.
	/// </summary>
	/// <remarks>
	/// Used by Statistics.
	/// </remarks>
	public class DeathAtMapAchievementCriteria : BaseAchievementCriteria
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaType.DeathAtMap;

		/// <summary>
		/// The ID of the map where the character must die.
		/// </summary>
		[MySqlColumnName("Asset_Id")]
		public uint MapId { get; set; }
	}
}
