namespace Acmil.Data.Contracts.Models.Battlegrounds.Enums
{
	/// <summary>
	/// Values used to identify the status of a battleground instance.
	/// </summary>
	public enum BattlegroundStatus : byte
	{
		/// <summary>
		/// No instance has been created.
		/// </summary>
		None = 0,

		/// <summary>
		/// Battleground is empty and waiting for a queue.
		/// </summary>
		WaitQueue = 1,

		/// <summary>
		/// Battleground has an instance and is waiting for more players.
		/// </summary>
		WaitJoin = 2,

		/// <summary>
		/// Battleground is in progress.
		/// </summary>
		InProgress = 3,

		/// <summary>
		/// Battleground has ended and players are being shown the final score.
		/// </summary>
		WaitLeave = 4
	}
}
