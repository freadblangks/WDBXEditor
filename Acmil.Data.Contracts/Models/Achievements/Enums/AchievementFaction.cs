namespace Acmil.Data.Contracts.Models.Achievements.Enums
{
	/// <summary>
	/// The faction (Alliance, Horde, or Both) whose players can earn a particular Achievement.
	/// </summary>
	public enum AchievementFaction
	{
		/// <summary>
		/// The Achievement can be earned by players of both factions.
		/// </summary>
		Both = -1,

		/// <summary>
		/// The Achievement can only be earned by Horde players.
		/// </summary>
		Horde = 0,

		/// <summary>
		/// The Achievement can only be earned by Alliance players.
		/// </summary>
		Alliance = 1
	}
}
