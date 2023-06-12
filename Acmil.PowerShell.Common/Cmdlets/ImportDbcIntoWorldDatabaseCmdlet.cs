using Acmil.Api.Managers;
using Acmil.Api.Managers.Interfaces;
using Acmil.Data.Contracts.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Acmil.PowerShell.Common.Cmdlets
{
	[Cmdlet(VerbsData.Import, "DbcIntoWorldDatabase")]
	public class ImportDbcIntoWorldDatabaseCmdlet : PSCmdlet
	{
		IDbcManager _dbcManager;

		[Parameter(Mandatory = true)]
		public MySqlConnectionInfo ConnectionInfo { get; set; }

		[Parameter(Mandatory = true)]
		public string DbcPath { get; set; }

		protected override void BeginProcessing()
		{
			//_dbcManager = new DbcManager(
			//	ConnectionInfo.Hostname,
			//	ConnectionInfo.Credential.UserName,
			//	ConnectionInfo.Credential.Password
			//);
		}

		protected override void ProcessRecord()
		{
			_dbcManager.LoadDbcIntoWorldDatabase(DbcPath);
		}
	}
}
