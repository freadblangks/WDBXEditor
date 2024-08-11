using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Criteria.Enums;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria.CriteriaData
{
	/// <summary>
	/// Requirement data where the target needs to be a specific gender.
	/// </summary>
	public class TargetIsGenderAchievementCriteriaData : BaseAchievementCriteriaData
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaDataType.TargetIsGender;

		/// <summary>
		/// The gender the target should be.
		/// <see langword="false"/> for male. <see langword="true"/> for female.
		/// </summary>
		[MySqlColumnName("value1")]
		public bool Gender { get; set; }
	}
}
