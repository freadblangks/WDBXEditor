using Acmil.Data.Contracts.Attributes;
using Acmil.Data.Contracts.Models.Achievements.Enums;
using Acmil.Data.Contracts.Models.Reputations.Enums;

namespace Acmil.Data.Contracts.Models.Achievements.Criteria.Reputations
{
	/// <summary>
	/// A requirement where the character must have a certain amount of reputation with a particular faction.
	/// </summary>
	public class HaveReputationAchievementCriteria : BaseAchievementCriteria
	{
		public override byte Type { get; internal set; } = (byte)AchievementCriteriaType.GainReputation;

		/// <summary>
		/// The ID of the faction the character must have an amount of reputation with.
		/// </summary>
		/// <remarks>
		/// For a list of supported values, see <see cref="PlayerVisibleFaction"/>.
		/// </remarks>
		[MySqlColumnName("Asset_Id")]
		[EnumType(typeof(PlayerVisibleFaction))]
		[AllowEnumConversionOverride(true)]
		public uint FactionId { get; set; }

		// NOTE: The non-negative requirement here is because of how this is implemented in AzerothCore.
		/// <summary>
		/// The minimum amount of reputation the player must have with the faction to meet the requirement.
		/// </summary>
		/// <remarks>
		/// For a list of supported values, see <see cref="ReputationMinimumAmount"/>.
		/// <br/>
		/// <br/>
		/// NOTE: Although a character can have negative reputation with a faction,
		/// only non-negative values are supported by this criteria type.
		/// </remarks>
		[MySqlColumnName("Quantity")]
		[EnumType(typeof(ReputationMinimumAmount))]
		[AllowEnumConversionOverride(true)]
		public uint RequiredReputationAmount { get; set; }
	}
}
