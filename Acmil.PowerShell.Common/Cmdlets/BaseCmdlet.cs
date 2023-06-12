using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Acmil.PowerShell.Common.Cmdlets
{
	/// <summary>
	/// Base class for PowerShell cmdlets in our library.
	/// </summary>
	public abstract class BaseCmdlet : PSCmdlet
	{
		/// <summary>
		/// Initializes an extension of <see cref="BaseCmdlet"/>.
		/// </summary>
		public BaseCmdlet() : base()
		{

		}
	}
}
