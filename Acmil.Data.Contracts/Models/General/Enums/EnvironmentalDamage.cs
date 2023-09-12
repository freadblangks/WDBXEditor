namespace Acmil.Data.Contracts.Models.General.Enums
{
	/// <summary>
	/// IDs used to identify different sources of environmental damage.
	/// </summary>
	public enum EnvironmentalDamage
	{
		/// <summary>
		/// Death from fatigue.
		/// </summary>
		Fatigue = 0,

		/// <summary>
		/// Death from drowning.
		/// </summary>
		Drowning = 1,

		/// <summary>
		/// Death from falling onto ground.
		/// </summary>
		Falling = 2,

		/// <summary>
		/// Death from lava.
		/// </summary>
		Lava = 3,

		/// <summary>
		/// Death from slime.
		/// </summary>
		Slime = 4,

		/// <summary>
		/// Death from fire.
		/// </summary>
		Fire = 5,

		/// <summary>
		/// Death from falling into the void (i.e. under the map/outside bounds).
		/// </summary>
		FallIntoVoid = 6
	}
}
