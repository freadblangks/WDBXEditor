using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Enums;
using Acmil.Data.Contracts.Models.General.Enums;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria.Deaths
{
	/// <summary>
	/// A requirement where the character must die due to a specific kind of environmental damage.
	/// </summary>
	public class DeathsFromEnvironmentalDamageAchievementCriteria : BaseAchievementCriteria
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaType.DeathsFrom;

		/// <summary>
		/// An ID identifying the source of environmental damage.
		/// </summary>
		/// <remarks>
		/// For a list of known values, see <see cref="EnvironmentalDamage"/>
		/// </remarks>
		[MySqlColumnName("Asset_Id")]
		[EnumType(typeof(EnvironmentalDamage))]
		public uint EnvironmentalDamageId { get; set; }
	}
}
