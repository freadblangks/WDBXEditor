namespace Acmil.Data.Contracts.Models.Maps.Enums
{
	/// <summary>
	/// Area IDs from the `WMOID` column of `WMOAreaTable.dbc`.
	/// </summary>
	/// <remarks>
	/// These are distinct from the Area IDs from `AreaTable.dbc` and
	/// are mostly only used by the client to show the names of areas
	/// and to detect progress for Exploration Achievements.
	/// </remarks>
	public enum WmoAreaId
	{
		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Ban'ethil Hollow.
		/// </summary>
		BanethilHollow = 84,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Shadowglen.
		/// </summary>
		Shadowglen = 91,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Gnomeregan.
		/// </summary>
		Gnomeregan = 101,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Iceflow Lake.
		/// </summary>
		IceflowLake = 102,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Frostmane Hold.
		/// </summary>
		FrostmaneHold = 104,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Helm's Bed Lake.
		/// </summary>
		HelmsBedLake = 108,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Misty Pine Refuge.
		/// </summary>
		MistyPineRefuge = 111,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Kharanos.
		/// </summary>
		Kharanos = 113,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for The Grizzled Den.
		/// </summary>
		TheGrizzledDen = 114,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Shimmer Ridge.
		/// </summary>
		ShimmerRidge = 115,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Chill Breeze Valley.
		/// </summary>
		ChillBreezeValley = 116,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Stormwind City.
		/// </summary>
		StormwindCity = 121,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Goldshire.
		/// </summary>
		Goldshire = 122,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Forest's Edge.
		/// </summary>
		ForestsEdge = 123,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Fargodeep Mine.
		/// </summary>
		FargodeepMine = 124,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Brackwell Pumpkin Patch.
		/// </summary>
		BrackwellPumpkinPatch = 128,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Eastvale Logging Camp.
		/// </summary>
		EastvaleLoggingCamp = 129,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Ridgepoint Tower.
		/// </summary>
		RidgepointTower = 130,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Crystal Lake.
		/// </summary>
		CrystalLake = 131,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Stone Cairn Lake.
		/// </summary>
		StoneCairnLake = 132,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Bloodhoof Village.
		/// </summary>
		BloodhoofVillage = 186,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Winterhoof Water Well.
		/// </summary>
		WinterhoofWaterWell = 188,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Ravaged Caravan.
		/// </summary>
		RavagedCaravan = 194,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Red Rocks.
		/// </summary>
		RedRocks = 206,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Windfury Ridge.
		/// </summary>
		WindfuryRidge = 208,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Whispering Gardens.
		/// </summary>
		WhisperingGardens = 223,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Scarlet Watch Post.
		/// </summary>
		ScarletWatchPost = 224,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Crusader Outpost.
		/// </summary>
		CrusaderOutpost = 225,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Balnir Farmstead.
		/// </summary>
		BalnirFarmstead = 226,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Brightwater Lake.
		/// </summary>
		BrightwaterLake = 227,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Undercity.
		/// </summary>
		Undercity = 228,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Brill.
		/// </summary>
		Brill = 230,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Cold Hearth Manor.
		/// </summary>
		ColdHearthManor = 231,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Nightmare Vale.
		/// </summary>
		NightmareVale = 232,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Stillwater Pond.
		/// </summary>
		StillwaterPond = 233,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Agamand Mills.
		/// </summary>
		AgamandMills = 234,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Solliden Farmstead.
		/// </summary>
		SollidenFarmstead = 235,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Deathknell.
		/// </summary>
		Deathknell = 236,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Orgrimmar.
		/// </summary>
		Orgrimmar = 262,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Darkshire.
		/// </summary>
		Darkshire = 431,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Chillwind Point.
		/// </summary>
		ChillwindPoint = 441,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Corrahn's Dagger.
		/// </summary>
		CorrahnsDagger = 442,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Gallows' Corner.
		/// </summary>
		GallowsCorner = 446,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Misty Shore.
		/// </summary>
		MistyShore = 450,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Strahnbrad.
		/// </summary>
		Strahnbrad = 453,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for The Headland.
		/// </summary>
		TheHeadland = 454,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for The Uplands.
		/// </summary>
		TheUplands = 455,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Purgation Isle.
		/// </summary>
		PurgationIsle = 456,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Boulder Lode Mine.
		/// </summary>
		BoulderLodeMine = 461,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for The Sludge Fen.
		/// </summary>
		TheSludgeFen = 462,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Jaguero Isle.
		/// </summary>
		JagueroIsle = 501,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Misty Valley.
		/// </summary>
		MistyValley = 542,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Circle of West Binding.
		/// </summary>
		CircleOfWestBinding = 561,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Thandol Span.
		/// </summary>
		ThandolSpan = 568,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Refuge Pointe.
		/// </summary>
		RefugePointe = 570,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Hammerfall.
		/// </summary>
		Hammerfall = 576,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Camp Cagg.
		/// </summary>
		CampCagg = 585,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Kargath.
		/// </summary>
		Kargath = 587,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Valley of Fangs.
		/// </summary>
		ValleyOfFangs = 589,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Angor Fortress.
		/// </summary>
		AngorFortress = 590,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for The Maker's Terrace.
		/// </summary>
		TheMakersTerrace = 593,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Camp Kosh.
		/// </summary>
		CampKosh = 594,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Hiri'watha.
		/// </summary>
		Hiriwatha = 603,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Shadra'Alor.
		/// </summary>
		ShadraAlor = 605,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Agol'watha.
		/// </summary>
		Agolwatha = 607,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for The Creeping Ruin.
		/// </summary>
		TheCreepingRuin = 608,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Skulk Rock.
		/// </summary>
		SkulkRock = 611,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Shaol'watha.
		/// </summary>
		Shaolwatha = 612,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Golakka Hot Springs.
		/// </summary>
		GolakkaHotSprings = 622,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Terror Run.
		/// </summary>
		TerrorRun = 623,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for The Slithering Scar.
		/// </summary>
		TheSlitheringScar = 624,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for The Marshlands.
		/// </summary>
		TheMarshlands = 625,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Valley of the Watchers.
		/// </summary>
		ValleyOfTheWatchers = 642,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Dunemaul Compound.
		/// </summary>
		DunemaulCompound = 648,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Broken Pillar.
		/// </summary>
		BrokenPillar = 650,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Lost Rigger Cove.
		/// </summary>
		LostRiggerCove = 653,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Zalashji's Den.
		/// </summary>
		ZalashjisDen = 654,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Steamwheedle Port.
		/// </summary>
		SteamwheedlePort = 655,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Noonshade Ruins.
		/// </summary>
		NoonshadeRuins = 656,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Caverns of Time.
		/// </summary>
		CavernsOfTime = 657,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Gadgetzan.
		/// </summary>
		Gadgetzan = 658,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Sandsorrow Watch.
		/// </summary>
		SandsorrowWatch = 659,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Zul'Farrak.
		/// </summary>
		ZulFarrak = 660,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Theramore Isle.
		/// </summary>
		TheramoreIsle = 661,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Witch Hill.
		/// </summary>
		WitchHill = 662,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Brackenwall Village.
		/// </summary>
		BrackenwallVillage = 663,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for The Quagmire.
		/// </summary>
		TheQuagmire = 664,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for The Den of Flame.
		/// </summary>
		TheDenOfFlame = 665,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Wyrmbog.
		/// </summary>
		Wyrmbog = 666,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Alcaz Island.
		/// </summary>
		AlcazIsland = 667,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Freewind Post.
		/// </summary>
		FreewindPost = 684,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Darkcloud Pinnacle.
		/// </summary>
		DarkcloudPinnacle = 686,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Camp E'thok.
		/// </summary>
		CampEthok = 688,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Highperch.
		/// </summary>
		Highperch = 689,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Lake Elune'ara.
		/// </summary>
		LakeEluneara = 701,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Firewatch Ridge.
		/// </summary>
		FirewatchRidge = 721,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for The Cauldron.
		/// </summary>
		TheCauldron = 722,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Blackchar Cave.
		/// </summary>
		BlackcharCave = 723,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for The Sea of Cinders.
		/// </summary>
		TheSeaOfCinders = 724,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Tanner Camp.
		/// </summary>
		TannerCamp = 725,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Grimesilt Dig Site.
		/// </summary>
		GrimesiltDigSite = 726,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Dustfire Valley.
		/// </summary>
		DustfireValley = 727,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Fire Scar Shrine.
		/// </summary>
		FireScarShrine = 746,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Astranaar.
		/// </summary>
		Astranaar = 747,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Raynewood Retreat.
		/// </summary>
		RaynewoodRetreat = 752,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Splintertree Post.
		/// </summary>
		SplintertreePost = 754,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Satyrnaar.
		/// </summary>
		Satyrnaar = 755,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Bough Shadow.
		/// </summary>
		BoughShadow = 756,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Warsong Lumber Camp.
		/// </summary>
		WarsongLumberCamp = 757,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Ranazjar Isle.
		/// </summary>
		RanazjarIsle = 761,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Shadowbreak Ravine.
		/// </summary>
		ShadowbreakRavine = 762,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Magram Village.
		/// </summary>
		MagramVillage = 763,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Mannoroc Coven.
		/// </summary>
		MannorocCoven = 764,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Sargeron.
		/// </summary>
		Sargeron = 773,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Nijel's Point.
		/// </summary>
		NijelsPoint = 774,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Tethris Aran.
		/// </summary>
		TethrisAran = 775,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Dreadmaul Rock.
		/// </summary>
		DreadmaulRock = 781,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Morgan's Vigil.
		/// </summary>
		MorgansVigil = 782,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Terror Wing Path.
		/// </summary>
		TerrorWingPath = 783,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Blackrock Pass.
		/// </summary>
		BlackrockPass = 784,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Ruins of Thaurissan.
		/// </summary>
		RuinsOfThaurissan = 785,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for The Pillar of Ash.
		/// </summary>
		ThePillarOfAsh = 786,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Blackrock Stronghold.
		/// </summary>
		BlackrockStronghold = 787,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Altar of Storms in Burning Steppes.
		/// </summary>
		AltarOfStormsBurningSteppes = 789,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Dreadmaul Hold.
		/// </summary>
		DreadmaulHold = 821,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Garrison Armory.
		/// </summary>
		GarrisonArmory = 822,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Nethergarde Keep.
		/// </summary>
		NethergardeKeep = 823,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Serpent's Coil.
		/// </summary>
		SerpentsCoil = 824,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for The Dark Portal.
		/// </summary>
		TheDarkPortal = 825,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Altar of Storms in Blasted Lands.
		/// </summary>
		AltarOfStormsBlastedLands = 826,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Dreadmaul Post.
		/// </summary>
		DreadmaulPost = 827,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Rise of the Defiler.
		/// </summary>
		RiseOfTheDefiler = 829,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for The Shattered Strand.
		/// </summary>
		TheShatteredStrand = 842,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Felpaw Village.
		/// </summary>
		FelpawVillage = 861,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Jadefire Run.
		/// </summary>
		JadefireRun = 864,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Jadefire Glen.
		/// </summary>
		JadefireGlen = 869,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Emerald Sanctuary.
		/// </summary>
		EmeraldSanctuary = 870,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Deadwood Village.
		/// </summary>
		DeadwoodVillage = 871,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Morlos'Aran.
		/// </summary>
		MorlosAran = 872,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Stratholme.
		/// </summary>
		Stratholme = 873,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Northdale.
		/// </summary>
		Northdale = 879,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for The Noxious Glade.
		/// </summary>
		TheNoxiousGlade = 882,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for The Infectis Scar.
		/// </summary>
		TheInfectisScar = 883,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Corin's Crossing.
		/// </summary>
		CorinsCrossing = 887,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Pestilent Scar.
		/// </summary>
		PestilentScar = 888,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Darrowshire.
		/// </summary>
		Darrowshire = 889,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for The Fungal Vale.
		/// </summary>
		TheFungalVale = 890,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Crown Guard Tower.
		/// </summary>
		CrownGuardTower = 891,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for The Undercroft.
		/// </summary>
		TheUndercroft = 892,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for The Marris Stead.
		/// </summary>
		TheMarrisStead = 893,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Thondroril River.
		/// </summary>
		ThondrorilRiver = 894,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Stonetalon Peak.
		/// </summary>
		StonetalonPeak = 921,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Windshear Crag.
		/// </summary>
		WindshearCrag = 925,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Deadman's Crossing.
		/// </summary>
		DeadmansCrossing = 941,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Darrowmere Lake.
		/// </summary>
		DarrowmereLake = 961,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Caer Darrow.
		/// </summary>
		CaerDarrow = 962,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Sorrow Hill.
		/// </summary>
		SorrowHill = 963,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Isle of Dread.
		/// </summary>
		IsleOfDread = 996,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Lower Wilds.
		/// </summary>
		LowerWilds = 997,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Frostwhisper Gorge.
		/// </summary>
		FrostwhisperGorge = 1006,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Ice Thistle Hills.
		/// </summary>
		IceThistleHills = 1009,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Everlook.
		/// </summary>
		Everlook = 1010,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Winterfall Village.
		/// </summary>
		WinterfallVillage = 1011,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for The Hidden Grove.
		/// </summary>
		TheHiddenGrove = 1012,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for The Crystal Vale.
		/// </summary>
		TheCrystalVale = 1021,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Gates of Ironforge.
		/// </summary>
		GatesOfIronforge = 1041,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Legion Hold.
		/// </summary>
		LegionHold = 1104,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Netherwing Ledge.
		/// </summary>
		NetherwingLedge = 1105,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Shadowmoon Village.
		/// </summary>
		ShadowmoonVillage = 1107,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Ruins of Silvermoon.
		/// </summary>
		RuinsOfSilvermoon = 1128,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for West Sanctum.
		/// </summary>
		WestSanctum = 1129,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Sunsail Anchorage.
		/// </summary>
		SunsailAnchorage = 1130,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for North Sanctum.
		/// </summary>
		NorthSanctum = 1132,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Farstrider Retreat.
		/// </summary>
		FarstriderRetreat = 1135,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Hewn Bog.
		/// </summary>
		HewnBog = 1136,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Stillwhisper Pond.
		/// </summary>
		StillwhisperPond = 1137,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Duskwither Grounds.
		/// </summary>
		DuskwitherGrounds = 1138,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Marshlight Lake.
		/// </summary>
		MarshlightLake = 1139,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Fairbreeze Village.
		/// </summary>
		FairbreezeVillage = 1140,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Quagg Ridge.
		/// </summary>
		QuaggRidge = 1141,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for The Living Wood.
		/// </summary>
		TheLivingWood = 1142,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Tor'Watha.
		/// </summary>
		TorWatha = 1143,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for The Scorched Grove.
		/// </summary>
		TheScorchedGrove = 1145,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Telredor.
		/// </summary>
		Telredor = 1146,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for The Dead Mire.
		/// </summary>
		TheDeadMire = 1147,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Tranquillien.
		/// </summary>
		Tranquillien = 1148,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for The Lagoon.
		/// </summary>
		TheLagoon = 1149,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Suncrown Village.
		/// </summary>
		SuncrownVillage = 1150,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Goldenmist Village.
		/// </summary>
		GoldenmistVillage = 1151,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Windrunner Village.
		/// </summary>
		WindrunnerVillage = 1152,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Sanctum of the Moon.
		/// </summary>
		SanctumOfTheMoon = 1153,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Twin Spire Ruins.
		/// </summary>
		TwinSpireRuins = 1154,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Sanctum of the Sun.
		/// </summary>
		SanctumOfTheSun = 1155,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Umbrafen Village.
		/// </summary>
		UmbrafenVillage = 1156,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Dawnstar Spire.
		/// </summary>
		DawnstarSpire = 1157,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Farstrider Enclave.
		/// </summary>
		FarstriderEnclave = 1158,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Allerian Stronghold.
		/// </summary>
		AllerianStronghold = 1161,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Howling Ziggurat.
		/// </summary>
		HowlingZiggurat = 1162,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Deatholme.
		/// </summary>
		Deatholme = 1163,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Zeb'Nowa.
		/// </summary>
		ZebNowa = 1164,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Amani Pass.
		/// </summary>
		AmaniPass = 1165,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Bleeding Hollow Ruins.
		/// </summary>
		BleedingHollowRuins = 1166,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Cenarion Thicket.
		/// </summary>
		CenarionThicket = 1167,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Firewing Point.
		/// </summary>
		FirewingPoint = 1168,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Grangol'var Village.
		/// </summary>
		GrangolvarVillage = 1169,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Stonebreaker Hold.
		/// </summary>
		StonebreakerHold = 1171,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Tuurem.
		/// </summary>
		Tuurem = 1173,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Windrunner Spire.
		/// </summary>
		WindrunnerSpire = 1174,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Silvermoon City.
		/// </summary>
		SilvermoonCity = 1175,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Shattrath City.
		/// </summary>
		ShattrathCity = 1176,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Bleeding Ziggurat.
		/// </summary>
		BleedingZiggurat = 1177,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Sporeggar.
		/// </summary>
		Sporeggar = 1178,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Area 52.
		/// </summary>
		Area52 = 1179,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Manaforge Coruu.
		/// </summary>
		ManaforgeCoruu = 1181,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Manaforge Duro.
		/// </summary>
		ManaforgeDuro = 1182,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Manaforge Ara.
		/// </summary>
		ManaforgeAra = 1183,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Manaforge Ultris.
		/// </summary>
		ManaforgeUltris = 1184,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Ruins of Farahlon.
		/// </summary>
		RuinsOfFarahlon = 1185,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Tempest Keep.
		/// </summary>
		TempestKeep = 1186,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for The Heap.
		/// </summary>
		TheHeap = 1187,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Kil'sorrow Fortress.
		/// </summary>
		KilsorrowFortress = 1192,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Laughing Skull Ruins.
		/// </summary>
		LaughingSkullRuins = 1193,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Telaar.
		/// </summary>
		Telaar = 1196,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for The Ring of Trials.
		/// </summary>
		TheRingOfTrials = 1197,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Throne of the Elements.
		/// </summary>
		ThroneOfTheElements = 1198,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Warmaul Hill.
		/// </summary>
		WarmaulHill = 1199,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Expedition Armory.
		/// </summary>
		ExpeditionArmory = 1200,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Falcon Watch.
		/// </summary>
		FalconWatch = 1201,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Hellfire Citadel.
		/// </summary>
		HellfireCitadel = 1202,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Honor Hold.
		/// </summary>
		HonorHold = 1203,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Mag'har Post.
		/// </summary>
		MagharPost = 1208,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Pools of Aggonar.
		/// </summary>
		PoolsOfAggonar = 1215,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Ruins of Sha'naar.
		/// </summary>
		RuinsOfShanaar = 1216,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for The Legion Front.
		/// </summary>
		TheLegionFront = 1218,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for The Stair of Destiny.
		/// </summary>
		TheStairOfDestiny = 1219,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Thrallmar.
		/// </summary>
		Thrallmar = 1220,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Southwind Cleft.
		/// </summary>
		SouthwindCleft = 1229,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for The Stormspire.
		/// </summary>
		TheStormspire = 1269,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Gyro-Plank Bridge.
		/// </summary>
		GyroPlankBridge = 1270,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Altar of Sha'tar.
		/// </summary>
		AltarOfShatar = 1273,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Illidari Point.
		/// </summary>
		IllidariPoint = 1274,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Bladed Gulch.
		/// </summary>
		BladedGulch = 1289,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Bladespire Hold.
		/// </summary>
		BladespireHold = 1290,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Sylvanaar.
		/// </summary>
		Sylvanaar = 1309,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Axxarien.
		/// </summary>
		Axxarien = 1329,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Blacksilt Shore.
		/// </summary>
		BlacksiltShore = 1330,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for The Lost Fold.
		/// </summary>
		TheLostFold = 1349,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for The Vector Coil.
		/// </summary>
		TheVectorCoil = 1350,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for The Warp Piston.
		/// </summary>
		TheWarpPiston = 1351,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Wyrmscar Island.
		/// </summary>
		WyrmscarIsland = 1355,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Azurebreeze Coast.
		/// </summary>
		AzurebreezeCoast = 1356,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Elrendar Falls.
		/// </summary>
		ElrendarFalls = 1357,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Goldenbough Pass.
		/// </summary>
		GoldenboughPass = 1358,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Lake Elrendar.
		/// </summary>
		LakeElrendar = 1359,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Geezle's Camp.
		/// </summary>
		GeezlesCamp = 1369,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Moongraze Woods.
		/// </summary>
		MoongrazeWoods = 1370,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Odesyus' Landing.
		/// </summary>
		OdesyusLanding = 1371,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Pod Cluster.
		/// </summary>
		PodCluster = 1372,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Silting Shore.
		/// </summary>
		SiltingShore = 1374,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Silvermyst Isle.
		/// </summary>
		SilvermystIsle = 1375,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Stillpine Hold.
		/// </summary>
		StillpineHold = 1376,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for The Exodar.
		/// </summary>
		TheExodar = 1377,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Valaar's Berth.
		/// </summary>
		ValaarsBerth = 1378,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Wrathscale Point.
		/// </summary>
		WrathscalePoint = 1379,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Golden Strand.
		/// </summary>
		GoldenStrand = 1380,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Thuron's Livery.
		/// </summary>
		ThuronsLivery = 1381,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Tranquil Shore.
		/// </summary>
		TranquilShore = 1382,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Zeb'Watha.
		/// </summary>
		ZebWatha = 1383,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Auchenai Grounds.
		/// </summary>
		AuchenaiGrounds = 1390,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Carrion Hill.
		/// </summary>
		CarrionHill = 1391,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Refugee Caravan.
		/// </summary>
		RefugeeCaravan = 1392,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Ring of Observance.
		/// </summary>
		RingOfObservance = 1393,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Shadow Tomb.
		/// </summary>
		ShadowTomb = 1394,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Derelict Caravan.
		/// </summary>
		DerelictCaravan = 1395,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Veil Rhaze.
		/// </summary>
		VeilRhaze = 1397,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Writhing Mound.
		/// </summary>
		WrithingMound = 1398,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Kaskala.
		/// </summary>
		Kaskala = 1469,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Ruby Dragonshrine.
		/// </summary>
		RubyDragonshrine = 1489,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Obsidian Dragonshrine.
		/// </summary>
		ObsidianDragonshrine = 1490,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for New Hearthglen.
		/// </summary>
		NewHearthglen = 1491,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Giant's Run.
		/// </summary>
		GiantsRun = 1509,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Scarlet Point.
		/// </summary>
		ScarletPoint = 1529,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Gundrak.
		/// </summary>
		Gundrak = 1530,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Drak'Sotra Fields.
		/// </summary>
		DrakSotraFields = 1531,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for The Suntouched Pillar.
		/// </summary>
		TheSuntouchedPillar = 1549,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Rainspeaker Canopy.
		/// </summary>
		RainspeakerCanopy = 1550,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for The Lifeblood Pillar.
		/// </summary>
		TheLifebloodPillar = 1551,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for The Avalanche.
		/// </summary>
		TheAvalanche = 1552,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for The Glimmering Pillar.
		/// </summary>
		TheGlimmeringPillar = 1553,

		/// <summary>
		/// The WorldMapOverlay (WMO) ID for Nidavelir.
		/// </summary>
		Nidavelir = 1589
	}
}

