using Acmil.Api.Managers.Interfaces;
using Acmil.Common.Utility.Connections;
using System.Management.Automation;

namespace Acmil.PowerShell.Common.Cmdlets
{
	[Cmdlet(VerbsCommunications.Write, "DbcDataFromDatabase")]
	public class WriteDbcDataFromDatabaseCmdlet : BaseCmdlet
	{
		IDbcManager _dbcManager;

		[Parameter(Mandatory = true)]
		public MySqlConnectionInfo ConnectionInfo { get; set; }

		[Parameter(Mandatory = true)]
		public string DatabaseName { get; set; }

		[Parameter(Mandatory = true)]
		public string DbcPath { get; set; }

		[Parameter(Mandatory = false)]
		public string TableName { get; set; }

		public WriteDbcDataFromDatabaseCmdlet()
		{
			_dbcManager = RootContainer.Resolve<IDbcManager>();
		}

		protected override void BeginProcessing()
		{
			base.BeginProcessing();
		}

		protected override void ProcessRecord()
		{
			_dbcManager.WriteDbcDataFromDatabase(ConnectionInfo, DatabaseName, DbcPath, TableName);
		}
	}
}
