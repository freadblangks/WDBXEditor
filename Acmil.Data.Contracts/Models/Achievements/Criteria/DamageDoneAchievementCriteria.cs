using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Criteria.Enums;
using Acmil.Data.Contracts.Models.Achievements.Enums;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria
{
	/// <summary>
	/// A requirement where the character must deal a certain total amount of damage.
	/// </summary>
	public class DamageDoneAchievementCriteria : BaseAchievementCriteria
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaType.DamageDone;

		/// <summary>
		/// The amount of total damage that needs to be done.
		/// </summary>
		[MySqlColumnName("Quantity")]
		public uint Amount { get; set; }

		/// <summary>
		/// A value indicating a type of additional criterion to evaluate.
		/// </summary>
		/// <remarks>
		/// Currently, the only values supported by AzerothCore are <see cref="AchievementCriteriaCondition.None"/>
		/// and <see cref="AchievementCriteriaCondition.Map"/>.
		/// </remarks>
		[MySqlColumnName("Start_Event")]
		[EnumType(typeof(AchievementCriteriaCondition))]
		public uint AdditionalRequirementType { get; set; }

		/// <summary>
		/// An ID to evaluate using the condition specified in <see cref="AdditionalRequirementType"/>.
		/// </summary>
		/// <remarks>
		/// When <see cref="AdditionalRequirementType"/> is <see cref="AchievementCriteriaCondition.Map"/>,
		/// this is the ID of the map.
		/// </remarks>
		[MySqlColumnName("Start_Asset")]
		public uint AdditionalRequirementAsset { get; set; }
	}
}
