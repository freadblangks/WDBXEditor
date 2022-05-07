using System;
using WDBXEditor.Data.Gateway.Gateways;
using WDBXEditor.Data.Gateway.Gateways.Interfaces;

namespace WDBXEditor.Extended.Api
{
	public class ItemTemplateApi
	{
		private IItemTemplateGateway _itemTemplateGateway;

		/// <summary>
		/// Initializes a new instance of the <see cref="ItemTemplateApi"/> class.
		/// </summary>
		public ItemTemplateApi()
		{
			_itemTemplateGateway = new ItemTemplateGateway();
		}

		public void TestGetItemTemplate()
		{
			_itemTemplateGateway.TestGetItemTemplate();
		}
	}
}