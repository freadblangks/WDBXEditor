using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Data;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Collections;
using Acmil.Core.Reader;
using Acmil.Common.Utility;
using Acmil.Common.Utility.Interfaces;
using Acmil.Common.Utility.Logging.Interfaces;
using Acmil.Core.Common.Enums;

namespace Acmil.Core.Storage
{
	public class Database
	{
		public Definition Definitions { get; private set; } = new Definition(); //= LoadDefinitions().ConfigureAwait(false).GetAwaiter().GetResult();
		public List<DBEntry> Entries { get; private set; } = new List<DBEntry>();
		public int BuildNumber { get; private set; } = (int)ExpansionFinalBuild.WotLK;

		public bool LoadDefinition(string definitionText)
		{
			return Definitions.LoadDefinition(definitionText);
		}

		/// <summary>
		/// Gets a <see cref="DBEntry"/> if the instance contains it.
		/// </summary>
		/// <param name="name">The name of the entry.</param>
		/// <returns>The <see cref="DBEntry"/>, if the instance contains it. Otherwise, <see langword="null"/>.</returns>
		public DBEntry GetDbEntry(string name)
		{
			return Entries.FirstOrDefault(entry => string.Compare(entry.EntryName, name, true) == 0);
		}

		/// <summary>
		/// Checks whether the instance contains the specified <see cref="DBEntry"/>.
		/// </summary>
		/// <param name="name">The name of the entry.</param>
		/// <returns>True if the instance contains the entry. Otherwise, false.</returns>
		public bool ContainsDbEntry(string name)
		{
			return GetDbEntry(name) != null;
		}

		// We should refactor this to be passed in with DI.
		//static Database()
		//{
		//	_utilityHelper = new UtilityHelper();
		//	_logger = _utilityHelper.Logger;
		//	//if (Definitions is null)
		//	//{
		//	//	LoadDefinitions().ConfigureAwait(false).GetAwaiter().GetResult();
		//	//}
		//}

		
	}
}
