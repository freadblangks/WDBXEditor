using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Enums;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria
{
	/// <summary>
	/// A requirement where the character needs to pay to have their appearance changed at the Barber Shop.
	/// </summary>
	public class VisitBarberShopAchievementCriteria : BaseAchievementCriteria
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaType.VisitBarberShop;

		/// <summary>
		/// The number of times the character needs to pay to have their appearance changed at the Barber Shop.
		/// </summary>
		[MySqlColumnName("Quantity")]
		public uint Count { get; set; }
	}
}
