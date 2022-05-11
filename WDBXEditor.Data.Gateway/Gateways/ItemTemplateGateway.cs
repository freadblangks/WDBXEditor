using System;
using System.Reflection;
using System.Collections.Generic;
using WDBXEditor.Data.Contexts;
using WDBXEditor.Data.Contracts.Attributes;
using WDBXEditor.Data.Contracts.Models.Items;
using WDBXEditor.Data.Gateway.Gateways.Interfaces;
using WDBXEditor.Data.Services;
using WDBXEditor.Data.Services.Interfaces;
using WDBXEditor.Common.Utility.Types.Primitives;

namespace WDBXEditor.Data.Gateway.Gateways
{
	public class ItemTemplateGateway : IItemTemplateGateway
	{
		private IItemTemplateService _itemTemplateService;

		public ItemTemplateGateway()
		{
			string connectionString = "Server=localhost;Database=acore_world;Uid=root;Pwd=";
			var worldContext = new MySqlContext(connectionString);
			_itemTemplateService = new ItemTemplateService(worldContext);
		}

		public void TestGetItemTemplate()
		{
			CompleteItemTemplate result = _itemTemplateService.ReadItemTemplate(56810);
			//List<CompleteItemTemplate> results = _itemTemplateService.ReadItemTemplates();
			Console.WriteLine(result);
		}

		public CompleteItemTemplate GetCompleteItemTemplateById(UInt24 entryId)
		{
			return _itemTemplateService.ReadItemTemplate(entryId);
		}
	}
}
