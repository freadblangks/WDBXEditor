using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Criteria.Enums;
using Acmil.Data.Contracts.Models.Characters.Enums;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria.CriteriaData
{
	/// <summary>
	/// Requirement data where the player must be at least at a certain level of drunkenness.
	/// </summary>
	public class SourcePlayerIsDrunkAchievementCriteriaData : BaseAchievementCriteriaData
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaDataType.SourcePlayerIsDrunk;

		/// <summary>
		/// A value indicating the state of drunkenness a player should at least be at.
		/// </summary>
		/// <remarks>
		/// For a full list of supported values, see <see cref="DrunkenState"/>.<br/>
		/// <br/>
		/// Sober : 0<br/>
		/// Tipsy : 1<br/>
		/// Drunk : 2<br/>
		/// Smashed : 3
		/// </remarks>
		[MySqlColumnName("value1")]
		[EnumType(typeof(DrunkenState))]
		[AllowEnumConversionOverride(false)]
		public uint DrunkenStateId { get; set; }
	}
}
