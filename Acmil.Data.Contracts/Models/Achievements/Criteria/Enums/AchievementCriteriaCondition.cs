namespace Acmil.Data.Contracts.Models.Achievements.Criteria.Enums
{
	/// <summary>
	/// Conditions used in some achievement criteria.
	/// </summary>
	public enum AchievementCriteriaCondition
	{
		/// <summary>
		/// No condition.
		/// </summary>
		None = 0,

		/// <summary>
		/// Reset progress on death.
		/// </summary>
		NoDeath = 1,

		/// <summary>
		/// (UNKNOWN)
		/// </summary>
		Unknown1 = 2,

		/// <summary>
		/// Requires the character to be in a specific map.
		/// </summary>
		Map = 3,

		/// <summary>
		/// Requires the character not to lose an arena.
		/// </summary>
		NoLose = 4,

		/// <summary>
		/// Requires the character not to be hit by a specific spell.
		/// </summary>
		NoSpellHit = 9,

		/// <summary>
		/// Requires the character not to be in a group.
		/// </summary>
		NotInGroup = 10,

		/// <summary>
		/// (UNKNOWN)
		/// </summary>
		Unknown3 = 13,
	}
}
