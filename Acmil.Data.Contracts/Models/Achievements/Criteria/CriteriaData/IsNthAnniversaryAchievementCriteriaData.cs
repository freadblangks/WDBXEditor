using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Criteria.Enums;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria.CriteriaData
{
	/// <summary>
	/// Requirement data where the current date needs to be the Nth anniversary of the server's
	/// configured birthday.
	/// </summary>
	/// <remarks>
	/// The server's birthday is defined in seconds since epoch by the BirthdayTime setting in worldserver.conf.
	/// </remarks>
	internal class IsNthAnniversaryAchievementCriteriaData : BaseAchievementCriteriaData
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaDataType.IsNthAnniversary;

		/// <summary>
		/// The number of years since the server's configured birthday.
		/// </summary>
		[MySqlColumnName("value1")]
		public uint N { get; set; }
	}
}
