using Acmil.Common.Ioc;
using Microsoft.Extensions.DependencyInjection;

namespace Acmil.PowerShell.Common.Ioc
{
	internal sealed class RootContainerInstaller : BaseRootContainerInstaller
	{
		protected override ServiceProvider BuildServiceProvider()
		{
			var powershellCommonInstaller = new ContainerInstaller();
			var services = powershellCommonInstaller.GetServices();
			return services.BuildServiceProvider(true);
		}
	}
}
