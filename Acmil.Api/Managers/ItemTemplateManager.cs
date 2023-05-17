using Acmil.Api.Managers.Interfaces;
using Acmil.Data.Contracts.Models.Items;
using System;
using System.Collections.Generic;
using System.Security;
using WDBXEditor.Common.Utility.Types.Primitives;
using WDBXEditor.Data.Services;
using WDBXEditor.Data.Services.Interfaces;

namespace Acmil.Api.Managers
{
	public class ItemTemplateManager : IItemTemplateManager
	{
		private IItemTemplateService _itemTemplateService;

		// TODO: Pass these in as a DTO.
		public ItemTemplateManager(string hostname, string username, SecureString password)
		{
			_itemTemplateService = new ItemTemplateService(hostname, username, password);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ItemTemplateManager"/>.
		/// </summary>
		public ItemTemplateManager(IItemTemplateService itemTemplateRepository)
		{
			_itemTemplateService = itemTemplateRepository;
		}

		public void TestGetItemTemplate()
		{
			_itemTemplateService.TestGetItemTemplate();
		}

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