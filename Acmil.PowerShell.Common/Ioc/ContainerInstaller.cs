using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Acmil.PowerShell.Common.Helpers.Interfaces;
using Acmil.PowerShell.Common.Helpers;
using Acmil.Common.Ioc;

namespace Acmil.PowerShell.Common.Ioc
{
	public sealed class ContainerInstaller : BaseDependentContainerInstaller
	{
		protected override ServiceCollection GetProjectRegistrations()
		{
			var services = new ServiceCollection() { GetDependencyRegistrations() };

			// SERVICE DEFINITIONS //
			services.AddTransient<ICmdletHelper, CmdletHelper>();

			return services;
		}

		protected override ServiceCollection GetDependencyRegistrations()
		{
			var internalApiInstaller = new Api.Ioc.ContainerInstaller();
			return internalApiInstaller.GetServices();
		}
	}
}
