using System.Management.Automation;

namespace Acmil.PowerShell.Common.Helpers.Interfaces
{
	/// <summary>
	/// Interface defining a helper class for interacting with PowerShell cmdlets.
	/// </summary>
	internal interface ICmdletHelper
	{
		/// <summary>
		/// Throws an error to inform the user that the attempted to call a cmdlet with an unsupported parameter set.
		/// </summary>
		/// <param name="cmdlet">The cmdlet that was called improperly.</param>
		/// <exception cref="ArgumentException">Thrown when this method is called.</exception>
		internal void HandleUnsupportedParameterSet(PSCmdlet cmdlet);
	}
}
