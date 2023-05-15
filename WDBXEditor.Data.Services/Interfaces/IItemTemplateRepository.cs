﻿using WDBXEditor.Common.Utility.Types.Primitives;
using WDBXEditor.Data.Contracts.Models.Items;

namespace WDBXEditor.Data.Repositories.Interfaces
{
	/// <summary>
	/// Interface describing a repository class for interacting with Item Templates.
	/// </summary>
	public interface IItemTemplateRepository
	{
		CompleteItemTemplate ReadItemTemplate(UInt24 entryId);
	}
}