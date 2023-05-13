using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading.Tasks;

namespace Acmil.PowerShell.Cmdlets
{
	[Cmdlet(VerbsData.Initialize, "Acmil")]
	[OutputType(typeof(PSCredential))]
	public class InitializeACMILCmdlet : PSCmdlet
	{
		protected override void ProcessRecord()
		{
			var credential = PromptForMySqlCredential();
			base.WriteObject(credential);
			//var credentialPromptCommand = new Command("Get-Credential");
		}

		private PSCredential PromptForMySqlCredential()
		{
			var credential = base.Host.UI.PromptForCredential(
				"Initializing AzerothCore Mod Installation Library (ACMIL)...",
				"Please enter connection info for your MySQL database.",
				null,
				"MySqlCredentials"
			);

			return credential;
		}
	}
}
