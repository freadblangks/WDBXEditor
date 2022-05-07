using System;
using System.Collections.Generic;
using WDBXEditor.Data.Contexts;
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
			List<CompleteItemTemplate> result = itemTemplateService.ReadItemTemplates();
			Console.WriteLine(result);
		}
	}
}
