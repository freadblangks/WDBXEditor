using Acmil.Data.Contracts.Models.Achievements.Criteria.Enums;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria.CriteriaData
{
	/// <summary>
	/// Requirement data where a server-side instance script must return true.
	/// </summary>
	/// <remarks>
	/// This requirement takes no inputs, as everything is handled by the server-side instance script.
	/// The `Asset_Id` column of `AchievementCriteria.dbc` CAN be used by those scripts, but it currently isn't.
	/// </remarks>
	public class InstanceScriptReturnsTrueAchievementCriteriaData : BaseAchievementCriteriaData
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaDataType.InstanceScriptReturnsTrue;
	}
}