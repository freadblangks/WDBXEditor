using Acmil.Data.Repositories.Interfaces;
using Acmil.Data.Contracts.Models.Items;
using Acmil.Data.Services.Interfaces;
using Acmil.Data.Contracts.Types.Primitives;
using Acmil.Common.Utility.Connections;

namespace Acmil.Data.Services
{
	public class ItemTemplateService : IItemTemplateService
	{
		private IItemTemplateRepository _itemTemplateRepository;

		// TODO: Pass these in with a DTO.
		//public ItemTemplateService(string hostname, string username, SecureString password)
		//{
		//	_itemTemplateRepository = new ItemTemplateRepository(hostname, username, password, new MySqlDbContextFactory());
		//}

		public ItemTemplateService(IItemTemplateRepository itemTemplateRepository)
		{
			_itemTemplateRepository = itemTemplateRepository;
		}

		//public void TestGetItemTemplate()
		//{
		//	CompleteItemTemplate result = _itemTemplateRepository.ReadItemTemplate(56810);
		//	Console.WriteLine(result);
		//}

		public CompleteItemTemplate GetCompleteItemTemplateById(UInt24 entryId)
		{
			return _itemTemplateRepository.ReadItemTemplate(entryId);
		}
	}
}
