using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Criteria.Enums;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria.CriteriaData
{
	/// <summary>
	/// Requirement data where the player needs to have a specific title.
	/// </summary>
	public class SourcePlayerHasTitleAchievementCriteriaData : BaseAchievementCriteriaData
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaDataType.SourcePlayerHasPvpTitle;

		/// <summary>
		/// The ID of the title.
		/// </summary>
		/// <remarks>
		/// Taken from the `ID` column of `CharTitles.dbc`.
		/// </remarks>
		[MySqlColumnName("value1")]
		public uint TitleId { get; set; }
	}
}
