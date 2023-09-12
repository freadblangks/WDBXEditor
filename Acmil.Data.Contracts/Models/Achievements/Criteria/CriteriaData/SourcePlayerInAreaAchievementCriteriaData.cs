using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Criteria.Enums;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria.CriteriaData
{
	/// <summary>
	/// Requirement data where the player needs to be in a particular area.
	/// </summary>
	public class SourcePlayerInAreaAchievementCriteriaData : BaseAchievementCriteriaData
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaDataType.SourcePlayerInArea;

		/// <summary>
		/// The ID of the Area the player should be in.
		/// </summary>
		/// <remarks>
		/// This is taken from the `ID` column of `AreaTable.dbc`.
		/// </remarks>
		[MySqlColumnName("value1")]
		public uint AreaId { get; set; }
	}
}
