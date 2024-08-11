using Acmil.Data.Contracts.Attributes;

namespace Acmil.Data.Contracts.Models.Items
{
	/// <summary>
	/// Item template for items that open a loot window when used.
	/// </summary>
	public class LootContainerItemTemplate : BaseItemTemplate
	{
		/// <summary>
		/// The minimum amount of money (in copper) the item can contain.
		/// </summary>
		[MySqlColumnName("minMoneyLoot")]
		public uint MinMoneyLoot { get; set; } = 0;

		/// <summary>
		/// The maximum amount of money (in copper) the item can contain.
		/// </summary>
		[MySqlColumnName("maxMoneyLoot")]
		public uint MaxMoneyLoot { get; set; } = 0;
	}
}
