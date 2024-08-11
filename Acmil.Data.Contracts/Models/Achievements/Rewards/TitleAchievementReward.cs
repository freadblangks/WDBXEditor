using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Types.Primitives;

namespace Acmil.Data.Contracts.Models.Achievements.Rewards
{
	/// <summary>
	/// An Achievement Reward where the player receives a title.
	/// </summary>
	public class TitleAchievementReward : BaseAchievementReward
	{
		/// <summary>
		/// The ID of the title awarded to Alliance players who earn the associated Achievement.
		/// </summary>
		[MySqlColumnName("TitleA")]
		public UInt24 TitleAlliance { get; set; }

		/// <summary>
		/// The ID of the title awarded to Horde players who earn the associated Achievement.
		/// </summary>
		[MySqlColumnName("TitleH")]
		public UInt24 TitleHorde { get; set; }

		public override string BuildRewardDescription(string rewardName) => $"Title: {rewardName}";
	}
}
