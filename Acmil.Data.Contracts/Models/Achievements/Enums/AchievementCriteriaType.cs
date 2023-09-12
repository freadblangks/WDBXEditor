using Acmil.Data.Contracts.Attributes;

namespace Acmil.Data.Contracts.Models.Achievements.Enums
{
	/// <summary>
	/// Types of Achievement criteria.
	/// </summary>
	public enum AchievementCriteriaType : byte
	{
		/// <summary>
		/// A requirement where the character must kill a particular creature.
		/// </summary>
		KillCreature = 0,

		/// <summary>
		/// A requirement where the character must win a particular battleground.
		/// </summary>
		WinBattleground = 1,

		/// <summary>
		/// A requirement where the character must reach a particular level.
		/// </summary>
		ReachLevel = 5,
		ReachSkillLevel = 7,

		/// <summary>
		/// A requirement where the character must complete a particular Achievement.
		/// </summary>
		CompleteAchievement = 8,
		CompleteQuestCount = 9,
		CompleteDailyQuestDaily = 10,
		CompleteQuestsInZone = 11,
		DamageDone = 13,
		CompleteDailyQuest = 14,
		CompleteBattleground = 15,
		DeathAtMap = 16,
		DeathInDungeon = 18,

		/// <summary>
		/// (NOT IMPLEMENTED) A requirement where the character must complete a particular raid.
		/// </summary>
		[NotImplemented]
		CompleteRaid = 19,
		KilledByCreature = 20,
		FallWithoutDying = 24,
		DeathsFrom = 26,
		CompleteQuest = 27,
		BeSpellTarget = 28,
		CastSpellExtended = 29,
		BattlegroundObjectiveCapture = 30,
		HonorableKillAtArea = 31,
		WinArena = 32,
		PlayArena = 33,
		LearnSpell = 34,

		/// <summary>
		/// A requirement where the character must own an item (i.e. have it in their bag or bank).
		/// </summary>
		OwnItem = 36,
		WinRatedArena = 37,
		HighestTeamRating = 38,
		ReachTeamRating = 39,
		LearnSkillLevel = 40,

		/// <summary>
		/// A requirement where the character must use an item.
		/// </summary>
		UseItem = 41,

		/// <summary>
		/// A requirement where the character must loot an item.
		/// </summary>
		LootItem = 42,

		/// <summary>
		/// A requirement where the character must discover an area.
		/// </summary>
		ExploreArea = 43,

		/// <summary>
		/// (NOT IMPLEMENTED) A requirement where the character must have a particular PVP rank (e.g. Grand Marshal).
		/// </summary>
		[NotImplemented]
		OwnRank = 44,

		/// <summary>
		/// A requirement where the character must buy one or more bank slots.
		/// </summary>
		BuyBankSlot = 45,

		/// <summary>
		/// A requirement where the character must have a certain amount of reputation with a particular faction.
		/// </summary>
		GainReputation = 46,

		/// <summary>
		/// A requirement where the character needs to have a certain number of Factions at Exalted.
		/// </summary>
		GainExaltedReputation = 47,

		/// <summary>
		/// A requirement where the character needs to pay to have their appearance changed at the Barber Shop.
		/// </summary>
		VisitBarberShop = 48,
		EquipEpicItem = 49,
		RollNeedOnLoot = 50,
		RollGreedOnLoot = 51,
		HkClass = 52,
		HkRace = 53,
		DoEmote = 54,
		HealingDone = 55,
		GetKillingBlows = 56,

		/// <summary>
		/// A requirement where the character must equip an item.
		/// </summary>
		EquipItem = 57,
		MoneyFromQuestReward = 62,
		LootMoney = 67,
		UseGameobject = 68,
		BeSpellTargetExtended = 69,
		SpecialPvpKill = 70,
		FishInGameobject = 72,
		LearnSkillLineSpells = 75,
		WinDuel = 76,

		/// <summary>
		/// (NOT IMPLEMENTED)
		/// </summary>
		[NotImplemented]
		HighestPower = 96,

		/// <summary>
		/// (NOT IMPLEMENTED)
		/// </summary>
		[NotImplemented]
		HighestStat = 97,

		/// <summary>
		/// (NOT IMPLEMENTED)
		/// </summary>
		[NotImplemented]
		HighestSpellpower = 98,

		/// <summary>
		/// (NOT IMPLEMENTED)
		/// </summary>
		[NotImplemented]
		HighestRating = 100,
		LootType = 109,
		CastSpell = 110,
		LearnSkillLine = 112,
		EarnHonorableKill = 113,
		AcceptedSummons = 114,
		AchivementpointsReached = 115,
		RandomDungeonPlayercount = 119,
	}
}
