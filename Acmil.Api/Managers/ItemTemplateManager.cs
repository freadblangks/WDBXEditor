using Acmil.Api.Managers.Interfaces;
using Acmil.Data.Contracts.Models.Items;
using Acmil.Data.Services.Interfaces;
using Acmil.Data.Contracts.Types.Primitives;
using Acmil.Data.Contracts.Connections;

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

		public CompleteItemTemplate GetCompleteItemTemplate(MySqlConnectionInfo connectionInfo, UInt24 entryId)
		{
			return _itemTemplateService.GetCompleteItemTemplateById(connectionInfo, entryId);
		}

		public List<CompleteItemTemplate> GetCompleteItemTemplates(
			MySqlConnectionInfo connectionInfo,
			string name,
			byte? itemClass,
			byte? itemSubclass
		)
		{
			throw new NotImplementedException();
		}
	}
}