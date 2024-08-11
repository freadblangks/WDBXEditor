using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Enums;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria
{
	/// <summary>
	/// A requirement where the character must fall from a specific height without dying.
	/// </summary>
	public class FallWithoutDyingAchievementCriteria : BaseAchievementCriteria
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaType.FallWithoutDying;

		/// <summary>
		/// The height the character must fall from.
		/// </summary>
		/// <remarks>
		/// Measured in hundredths of yards (e.g. value of 6500 translates to 65 yards).
		/// </remarks>
		[MySqlColumnName("Quantity")]
		public uint FallHeight { get; set; }
	}
}
