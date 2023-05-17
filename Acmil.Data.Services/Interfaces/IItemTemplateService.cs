using Acmil.Common.Utility.Types.Primitives;
using Acmil.Data.Contracts.Models.Items;

namespace WDBXEditor.Data.Services.Interfaces
{
	public interface IItemTemplateService
    {
        void TestGetItemTemplate();

        CompleteItemTemplate GetCompleteItemTemplateById(UInt24 entryId);
    }
}
