using Acmil.Data.Contracts.Models.Items;
using WDBXEditor.Common.Utility.Types.Primitives;

namespace WDBXEditor.Data.Services.Interfaces
{
	public interface IItemTemplateService
    {
        void TestGetItemTemplate();

        CompleteItemTemplate GetCompleteItemTemplateById(UInt24 entryId);
    }
}
