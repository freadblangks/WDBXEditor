namespace Acmil.Data.Contracts.Models.General.Enums
{
	/// <summary>
	/// The in-world languages of World of Warcraft.
	/// </summary>
	public enum Language : byte
	{
		/// <summary>
		/// Used for things that are understandable by all players regardless of race or faction.
		/// </summary>
		None = 0,

		/// <summary>
		/// The language spoken by Orcs as well as the other Horde races.
		/// </summary>
		Orcish = 1,

		/// <summary>
		/// The language spoken by Night Elves.
		/// </summary>
		Darnassian = 2,

		/// <summary>
		/// The language spoken by Tauren.
		/// </summary>
		Taurahe = 3,

		/// <summary>
		/// The language spoken by Dwarves.
		/// </summary>
		Dwarvish = 6,

		/// <summary>
		/// The language spoken by Humans as well as the other Alliance races.
		/// </summary>
		Common = 7,

		/// <summary>
		/// The language spoken by Demons.
		/// </summary>
		Demonic = 8,

		/// <summary>
		/// The language spoken by Titans.
		/// </summary>
		Titan = 9,

		/// <summary>
		/// The language spoken by Blood Elves
		/// </summary>
		Thalassian = 10,

		/// <summary>
		/// The language spoken by Dragons.
		/// </summary>
		Draconic = 11,

		/// <summary>
		/// The language spoken my Elementals.
		/// </summary>
		Kalimag = 12,

		/// <summary>
		/// The language spoken by Gnomes.
		/// </summary>
		Gnomish = 13,

		/// <summary>
		/// The language spoken by trolls.
		/// </summary>
		Troll = 14,

		/// <summary>
		/// The language spoken by Undead (Forsaken).
		/// </summary>
		Gutterspeak = 33,

		/// <summary>
		/// The language spoken by Draenei.
		/// </summary>
		Draenei = 35,

		/// <summary>
		/// The language spoken by the Undead (Scourge).
		/// </summary>
		/// <remarks>
		/// Used to block communication between infected and non-infected players during 
		/// the Scourge Invasion event that kicked off Wrath of the Lich King.
		/// </remarks>
		Zombie = 36,

		/// <summary>
		/// The Gnomish standard for base-2 machine code.
		/// </summary>
		GnomishBinary = 37,

		/// <summary>
		/// The Goblin standard for base-2 machine code.
		/// </summary>
		GoblinBinary = 38
	}
}
