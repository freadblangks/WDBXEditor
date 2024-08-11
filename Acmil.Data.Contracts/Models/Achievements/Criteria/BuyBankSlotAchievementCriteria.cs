using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Enums;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria
{
	/// <summary>
	/// A requirement where the character must buy one or more bank slots.
	/// </summary>
	public class BuyBankSlotAchievementCriteria : BaseAchievementCriteria
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaType.BuyBankSlot;

		/// <summary>
		/// The number of bank slots that need to be purchased.
		/// </summary>
		[MySqlColumnName("Quantity")]
		public uint Count { get; set; }
	}
}
