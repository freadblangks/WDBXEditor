using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Criteria.Enums;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria.CriteriaData
{
	/// <summary>
	/// Requirement data where a targeted player's health needs to be at or below a certain percentage.
	/// </summary>
	public class TargetPlayerAtOrLessThanXPercentHealthAchievementCriteriaData : BaseAchievementCriteriaData
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaDataType.TargetPlayerAtOrLessThanXPercentHealth;

		/// <summary>
		/// The percentage that the target player's health needs to be at or below.
		/// </summary>
		[MySqlColumnName("value1")]
		public byte Percent { get; set; }
	}
}
