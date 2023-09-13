using Acmil.Api.Managers.Interfaces;
using Acmil.Data.Contracts.Models.Items;
using Acmil.Data.Services.Interfaces;
using Acmil.Data.Contracts.Types.Primitives;
using Acmil.Common.Utility.Connections;

namespace Acmil.Api.Managers
{
	public class ItemTemplateManager : IItemTemplateManager
	{
		private IItemTemplateService _itemTemplateService;

		public ItemTemplateManager(IItemTemplateService itemTemplateService)
		{
			_itemTemplateService = itemTemplateService;
		}

		//public void TestGetItemTemplate()
		//{
		//	_itemTemplateService.TestGetItemTemplate();
		//}

		public CompleteItemTemplate GetCompleteItemTemplate(UInt24 entryId)
		{
			return _itemTemplateService.GetCompleteItemTemplateById(entryId);
		}

		public List<CompleteItemTemplate> GetCompleteItemTemplates(
			string name,
			byte? itemClass,
			byte? itemSubclass
		)
		{
			throw new NotImplementedException();
		}
	}
}