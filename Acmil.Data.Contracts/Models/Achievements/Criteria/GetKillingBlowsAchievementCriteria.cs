using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Enums;
using Acmil.Data.Contracts.Models.Battlegrounds.Enums;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria
{
	// TODO: Do more research into how this one actually works.
	// There are more fields according to https://wowdev.wiki/DB/Achievement_Criteria,
	// but things seem inconsistent.
	/// <summary>
	/// A requirement where the character must get a particular amount of killing blows.
	/// </summary>
	public class GetKillingBlowsAchievementCriteria : BaseAchievementCriteria
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaType.GetKillingBlows;

		/// <summary>
		/// The number of killing blows the player needs to get.
		/// </summary>
		[MySqlColumnName("Quantity")]
		public uint Count { get; set; }

		/// <summary>
		/// If the requirement is for kills in a battleground,
		/// the status the battleground instance must be in
		/// for the character to be eligible for the requirement.
		/// </summary>
		/// <remarks>
		/// For a list of supported values, see <see cref="BattlegroundStatus"/>.
		/// </remarks>
		[MySqlColumnName("Start_Event")]
		[EnumType(typeof(BattlegroundStatus))]
		public uint BattlegroundRequiredStatus { get; set; }

		/// <summary>
		/// If the requirement is for kills in a battleground,
		/// the ID of the map where the killing blows must take place.
		/// </summary>
		[MySqlColumnName("Start_Asset")]
		public uint BattlegroundRequiredMapId { get; set; }
	}
}
