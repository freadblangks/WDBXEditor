namespace Acmil.Data.Contracts.Models.Achievements.Criteria.Enums
{
	/// <summary>
	/// Different ways a timer for a requirement can be started.
	/// </summary>
	public enum AchievementCriteriaTimerStartEvent
	{
		/// <summary>
		/// There is no timer.
		/// </summary>
		None = 0,

		/// <summary>
		/// Timer is started by an internal event, whose ID is in the `Timer_Asset_Id` field.
		/// </summary>
		Event = 1,

		/// <summary>
		/// Timer is started by accepting a quest, whose entry ID is in the `Timer_Asset_Id` field.
		/// </summary>
		Quest = 2,

		/// <summary>
		/// Timer is started by casting a spell, whose ID is in the `Timer_Asset_Id` field.
		/// </summary>
		SpellCaster = 5,

		/// <summary>
		/// Timer is started by being the target of a spell, whose ID is in the `Timer_Asset_Id` field.
		/// </summary>
		SpellTarget = 6,

		/// <summary>
		/// Timer is started by killing a creature, whose entry ID is in the `Timer_Asset_Id` field.
		/// </summary>
		Creature = 7,

		/// <summary>
		/// Timer is started by using an item, whose entry ID is in the `Timer_Asset_Id` field.
		/// </summary>
		Item = 9
	}
}
