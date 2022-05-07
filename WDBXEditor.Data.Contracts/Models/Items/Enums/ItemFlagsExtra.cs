using System;

namespace WDBXEditor.Data.Contracts.Models.Items.Enums
{
	/// <summary>
	/// More flags that are used to defined specific aspects of an item's behavior.
	/// </summary>
	[Flags]
	public enum ItemFlagsExtra : uint
	{
		/// <summary>
		/// The item is only usable by Horde players.
		/// </summary>
		/// TODO: Figure out what this means.
		HordeOnly = 0x01,

		/// <summary>
		/// The item is only usable by Alliance players.
		/// </summary>
		/// TODO: Figure out what this means.
		AllianceOnly = 0x02,

		/// <summary>
		/// If the item uses the ExtendedCost field in the `npc_vendor` table,
		/// gold is also required.
		/// </summary>
		ExtendedCostAlsoCostsGold = 0x04,

		/// <summary>
		/// When the item is rolled for, the Need roll option is disabled.
		/// </summary>
		NeedRoleDisabled = 0x0100,

		/// <summary>
		/// When the item is rolled for, the Need roll option is disabled.
		/// </summary>
		/// TODO: See if this works.
		NeedRoleDisabled2 = 0x0200,

		/// <summary>
		/// UNKNOWN
		/// </summary>
		HasNormalPrice = 0x04000,

		/// <summary>
		/// UNKNOWN
		/// </summary>
		BNetAccountBound = 0x020000,

		/// <summary>
		/// UNKNOWN
		/// </summary>
		CannotBeTransmog = 0x0200000,

		/// <summary>
		/// UNKNOWN
		/// </summary>
		CannotTransmog = 0x0400000,

		/// <summary>
		/// UNKNOWN
		/// </summary>
		CanTransmog = 0x0800000,
	}
}
