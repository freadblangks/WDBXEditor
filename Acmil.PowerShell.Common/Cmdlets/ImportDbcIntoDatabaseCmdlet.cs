using Acmil.Api.Managers.Interfaces;
using Acmil.Common.Utility.Connections;
using System.Management.Automation;

namespace Acmil.PowerShell.Common.Cmdlets
{
	[Cmdlet(VerbsData.Import, "DbcIntoDatabase")]
	public class ImportDbcIntoDatabaseCmdlet : BaseCmdlet
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

		public ImportDbcIntoDatabaseCmdlet()
		{
			_dbcManager = RootContainer.Resolve<IDbcManager>();
		}

		protected override void BeginProcessing()
		{
			base.BeginProcessing();
		}

		protected override void ProcessRecord()
		{
			_dbcManager.LoadDbcIntoDatabase(ConnectionInfo, DatabaseName, DbcPath, TableName);
		}
	}
}
