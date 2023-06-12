using Acmil.Api.Managers.Interfaces;
using Acmil.Data.Contracts.Connections;
using Acmil.Data.Contracts.Models.Items;
using Acmil.Data.Contracts.Types.Primitives;
using Acmil.PowerShell.Engines.Ioc;
using Microsoft.Extensions.DependencyInjection;

namespace Acmil.PowerShell.Engines
{
	public class ItemTemplateEngine : IDisposable
	{
		private IItemTemplateManager _itemTemplateManager;

		public ItemTemplateEngine()
		{
			_itemTemplateManager = DependencyInjector.ServiceProvider.GetService<IItemTemplateManager>();
		}

		public void Dispose()
		{
			//_itemTemplateManager.Dispose();
		}

		//public static CompleteItemTemplate GetCompleteItemTemplate(MySqlConnectionInfo connectionInfo, UInt24 entryId)
		public static object GetCompleteItemTemplate(object connectionInfo, uint entryId)
		{
			var itemTemplateManager = DependencyInjector.ServiceProvider.GetService<IItemTemplateManager>();
			return itemTemplateManager.GetCompleteItemTemplate((UInt24)entryId);
		}

		//public List<CompleteItemTemplate> GetCompleteItemTemplates(MySqlConnectionInfo connectionInfo, string name, byte? itemClass, byte? itemSubclass)
		//{
		//	return _itemTemplateManager.GetCompleteItemTemplates(name, itemClass, itemSubclass);
		//}
	}
}
