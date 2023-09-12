namespace Acmil.Data.Contracts.Models.Achievements.Criteria.Enums
{
	/// <summary>
	/// Types of criteria data that can be read from the World database's `achievement_criteria_data` table.
	/// </summary>
	public enum AchievementCriteriaDataType : byte
	{
		/// <summary>
		/// None.
		/// </summary>
		None = 0,

		/// <summary>
		/// Requirement data where a specific creature needs to be targeted.
		/// </summary>
		CreatureIsTarget = 1,

		/// <summary>
		/// Requirement data where a player of a specific class-race combination needs to be targeted.
		/// </summary>
		TargetPlayerIsClassRaceCombination = 2,

		/// <summary>
		/// Requirement data where a targeted player's health needs to be at or below a certain percentage.
		/// </summary>
		TargetPlayerAtOrLessThanXPercentHealth = 3,

		/// <summary>
		/// Requirement data where a target player must be dead but not released.
		/// </summary>
		TargetPlayerDeadButNotReleased = 4,

		/// <summary>
		/// Requirement data where the source player has an aura effect on them.
		/// </summary>
		SourcePlayerHasAuraEffect = 5,

		/// <summary>
		/// Requirement data where the player needs to be in a particular area.
		/// </summary>
		SourcePlayerInArea = 6,

		/// <summary>
		/// Requirement data where the target needs to have a specific aura effect on them.
		/// </summary>
		TargetHasAuraEffect = 7,

		/// <summary>
		/// Requirement data where the value in the Asset_Id column is compared
		/// to a specified valueusing a particular comparison operator.
		/// </summary>
		CompareToValue = 8,

		/// <summary>
		/// Requirement data where the target needs to be at or above a certain level.
		/// </summary>
		TargetIsAtOrAboveLevel = 9,

		/// <summary>
		/// Requirement data where the target needs to be a specific gender.
		/// </summary>
		TargetIsGender = 10,

		/// <summary>
		/// Requirement data where a server-side script needs to return <see langword="true"/>.
		/// </summary>
		ScriptReturnsTrue = 11,

		/// <summary>
		/// Requirement data where the player needs to be in an instance at a certain difficulty level.
		/// </summary>
		InstanceDifficulty = 12,

		/// <summary>
		/// Requirement data where there need to be a certain number or fewer players in the instance.
		/// </summary>
		InstanceMaxPlayerCount = 13,

		/// <summary>
		/// Requirement data where the target player needs to belong to a specific faction (Alliance/Horde).
		/// </summary>
		TargetPlayerBelongsToFaction = 14,

		/// <summary>
		/// Requirement data where the player must be at least at a certain level of drunkenness.
		/// </summary>
		SourcePlayerIsDrunk = 15,

		/// <summary>
		/// Requirement data where a specific holiday or world event needs to be active.
		/// </summary>
		WorldEventIsActive = 16,

		/// <summary>
		/// Requirement data where the player's team must win a battleground with a score within a defined range.
		/// </summary>
		BattlegroundTeamScoreIsWithinRange = 17,

		/// <summary>
		/// Requirement data where a server-side instance script must return true.
		/// </summary>
		InstanceScriptReturnsTrue = 18,

		/// <summary>
		/// Requirement data where the player must equip an item of a certain quality and iLevel in a particular equipment slot.
		/// </summary>
		SourcePlayerHasItemOfQualityAndILevelEquipped = 19,

		/// <summary>
		/// Requirement data where the player must be in a particular map.
		/// </summary>
		SourcePlayerIsInMap = 20,

		/// <summary>
		/// Requirement data where the player needs to be of a specific class-race combination.
		/// </summary>
		SourcePlayerIsClassRaceCombination = 21,

		/// <summary>
		/// Requirement data where the current date needs to be the Nth anniversary of the server's
		/// configured birthday.
		/// </summary>
		IsNthAnniversary = 22,

		/// <summary>
		/// Requirement data where the player needs to have a specific title.
		/// </summary>
		SourcePlayerHasPvpTitle = 23
	}
}
