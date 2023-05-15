using WDBXEditor.Common.Utility.Types.Primitives;
using WDBXEditor.Data.Contracts.Models.Items;

namespace WDBXEditor.Data.Services.Interfaces
{
    public interface IItemTemplateService
    {
        void TestGetItemTemplate();

        CompleteItemTemplate GetCompleteItemTemplateById(UInt24 entryId);
    }
}
