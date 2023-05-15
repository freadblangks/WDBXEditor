using System;
using System.Reflection;
using System.Collections.Generic;
using WDBXEditor.Data.Contexts;
using WDBXEditor.Data.Contracts.Attributes;
using WDBXEditor.Data.Contracts.Models.Items;
using WDBXEditor.Data.Repositories;
using WDBXEditor.Common.Utility.Types.Primitives;
using System.Security;
using WDBXEditor.Data.Helpers;
using WDBXEditor.Data.Repositories.Interfaces;
using WDBXEditor.Data.Services.Interfaces;

namespace WDBXEditor.Data.Services
{
    public class ItemTemplateService : IItemTemplateService
    {
        private IItemTemplateRepository _itemTemplateRepository;

        // TODO: Pass these in with a DTO.
        public ItemTemplateService(string hostname, string username, SecureString password)
        {
            _itemTemplateRepository = new ItemTemplateRepository(hostname, username, password, new MySqlDbContextFactory());
        }

        public ItemTemplateService(IItemTemplateRepository itemTemplateService)
        {
            _itemTemplateRepository = itemTemplateService;
        }

        public void TestGetItemTemplate()
        {
            CompleteItemTemplate result = _itemTemplateRepository.ReadItemTemplate(56810);
            //List<CompleteItemTemplate> results = _itemTemplateService.ReadItemTemplates();
            Console.WriteLine(result);
        }

        public CompleteItemTemplate GetCompleteItemTemplateById(UInt24 entryId)
        {
            return _itemTemplateRepository.ReadItemTemplate(entryId);
        }
    }
}
