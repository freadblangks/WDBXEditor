using WDBXEditor.Data.Contracts.Models.Items;

namespace WDBXEditor.Data.Services.Interfaces
{
	/// <summary>
	/// Service for interacting with Item Templates.
	/// </summary>
	public interface IItemTemplateService
	{
		CompleteItemTemplate ReadItemTemplate(int entryId);
	}
}
