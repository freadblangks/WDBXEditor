namespace Acmil.Data.Contracts.Models.Battlegrounds.Enums
{
	/// <summary>
	/// IDs identifying different battleground objectives that can be achieved.
	/// </summary>
	public enum BattlegroundObjective : byte
	{
		/// <summary>
		/// Capturing a flag in Warsong Gulch.
		/// </summary>
		WarsongGulchCaptureFlag = 42,

		/// <summary>
		/// Returning a flag in Warsong Gulch.
		/// </summary>
		WarsongGulchReturnFlag = 44,

		/// <summary>
		/// Assaulting a tower in Alterac Valley.
		/// </summary>
		AlteracValleyAssaultTower = 61,

		/// <summary>
		/// Assaulting a graveyard in Alterac Valley.
		/// </summary>
		AlteracValleyAssaultGraveyard = 63,

		/// <summary>
		/// Defending a tower in Alterac Valley.
		/// </summary>
		AlteracValleyDefendTower = 64,

		/// <summary>
		/// Defending a tower in Alterac Valley.
		/// </summary>
		AlteracValleyDefendGraveyard = 65,

		/// <summary>
		/// Assaulting a base in Arathi Basin.
		/// </summary>
		ArathiBasinAssaultBase = 122,

		/// <summary>
		/// Defending a base in Arathi Basin.
		/// </summary>
		ArathiBasinDefendBase = 123,

		/// <summary>
		/// Capturing the flag in Eye of the Storm.
		/// </summary>
		EyeOfTheStormCaptureFlag = 183,

		/// <summary>
		/// Assaulting a base in Isle of Conquest.
		/// </summary>
		IsleOfConquestAssaultBase = 245,

		/// <summary>
		/// Defending a base in Isle of Conquest.
		/// </summary>
		IsleOfConquestDefendBase = 246
	}
}
