﻿using Acmil.Data.Contracts.Models.Items;
using WDBXEditor.Common.Utility.Types.Primitives;

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
