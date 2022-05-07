using System;
using System.Collections.Generic;
using System.Text;
using WDBXEditor.Common.Utility.Types.Primitives;
using WDBXEditor.Data.Contracts.Attributes;

namespace WDBXEditor.Data.Contracts.Models.Items
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
