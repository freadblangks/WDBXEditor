using Acmil.Data.Contracts.Models.Items;
using Acmil.Data.Contracts.Models.Items.Enums;
using System.Collections.Generic;
using WDBXEditor.Common.Utility.Types.Primitives;

namespace Acmil.Api.Managers.Interfaces
{
	public interface IItemTemplateManager
	{
		/// <summary>
		/// Gets an Item Template.
		/// </summary>
		/// <param name="entryId">The entry ID uniquely identifying the Item Template.</param>
		/// <returns>The Item Template with the specified entry ID.</returns>
		CompleteItemTemplate GetCompleteItemTemplate(UInt24 entryId);

		/// <summary>
		/// Gets all Item Templates matching the specified <paramref name="name"/>. Results may be
		/// further filtered using optional the <paramref name="itemClass"/> and <paramref name="itemSubclass"/> parameters.
		/// </summary>
		/// <param name="name">The name of the Item Template.</param>
		/// <param name="itemClass">
		/// An ID referring to an Item Class to filter results by.
		/// For supported values see <see cref="ItemClass"/>.
		/// </param>
		/// <param name="itemSubclass">An ID referring to an Item Subclass to filter results by.</param>
		/// <returns>All Item Templates matching the specified critieria.</returns>
		List<CompleteItemTemplate> GetCompleteItemTemplates(string name, byte? itemClass, byte? itemSubclass);
	}
}
