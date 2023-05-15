using Acmil.PowerShell.Common.Helpers.Interfaces;
using System.Management.Automation;

namespace Acmil.PowerShell.Common.Helpers
{
	/// <summary>
	/// Helper class for interacting with PowerShell cmdlets.
	/// </summary>
	internal class CmdletHelper : ICmdletHelper
	{
		void ICmdletHelper.HandleUnsupportedParameterSet(PSCmdlet cmdlet)
		{
			throw new ArgumentException($"Cmdlet '{cmdlet.MyInvocation.MyCommand.Name}' was called with an unsupported parameter set. "
				+ $"For information on how to properly call this cmdlet, run 'Get-Help {cmdlet.MyInvocation.MyCommand.Name}'");
		}
	}
}
