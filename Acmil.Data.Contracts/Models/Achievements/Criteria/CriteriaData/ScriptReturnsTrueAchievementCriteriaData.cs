using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Criteria.Enums;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria.CriteriaData
{
	/// <summary>
	/// Requirement data where a server-side script needs to return <see langword="true"/>.
	/// </summary>
	public class ScriptReturnsTrueAchievementCriteriaData : BaseAchievementCriteriaData
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaDataType.ScriptReturnsTrue;

		/// <summary>
		/// The name of the server-side script that should return <see langword="true"/>.
		/// </summary>
		[MySqlColumnName("ScriptName")]
		public string ScriptName { get; set; }
	}
}
