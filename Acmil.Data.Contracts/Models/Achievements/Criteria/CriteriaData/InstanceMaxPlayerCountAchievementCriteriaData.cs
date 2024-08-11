using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Criteria.Enums;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria.CriteriaData
{
	/// <summary>
	/// Requirement data where there need to be a certain number or fewer players in the instance.
	/// </summary>
	public class InstanceMaxPlayerCountAchievementCriteriaData : BaseAchievementCriteriaData
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaDataType.InstanceMaxPlayerCount;

		/// <summary>
		/// The max number of players in the instance.
		/// The amount must be less than or equal to this number.
		/// </summary>
		/// <remarks>
		/// GMs do not count towards the total.
		/// </remarks>
		[MySqlColumnName("value1")]
		public uint MaxPlayerCount { get; set; }
	}
}
