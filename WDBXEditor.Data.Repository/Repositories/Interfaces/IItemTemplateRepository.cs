using WDBXEditor.Common.Utility.Types.Primitives;
using WDBXEditor.Data.Contracts.Models.Items;

namespace WDBXEditor.Data.Repository.Repositories.Interfaces
{
	public interface IItemTemplateRepository
	{
		void TestGetItemTemplate();

		CompleteItemTemplate GetCompleteItemTemplateById(UInt24 entryId);
	}
}
