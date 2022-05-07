namespace WDBXEditor.Data.Contracts.Models.Items.Enums
{
	/// <summary>
	/// Different binding types for items.
	/// </summary>
	public enum ItemBindingType : byte
	{
		/// <summary>
		/// The item never becomes bound to the player.
		/// </summary>
		NoBinding = 0,

		/// <summary>
		/// The item becomes soulbound when looted by the player.
		/// </summary>
		BindsWhenPickedUp = 1,

		/// <summary>
		/// The item becomes soulbound when equipped by the player.
		/// </summary>
		BindsWhenEquipped = 2,

		/// <summary>
		/// The item becomes soulbound when used by the player.
		/// </summary>
		BindsWhenUsed = 3,

		/// <summary>
		/// The item is a quest item.
		/// </summary>
		QuestItem = 4,

		/// <summary>
		/// The item is an "iCoke Prize Voucher". DO NOT USE unless you're adding a new iCoke Prize Voucher.
		/// </summary>
		CokePrizeVoucher = 5
	}
}
