using System;
using System.Collections.Generic;
using System.Text;

namespace Acmil.Api.Helpers.Interfaces
{
	interface IDbcImportExportHelper
	{
		string SqlDump(string file, string source, int buildNumber, string databaseName);
	}
}
