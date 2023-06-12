using Acmil.Data.Contracts.Models.Items;
using Acmil.Data.Contracts.Types.Primitives;

namespace Acmil.Data.Services.Interfaces
{
	public interface IItemTemplateService
    {
        void TestGetItemTemplate();

        CompleteItemTemplate GetCompleteItemTemplateById(UInt24 entryId);
    }
}
