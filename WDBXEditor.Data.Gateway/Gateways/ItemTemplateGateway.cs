using System;
using System.Reflection;
using System.Collections.Generic;
using WDBXEditor.Data.Contexts;
using WDBXEditor.Data.Contracts.Attributes;
using WDBXEditor.Data.Contracts.Models.Items;
using WDBXEditor.Data.Gateway.Gateways.Interfaces;
using WDBXEditor.Data.Services;

namespace WDBXEditor.Data.Gateway.Gateways
{
	public class ItemTemplateGateway : IItemTemplateGateway
	{
		public void TestGetItemTemplate()
		{
			string connectionString = "Server=localhost;Database=acore_world;Uid=root;Pwd=";
			var worldContext = new MySqlContext(connectionString);
			var itemTemplateService = new ItemTemplateService(worldContext);
			CompleteItemTemplate result = itemTemplateService.ReadItemTemplate(56810);
			List<CompleteItemTemplate> results = itemTemplateService.ReadItemTemplates();
			Console.WriteLine(result);
		}
	}
}
