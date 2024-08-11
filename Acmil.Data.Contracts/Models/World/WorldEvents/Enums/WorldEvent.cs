using Acmil.Data.Contracts.Attributes;

namespace Acmil.Data.Contracts.Models.World.WorldEvents.Enums
{
	/// <summary>
	/// Different holidays and other realm-wide calendar events.
	/// </summary>
	/// <remarks>
	/// Taken from the `ID` column of `Holidays.dbc`.
	/// </remarks>
	public enum WorldEvent
	{
		/// <summary>
		/// Midsummer Fireworks Spectacular.
		/// </summary>
		FireworksSpectacular = 62,

		/// <summary>
		/// Feast of Winter Veil.
		/// </summary>
		FeastOfWinterVeil = 141,

		/// <summary>
		/// Noblegarden.
		/// </summary>
		Noblegarden = 181,

		/// <summary>
		/// Children's Week.
		/// </summary>
		ChildrensWeek = 201,

		/// <summary>
		/// Call to Arms: Alterac Valley.
		/// </summary>
		CallToArmsAlteracValley = 283,

		/// <summary>
		/// Call to Arms: Warsong Gulch.
		/// </summary>
		CallToArmsWarsongGulch = 284,

		/// <summary>
		/// Call to Arms: Arathi Basin.
		/// </summary>
		CallToArmsArathiBasin = 285,

		/// <summary>
		/// Stranglethorn Fishing Extravaganza.
		/// </summary>
		StranglethornFishingExtravaganza = 301,

		/// <summary>
		/// Harvest Festival.
		/// </summary>
		HarvestFestival = 321,

		/// <summary>
		/// Hallow's End.
		/// </summary>
		HallowsEnd = 324,

		/// <summary>
		/// Lunar Festival.
		/// </summary>
		LunarFestival = 327,

		/// <summary>
		/// Midsummer Fire Festival.
		/// </summary>
		MidsummerFireFestival = 341,

		/// <summary>
		/// Call to Arms: Eye of the Storm.
		/// </summary>
		CallToArmsEyeOfTheStorm = 353,

		/// <summary>
		/// Brewfest.
		/// </summary>
		Brewfest = 372,

		/// <summary>
		/// Darkmoon Faire in Elwynn Forest.
		/// </summary>
		DarkmoonFaireElwynnForest = 374,

		/// <summary>
		/// Darkmoon Faire in Mulgore.
		/// </summary>
		DarkmoonFaireMulgore = 375,

		/// <summary>
		/// Darkmoon Faire in Terokkar Forest.
		/// </summary>
		DarkmoonFaireTerokkarForest = 376,

		/// <summary>
		/// Pirates' Day.
		/// </summary>
		PiratesDay = 398,

		/// <summary>
		/// Call to Arms: Strand of the Ancients
		/// </summary>
		CallToArmsStrandOfTheAncients = 400,

		/// <summary>
		/// Pilgrim's Bounty.
		/// </summary>
		PilgrimsBounty = 404,

		/// <summary>
		/// (NOT IMPLEMENTED) Wrath of the Lich King Launch.
		/// </summary>
		/// <remarks>
		/// Although this has a date in the DBC and can be used for Achievement criteria data,
		/// this is not used anywhere in the server code.
		/// </remarks>
		[NotImplemented]
		WotlkLaunch = 406,

		/// <summary>
		/// Day of the Dead.
		/// </summary>
		DayOfTheDead = 409,

		/// <summary>
		/// Call to Arms: Isle of Conquest.
		/// </summary>
		CallToArmsIsleOfConquest = 420,

		/// <summary>
		/// Love is in the Air.
		/// </summary>
		LoveIsInTheAir = 423,

		/// <summary>
		/// Kalu'ak Fishing Derby.
		/// </summary>
		KaluAkFishingDerby = 424
	}
}
