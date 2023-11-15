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

		/// <summary>
		/// A requirement where the character must reach a certain level in a particular skill.
		/// </summary>
		ReachSkillLevel = 7,

		/// <summary>
		/// A requirement where the character must complete a particular Achievement.
		/// </summary>
		CompleteAchievement = 8,

		/// <summary>
		/// A requirement where the character must complete a certain number of total quests.
		/// </summary>
		CompleteQuestCount = 9,

		/// <summary>
		/// A requirement where the character must complete a daily quest once a day for a consecutive number of days.
		/// </summary>
		CompleteDailyQuestDaily = 10,

		/// <summary>
		/// A requirement where the character must complete a certain number of quests in a particular zone.
		/// </summary>
		CompleteQuestsInZone = 11,

		/// <summary>
		/// A requirement where the character must deal a certain total amount of damage.
		/// </summary>
		DamageDone = 13,

		/// <summary>
		/// A requirement where the character must complete a total number of daily quests.
		/// </summary>
		CompleteDailyQuest = 14,

		/// <summary>
		/// A requirement where the character must complete a particular battleground.
		/// </summary>
		CompleteBattleground = 15,

		/// <summary>
		/// A requirement where the character must die in a particular map.
		/// </summary>
		DeathAtMap = 16,

		/// <summary>
		/// A requirement where the character must die in a dungeon or raid with a specific group size.
		/// </summary>
		DeathInDungeon = 18,

		/// <summary>
		/// A requirement where the character must complete a dungeon or raid of a specific player count.
		/// </summary>
		CompleteDungeonOrRaidOfMaxPlayerCount = 19,

		/// <summary>
		/// A requirement where the character must be killed by a creature.
		/// </summary>
		KilledByCreature = 20,

		/// <summary>
		/// A requirement where the character must fall from a specific height without dying.
		/// </summary>
		FallWithoutDying = 24,

		/// <summary>
		/// A requirement where the character must die due to a specific kind of environmental damage.
		/// </summary>
		DeathsFromEnvironmentalDamage = 26,

		/// <summary>
		/// A requirement where the character must complete a particular quest a certain number of times.
		/// </summary>
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
