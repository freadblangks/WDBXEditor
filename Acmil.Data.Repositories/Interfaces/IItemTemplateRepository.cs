using Acmil.Common.Utility.Types.Primitives;
using Acmil.Data.Contracts.Models.Items;

namespace Acmil.Data.Repositories.Interfaces
{
	/// <summary>
	/// Interface describing a repository class for interacting with Item Templates.
	/// </summary>
	public interface IItemTemplateRepository
	{
		CompleteItemTemplate ReadItemTemplate(UInt24 entryId);
	}
}
