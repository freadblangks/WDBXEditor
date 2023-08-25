using Acmil.Api.Managers.Interfaces;
using Acmil.Data.Contracts.Connections;
using Acmil.PowerShell.Common.Ioc;
using System.Management.Automation;

namespace Acmil.PowerShell.Common.Cmdlets
{
	[Cmdlet(VerbsData.Import, "DbcIntoDatabase")]
	public class ImportDbcIntoDatabaseCmdlet : PSCmdlet
	{
		IDbcManager _dbcManager;

		private RootContainerInstaller _rootContainer;

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
			_rootContainer = new RootContainerInstaller();
			_dbcManager = _rootContainer.Resolve<IDbcManager>();
		}

		protected override void BeginProcessing()
		{

		}

		protected override void ProcessRecord()
		{
			_dbcManager.LoadDbcIntoDatabase(ConnectionInfo, DatabaseName, DbcPath, TableName);
		}
	}
}
