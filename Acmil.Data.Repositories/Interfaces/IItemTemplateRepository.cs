using Acmil.Data.Contracts.Connections;
using Acmil.Data.Contracts.Models.Items;
using Acmil.Data.Contracts.Types.Primitives;

namespace Acmil.Data.Repositories.Interfaces
{
	/// <summary>
	/// Interface describing a repository class for interacting with Item Templates.
	/// </summary>
	public interface IItemTemplateRepository
	{
		CompleteItemTemplate ReadItemTemplate(MySqlConnectionInfo connectionInfo, UInt24 entryId);
	}
}
