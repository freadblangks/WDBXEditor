using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Criteria.Enums;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria.CriteriaData
{
	/// <summary>
	/// Requirement data where the player must be in a particular map.
	/// </summary>
	public class SourcePlayerIsInMapAchievementCriteriaData : BaseAchievementCriteriaData
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaDataType.SourcePlayerIsInMap;

		/// <summary>
		/// The ID of the map the player must be in.
		/// </summary>
		/// <remarks>
		/// Taken from the `ID` column of `Map.dbc`.
		/// </remarks>
		[MySqlColumnName("value1")]
		public uint MapId { get; set; }
	}
}
