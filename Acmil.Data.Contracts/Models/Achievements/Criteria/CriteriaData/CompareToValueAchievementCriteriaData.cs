using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Criteria.Enums;
using Acmil.Data.Contracts.Models.General.Enums;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria.CriteriaData
{
	/// <summary>
	/// Requirement data where the value in the `Asset_Id` column is compared to the value in 
	/// <see cref="RightHandSideValue"/> using the operator specified in <see cref="ComparisonType"/>.
	/// </summary>
	public class CompareToValueAchievementCriteriaData : BaseAchievementCriteriaData
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaDataType.CompareToValue;

		/// <summary>
		/// The value that should be on the right-hand side of the comparison.
		/// </summary>
		[MySqlColumnName("value1")]
		public uint RightHandSideValue { get; set; }

		/// <summary>
		/// A value indicating the type of comparison to make.
		/// </summary>
		/// <remarks>
		/// 0 : EQUALS<br/>
		/// 1 : GREATER THAN<br/>
		/// 2 : LESS THAN<br/>
		/// 3 : GREATER THAN OR EQUAL<br/>
		/// 4 : LESS THAN OR EQUAL
		/// </remarks>
		[MySqlColumnName("value2")]
		[EnumType(typeof(ComparisonType))]
		[AllowEnumConversionOverride(false)]
		public byte ComparisonType { get; set; }
	}
}
