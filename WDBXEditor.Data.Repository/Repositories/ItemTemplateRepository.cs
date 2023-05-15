using System;
using System.Reflection;
using System.Collections.Generic;
using WDBXEditor.Data.Contexts;
using WDBXEditor.Data.Contracts.Attributes;
using WDBXEditor.Data.Contracts.Models.Items;
using WDBXEditor.Data.Services;
using WDBXEditor.Data.Services.Interfaces;
using WDBXEditor.Common.Utility.Types.Primitives;
using WDBXEditor.Data.Repository.Repositories.Interfaces;
using System.Security;
using WDBXEditor.Data.Helpers;

namespace WDBXEditor.Data.Repository.Repositories
{
	public class ItemTemplateRepository : IItemTemplateRepository
	{
		private IItemTemplateService _itemTemplateService;

		// TODO: Pass these in with a DTO.
		public ItemTemplateRepository(string hostname, string username, SecureString password)
		{
			_itemTemplateService = new ItemTemplateService(hostname, username, password, new MySqlDbContextFactory());
		}

		public ItemTemplateRepository(IItemTemplateService itemTemplateService)
		{
			_itemTemplateService = itemTemplateService;
			//string connectionString = "Server=localhost;Database=acore_world;Uid=root;Pwd=";
			//var worldContext = new MySqlContext(connectionString);
			//_itemTemplateService = new ItemTemplateService(worldContext);
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
