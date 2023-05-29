using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acmil.Data.Services.Interfaces
{
	public interface IDbcService
	{
		public void LoadDbcIntoWorldDatabase(string dbcPath, string tableName = null);
	}
}
