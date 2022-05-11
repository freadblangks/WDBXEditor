using System;
using WDBXEditor.Common.Utility.Types.Primitives;
using WDBXEditor.Data.Contracts.Models.Items;
using WDBXEditor.Data.Gateway.Gateways;
using WDBXEditor.Data.Gateway.Gateways.Interfaces;

namespace WDBXEditor.Extended.Api
{
	public class ItemTemplateManager
	{
		private IItemTemplateGateway _itemTemplateGateway;

		/// <summary>
		/// Initializes a new instance of the <see cref="ItemTemplateManager"/> class.
		/// </summary>
		public ItemTemplateManager()
		{
			_itemTemplateGateway = new ItemTemplateGateway();
		}

		public void TestGetItemTemplate()
		{
			_itemTemplateGateway.TestGetItemTemplate();
		}

		public CompleteItemTemplate GetCompleteItemTemplate(uint entryId)
		{
			return _itemTemplateGateway.GetCompleteItemTemplateById((UInt24)entryId);
		}
	}
}