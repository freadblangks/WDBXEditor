namespace Acmil.Data.Contracts.Models.Reputations.Enums
{
	/// <summary>
	/// The minimum amounts of reputation required for different standings with a faction.
	/// </summary>
	public enum ReputationMinimumAmount
	{
		/// <summary>
		/// The minimum amount of reputation required to be Hated with a faction.
		/// </summary>
		Hated = -42000,

		/// <summary>
		/// The minimum amount of reputation required to be Hostile with a faction.
		/// </summary>
		Hostile = -6000,

		/// <summary>
		/// The minimum amount of reputation required to be Unfriendly with a faction.
		/// </summary>
		Unfriendly = -3000,

		/// <summary>
		/// The minimum amount of reputation required to be Neutral with a faction.
		/// </summary>
		Neutral = 0,

		/// <summary>
		/// The minimum amount of reputation required to be Friendly with a faction.
		/// </summary>
		Friendly = 3000,

		/// <summary>
		/// The minimum amount of reputation required to be Honored with a faction.
		/// </summary>
		Honored = 9000,

		/// <summary>
		/// The minimum amount of reputation required to be Revered with a faction.
		/// </summary>
		Revered = 21000,

		/// <summary>
		/// The minimum amount of reputation required to be Exalted with a faction.
		/// </summary>
		Exalted = 42000
	}
}
