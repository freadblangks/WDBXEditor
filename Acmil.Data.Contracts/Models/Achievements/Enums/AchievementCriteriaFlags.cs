using Acmil.Data.Contracts.Attributes;

namespace Acmil.Data.Contracts.Models.Achievements.Enums
{
	// TODO: Verify that these actually do anything. Like, that they're not just descriptive.
	/// <summary>
	/// Flags for modifying the behavior of Achievement criteria.
	/// </summary>
	[Flags]
	public enum AchievementCriteriaFlags
	{
		/// <summary>
		/// Criteria progress is shown with a progress bar.
		/// </summary>
		ProgressBar = 0x01,

		/// <summary>
		/// Criteria progress is hidden.
		/// </summary>
		Hidden = 0x02,

		/// <summary>
		/// Achievement is failed if the criteria are not met.
		/// </summary>
		FailAchievement = 0x04,

		// TODO
		/// <summary>
		/// 
		/// </summary>
		ResetOnStart = 0x08,

		/// <summary>
		/// Criterion is a date requirement.
		/// </summary>
		IsDate = 0x10,

		/// <summary>
		/// Criteria progress shows as currency.
		/// </summary>
		IsMoney = 0x20,

		[NotImplemented]
		IsAchievementId = 0x40,

		[NotImplemented]
		QuantityIsCapped = 0x80
	}
}
