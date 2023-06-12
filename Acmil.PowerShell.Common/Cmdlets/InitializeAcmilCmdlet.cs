using Acmil.Data.Contracts.Connections;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Host;

namespace Acmil.PowerShell.Common.Cmdlets
{
	[Cmdlet(VerbsData.Initialize, "Acmil")]
	[OutputType(typeof(MySqlConnectionInfo))]
	public class InitializeACMILCmdlet : BaseCmdlet
	{
		protected override void ProcessRecord()
		{
			var serverHostname = PromptForMySqlHostname();
			var credential = PromptForMySqlCredential();
			WriteObject(new MySqlConnectionInfo() { Hostname = serverHostname, Credential = credential });
		}

		private string PromptForMySqlHostname()
		{
			var promptResults = Host.UI.Prompt("Initializing AzerothCore Mod Installation Library (ACMIL)...",
				"Please enter the hostname of your MySQL database",
				new Collection<FieldDescription>
				{
					new FieldDescription("Server")
				}
			);

			return promptResults["Server"].ToString();
		}

		private Credential PromptForMySqlCredential()
		{
			var psCredential = Host.UI.PromptForCredential(
				"Initializing AzerothCore Mod Installation Library (ACMIL)...",
				/*"Please enter connection info for your MySQL database."*/"Please enter your MySQL credentials.",
				null,
				""
			);

			return new Credential() { UserName = psCredential.UserName, Password = psCredential.Password };
		}
	}
}