using Acmil.Common.Utility.Configuration.Interfaces;
using Acmil.Common.Utility.Connections;
using Acmil.PowerShell.Common.Helpers.Interfaces;
using Acmil.PowerShell.Common.Ioc;
using System.Management.Automation;

namespace Acmil.PowerShell.Common.Cmdlets
{
	/// <summary>
	/// Base class for PowerShell cmdlets in our library.
	/// </summary>
	public abstract class BaseCmdlet : PSCmdlet
	{
		internal RootContainerInstaller RootContainer { get; private set; } = new RootContainerInstaller();
		internal ICmdletHelper CmdletHelper { get; private set; }

		private IConfigurationManager _configurationManager;

		/// <summary>
		/// Initializes an extension of <see cref="BaseCmdlet"/>.
		/// </summary>
		public BaseCmdlet() : base()
		{
			CmdletHelper = RootContainer.Resolve<ICmdletHelper>();
			_configurationManager = RootContainer.Resolve<IConfigurationManager>();
		}

		protected override void BeginProcessing()
		{
			base.BeginProcessing();

			var wrappedConnectionInfo = (PSObject)SessionState.PSVariable.GetValue("script:connectionInfo");
			if (wrappedConnectionInfo is null)
			{
				throw new InvalidOperationException("Found no configured connection to MySQL. Ensure that Initialize-Acmil was called before any other ACMIL cmdlets.");
			}

			_configurationManager.SetConnectionInfo((MySqlConnectionInfo)wrappedConnectionInfo.BaseObject);
		}
	}
}
