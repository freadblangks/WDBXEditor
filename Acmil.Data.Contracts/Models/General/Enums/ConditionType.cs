namespace Acmil.Data.Contracts.Models.General.Enums
{
	/// <summary>
	/// Types of conditions.
	/// </summary>
	/// <remarks>
	/// Used in `conditions.ConditionTypeOrReference` and possibly
	/// for `Start_Event`, `Fail_Event`, and `Timer_Start_Event`
	/// in `AchievementCriteria.dbc`
	/// </remarks>
	public enum ConditionType : byte
	{
		/// <summary>
		/// No condition in use.
		/// </summary>
		None = 0,
		Aura = 1,
		Item = 2,
		ItemEquipped = 3,
		ZoneId = 4,
		ReputationRank = 5,
		Team = 6,
		Skill = 7,
		QuestRewarded = 8,
		QuestTaken = 9,
		DrunkenState = 10,
		WorldState = 11,
		ActiveEvent = 12,
		InstanceInfo = 13,
		QuestNone = 14,
		Class = 15,
		Race = 16,
		Achievement = 17,
		Title = 18,
		SpawnMask = 19,
		Gender = 20,
		UnitState = 21,
		MapId = 22,
		AreaId = 23,
		CreatureType = 24,
		Spell = 25,
		PhaseMask = 26,
		Level = 27,
		QuestComplete = 28,
		NearCreature = 29,
		NearGameObject = 30,
		EntryGuid = 31,
		TypeMask = 32,
		RelationTo = 33,
		ReactionTo = 34,
		DistanceTo = 35,
		Alive = 36,
		HpValue = 37,
		HpPercent = 38,
		RealmAchievement = 39,
		InWater = 40,
		StandState = 42,
		DailyQuestDone = 43,
		Charmed = 44,
		PetType = 45,
		Taxi = 46,
		QuestState = 47,
		QuestObjectiveProgress = 48,
		Max = 49
	}
}
