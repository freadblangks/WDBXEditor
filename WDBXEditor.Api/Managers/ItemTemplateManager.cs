using System;
using System.Collections.Generic;
using System.Security;
using WDBXEditor.Common.Utility.Types.Primitives;
using WDBXEditor.Data.Contracts.Models.Items;
using WDBXEditor.Data.Repository.Repositories;
using WDBXEditor.Data.Repository.Repositories.Interfaces;
using WDBXEditor.Extended.Api.Managers.Interfaces;

namespace WDBXEditor.Extended.Api.Managers
{
	public class ItemTemplateManager : IItemTemplateManager
	{
		private IItemTemplateRepository _itemTemplateService;

		// TODO: Pass these in as a DTO.
		public ItemTemplateManager(string hostname, string username, SecureString password)
		{
			_itemTemplateService = new ItemTemplateRepository(hostname, username, password);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ItemTemplateManager"/>.
		/// </summary>
		public ItemTemplateManager(IItemTemplateRepository itemTemplateRepository)
		{
			_itemTemplateService = itemTemplateRepository;
		}

		public void TestGetItemTemplate()
		{
			_itemTemplateService.TestGetItemTemplate();
		}

		//public CompleteItemTemplate GetCompleteItemTemplate(uint entryId)
		//{
		//	return _itemTemplateGateway.GetCompleteItemTemplateById((UInt24)entryId);
		//}

		public CompleteItemTemplate GetCompleteItemTemplate(UInt24 entryId)
		{
			return _itemTemplateService.GetCompleteItemTemplateById(entryId);
		}

		public List<CompleteItemTemplate> GetCompleteItemTemplates(string name, byte? itemClass, byte? itemSubclass)
		{
			throw new NotImplementedException();
		}
	}
}