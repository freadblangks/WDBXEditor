using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Criteria.Enums;
using Acmil.Data.Contracts.Models.Dungeons.Enums;
using Acmil.Data.Contracts.Models.Raids.Enums;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria.CriteriaData
{
	/// <summary>
	/// Requirement data where the player needs to be in an instance at a certain difficulty level.
	/// </summary>
	public class InstanceDifficultyAchievementCriteriaData : BaseAchievementCriteriaData
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaDataType.InstanceDifficulty;

		/// <summary>
		/// A value indicating the instance difficulty.
		/// </summary>
		/// <remarks>
		/// For supported values, see <see cref="DungeonDifficulty"/> and <see cref="RaidDifficulty"/>.
		/// </remarks>
		[MySqlColumnName("value1")]
		[EnumType(typeof(DungeonDifficulty))]
		[EnumType(typeof(RaidDifficulty))]
		public byte Difficulty { get; set; }
	}
}
