using System;
using System.Collections.Generic;
using System.Text;
using WDBXEditor.Data.Contracts.IO.Enums;

namespace WDBXEditor.Extended.Api.Managers.Interfaces
{
	/// <summary>
	/// Manager for interacting with DBC files.
	/// </summary>
	public interface IDbcManager
	{
		void LoadDbcIntoSql(string filepath, int buildNumber);

		void LoadDbcIntoSql(string dbcDirectory, string dbcFileName, int buildNumber);

		void LoadDbcFromSql(string filepath, int buildNumber, string tableName, UpdateMode updateStrategy);

		void LoadDbcFromSql(string dbcDirectory, string dbcFileName, int buildNumber, string tableName, UpdateMode updateStrategy);
	}
}
