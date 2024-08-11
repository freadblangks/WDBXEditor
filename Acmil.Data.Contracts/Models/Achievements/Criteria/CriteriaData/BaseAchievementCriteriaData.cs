using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Criteria.Enums;
using Acmil.Data.Contracts.Types.Primitives;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria.CriteriaData
{
	/// <summary>
	/// Base class for Achievement Criteria Data.
	/// </summary>
	public abstract class BaseAchievementCriteriaData
	{
		/// <summary>
		/// The ID of the Achievement Criteria that the data is associated with.
		/// </summary>
		[MySqlColumnName("criteria_id")]
		public Int24 CriteriaId { get; set; }

		/// <summary>
		/// A value indicating the type of criteria data.
		/// </summary>
		/// <remarks>
		/// For a full list of supported values, see <see cref="AchievementCriteriaDataType"/>.
		/// </remarks>
		[MySqlColumnName("type")]
		[EnumType(typeof(AchievementCriteriaDataType))]
		[AllowEnumConversionOverride(false)]
		public abstract byte Type { get; internal set; }
	}
}
