using System;
using Acmil.Data.Contracts.Attributes;

namespace Acmil.Data.Contracts.Models.Items.Enums
{
	/// <summary>
	/// Flags that are used to defined specific aspects of an item's behavior.
	/// </summary>
	[Flags]
	public enum ItemFlags : uint
	{
		/// <summary>
		/// (NOT IMPLEMENTED)
		/// </summary>
		[NotImplemented]
		HasNoPickup = 0x01,

		/// <summary>
		/// The item is conjured (e.g. mage food).
		/// </summary>
		/// // TODO: Determine whether this dictates a conjured item's tendency to disappear.
		ConjuredItem = 0x02,

		/// <summary>
		/// The item can be opened by right-clicking.
		/// </summary>
		Openable = 0x04,

		/// <summary>
		/// (NOT IMPLEMENTED)
		/// </summary>
		[NotImplemented]
		HeroicTooltip = 0x08,

		/// <summary>
		/// (NOT IMPLEMENTED) The item has been deprecated.
		/// </summary>
		[NotImplemented]
		Deprecated = 0x010,

		/// <summary>
		/// The item cannot be destroyed, except by using a spell (e.g. the item is a reagent for the spell).
		/// </summary>
		CannotBeDestroyed = 0x020,

		/// <summary>
		/// (NOT IMPLEMENTED) The item's spells are castable by players.
		/// </summary>
		[NotImplemented]
		PlayerCast = 0x040,

		/// <summary>
		/// The item has no 30-second default cooldown when equipped.
		/// </summary>
		/// <remarks>
		/// For "on use" items.
		/// </remarks>
		NoDefaultCooldownWhenEquipped = 0x080,

		/// <summary>
		/// (NOT IMPLEMENTED)
		/// </summary>
		[NotImplemented]
		MultiLootQuest = 0x0100,

		/// <summary>
		/// The item can wrap other items.
		/// </summary>
		/// // TODO: Look into whether this is how "loot bags" work.
		Wrapper = 0x0200,

		/// <summary>
		/// (NOT IMPLEMENTED)
		/// </summary>
		[NotImplemented]
		UsesResources = 0x0400,

		/// <summary>
		/// The item is party loot and can be looted by all.
		/// </summary>
		PartyLoot = 0x0800,

		/// <summary>
		/// The item is refundable.
		/// </summary>
		/// // TODO: Figure out if this means that an item can be refunded at all or if it means
		/// you're elligible for a full refund.
		Refundable = 0x01000,

		/// <summary>
		/// The item is a Charter (for a Guild or Arena Team).
		/// </summary>
		Charter = 0x02000,

		/// <summary>
		/// (NOT IMPLEMENTED) The item contains readable text.
		/// </summary>
		/// <remarks>
		/// Some readable items have this, but not all.
		/// </remarks>
		[NotImplemented]
		HasText = 0x04000,

		/// <summary>
		/// (NOT IMPLEMENTED) The item cannot be disenchanted.
		/// </summary>
		/// <remarks>
		/// This is handled by the <see cref="BaseArmamentEquipableItemTemplate.RequiredDisenchantSkill"/> field.
		/// </remarks>
		[NotImplemented]
		NoDisenchant = 0x08000,

		/// <summary>
		/// (NOT IMPLEMENTED) Likely used for real time duration for items.
		/// // TODO: Add reference to where this is implemented now.
		/// </summary>
		[NotImplemented]
		RealDuration = 0x010000,

		/// <summary>
		/// (NOT IMPLEMENTED) Likely intended to remove the "Made by XX" message on crafted/summoned
		/// items or for signing Charters.
		/// </summary>
		[NotImplemented]
		NoCreator = 0x020000,

		/// <summary>
		/// The item can be prospected by Jewelcrafters.
		/// </summary>
		Prospectable = 0x040000,

		/// <summary>
		/// The item is Unique-Equipped. This means that the player can only have on instance
		/// of the item equipped at one time. The text "Unique-Equipped" will display on the item.
		/// </summary>
		UniqueEquipped = 0x080000,

		/// <summary>
		/// (NOT IMPLEMENTED)
		/// </summary>
		[NotImplemented]
		IgnoreForAuras = 0x0100000,

		/// <summary>
		/// The item can be used during an Arena match.
		/// </summary>
		UsableInArena = 0x0200000,

		/// <summary>
		/// The item will show "Throwable" on its tooltip.
		/// </summary>
		ShowThrowableTooltip = 0x0400000,

		/// <summary>
		/// The item can be used while shapeshifted.
		/// </summary>
		UsableWhileShapeshifted = 0x0800000,

		/// <summary>
		/// (NOT IMPLEMENTED)
		/// </summary>
		[NotImplemented]
		HasQuestGlow = 0x01000000,

		/// <summary>
		/// The item is a profession recipe that can only be looted if the player
		/// meets the requirements to learn its spell and they don't already know it.
		/// </summary>
		ProfessionRecipeLootRestriction = 0x02000000,

		/// <summary>
		/// The item is not usable in an Arena match.
		/// </summary>
		NotUsableInArena = 0x04000000,

		/// <summary>
		/// The item is Bind to Account.
		/// </summary>
		/// TODO: Check if this only displays it on the tooltip.
		BindToAccount = 0x08000000,

		/// <summary>
		/// The item casts its spell with the "triggered" flag and ignores reagents.
		/// </summary>
		TriggersSpellWithNoReagentCost = 0x010000000,

		/// <summary>
		/// The item is millable by Scribes.
		/// </summary>
		Millable = 0x020000000,

		/// <summary>
		/// (NOT IMPLEMENTED)
		/// </summary>
		[NotImplemented]
		ReportToGuildChat = 0x040000000,

		/// <summary>
		/// (NOT IMPLEMENTED)
		/// </summary>
		[NotImplemented]
		NoProgressiveLoot = 0x080000000
	}
}
