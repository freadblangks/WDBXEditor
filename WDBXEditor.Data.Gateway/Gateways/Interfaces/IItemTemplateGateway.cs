using WDBXEditor.Common.Utility.Types.Primitives;
using WDBXEditor.Data.Contracts.Models.Items;

namespace WDBXEditor.Data.Gateway.Gateways.Interfaces
{
	public interface IItemTemplateGateway
	{
		void TestGetItemTemplate();

		CompleteItemTemplate GetCompleteItemTemplateById(UInt24 entryId);
	}
}
