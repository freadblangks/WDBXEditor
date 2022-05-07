namespace WDBXEditor.Data.Contracts.Models.Items.Enums.SubClasses
{
	/// <summary>
	/// Subclasses of the <see cref="ItemClass.Reagent"/> item class.
	/// </summary>
	public enum ItemSubClassReagent : byte
	{
		/// <summary>
		/// Subclass indicating that an item is a Reagent. Although this is a valid subclass, the only player-obtainable
		/// item in it is "Ankh". If you're adding a new Reagent, add it as <see cref="ItemSubClassMiscellaneous.Reagent"/>
		/// under the <see cref="ItemClass.Miscellaneous"/> class.
		/// </summary>
		Reagent = 0,
	}
}
