using Acmil.Data.Contracts.Attributes;
using Acmil.Common.Utility.Types.Primitives;

namespace Acmil.Data.Contracts.Models.Items
{
	/// <summary>
	/// Item template for an item that starts a quest.
	/// </summary>
	public class QuestStartingItemTemplate : BaseItemTemplate
	{
		/// <summary>
		/// The ID of the quest started by the item.
		/// </summary>
		[MySqlColumnName("startquest")]
		public UInt24 QuestId { get; set; } = 0;
	}
}
