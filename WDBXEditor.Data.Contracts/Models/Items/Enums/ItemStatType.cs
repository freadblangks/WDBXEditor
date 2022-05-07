using WDBXEditor.Data.Contracts.Attributes;

namespace WDBXEditor.Data.Contracts.Models.Items.Enums
{
	/// <summary>
	/// The stat bonuses that can be granted by an equipped item.
	/// </summary>
	public enum ItemStatType : byte
	{
		// TODO: Determine if these have any effect.
		Mana = 0,
		Health = 1,

		/// <summary>
		/// The stat modified is Agility.
		/// </summary>
		Agility = 3,

		/// <summary>
		/// The stat modified is Strength.
		/// </summary>
		Strength = 4,

		/// <summary>
		/// The stat modified is Intellect.
		/// </summary>
		Intellect = 5,

		/// <summary>
		/// The stat modified is Spirit.
		/// </summary>
		Spirit = 6,

		/// <summary>
		/// The stat modified is Stamina.
		/// </summary>
		Stamina = 7,

		/// <summary>
		/// The stat modified is Defense Rating.
		/// </summary>
		DefenseSkillRating = 12,

		/// <summary>
		/// The stat modified is Dodge Rating.
		/// </summary>
		DodgeRating = 13,

		/// <summary>
		/// The stat modified is Parry Rating.
		/// </summary>
		ParryRating = 14,

		/// <summary>
		/// The stat modified is Block Rating.
		/// </summary>
		/// <remarks>
		/// Displayed as "shield block rating" on a shield.
		/// </remarks>
		/// TODO: Verify this.
		BlockRating = 15,

		// TODO: Figure out what the fuck is going on with all these guys.
		HitRatingMelee = 16,
		HitRatingRanged = 17,
		HitRatingSpell = 18,
		CritRatingMelee = 19,
		CritRatingRanged = 20,
		CritRatingSpell = 21,
		HitTakenRatingMelee = 22,
		HitTakenRatingRanged = 23,
		HitTakenRatingSpell = 24,
		CritTakenRatingMelee = 25,
		CritTakenRatingRanged = 26,
		CritTakenRatingSpell = 27,
		HasteRatingMelee = 28,
		HasteRatingRanged = 29,
		HasteRatingSpell = 30,

		/// <summary>
		/// The stat modified is Hit Rating.
		/// </summary>
		HitRating = 31,

		/// <summary>
		/// The stat modified is Crit Rating.
		/// </summary>
		CritRating = 32,

		// TODO: Figure out what the lads be.
		HitTakenRating = 33,
		CritTakenRating = 34,

		/// <summary>
		/// The stat modified is Resilience.
		/// </summary>
		ResilienceRating = 35,

		/// <summary>
		/// The stat modified is Haste Rating.
		/// </summary>
		HasteRating = 36,

		/// <summary>
		/// The stat modified is Expertise.
		/// </summary>
		ExpertiseRating = 37,

		/// <summary>
		/// The stat modified is melee Attack Power.
		/// </summary>
		AttackPowerMelee = 38,

		/// <summary>
		/// The stat modified is ranged Attack Power.
		/// </summary>
		AttackPowerRanged = 39,

		/// <summary>
		/// (NOT IMPLEMENTED) as of 3.3.
		/// </summary>
		[NotImplemented]
		AttackPowerFeral = 40,

		// TODO: Figure out if these still do anything.
		SpellPowerHealing = 41,
		SpellPowerDamage = 42,

		/// <summary>
		/// The stat modified is Mana Regen (a.k.a. Mana Per 5)
		/// </summary>
		ManaRegeneration = 43,

		/// <summary>
		/// The stat modified is Armor Penetration Rating.
		/// </summary>
		ArmorPenetrationRating = 44,

		/// <summary>
		/// The stat modified is Spell Power.
		/// </summary>
		SpellPower = 45,

		/// <summary>
		/// The stat modified is Health Regeneration.
		/// </summary>
		HealthRegeneration = 46,

		/// <summary>
		/// The stat modified is Spell Penetration.
		/// </summary>
		SpellPenetration = 47,

		/// <summary>
		/// The stat provided is Shield Block Value.
		/// </summary>
		BlockValue = 48,

	}
}
