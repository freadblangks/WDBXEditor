using System;
using WDBXEditor.Data.Contracts.Models.Items;
using WDBXEditor.Data.Repositories;
using WDBXEditor.Common.Utility.Types.Primitives;
using System.Security;
using WDBXEditor.Data.Repositories.Interfaces;
using WDBXEditor.Data.Services.Interfaces;
using Acmil.Data.Helpers;

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
            Console.WriteLine(result);
        }

        public CompleteItemTemplate GetCompleteItemTemplateById(UInt24 entryId)
        {
            return _itemTemplateRepository.ReadItemTemplate(entryId);
        }
    }
}
