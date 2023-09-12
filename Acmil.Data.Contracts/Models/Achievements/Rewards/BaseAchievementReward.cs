using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Types.Primitives;

namespace Acmil.Data.Contracts.Models.Achievements.Rewards
{
	/// <summary>
	/// Base class for Achievement rewards.
	/// </summary>
	public abstract class BaseAchievementReward
	{
		/// <summary>
		/// The ID of the Achievement the reward is for.
		/// </summary>
		[MySqlColumnName("ID")]
		public UInt24 AchievementId { get; set; }

		/// <summary>
		/// Builds a string to use for the reward description on the associated Achievement
		/// if no value is provided for the <see cref="Achievement.RewardText"/>.
		/// </summary>
		/// <param name="rewardName">The name of the reward (e.g. item, title).</param>
		/// <returns>A string to use for the reward description on the associated Achievement.</returns>
		public abstract string BuildRewardDescription(string rewardName);
	}
}
