using System;

namespace WDBXEditor.Data.Contracts.Models.Items.Enums
{
	/// <summary>
	/// Flags that are used to defined specific aspects of an item's behavior.
	/// </summary>
	[Flags]
	public enum ItemFlagsCustom : uint
	{
		/// <summary>
		/// The item's <see cref="BaseItemTemplate.DurationInSeconds"/> property will tick even if the player is offline.
		/// </summary>
		DurationRealTime = 1,

		/// <summary>
		/// No quest status will be checked when the item drops.
		/// </summary>
		/// TODO: Figure out what this means.
		IgnoreQuestStatus = 2,

		/// <summary>
		/// The item will always follow Group Loot/Master Loot/Need Before Greed Loot rules.
		/// </summary>
		/// TODO: Figure out what this means.
		FollowLootRules = 4
	}
}
