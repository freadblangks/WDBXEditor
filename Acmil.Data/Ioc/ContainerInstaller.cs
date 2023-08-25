using Acmil.Common.Ioc;
using Acmil.Data.Helpers;
using Acmil.Data.Helpers.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Acmil.Data.Ioc
{
	public sealed class ContainerInstaller : BaseDependentContainerInstaller
	{
		protected override ServiceCollection GetProjectRegistrations()
		{
			var services = new ServiceCollection();

			// SERVICE DEFINITIONS //
			services.AddTransient<IDbContextFactory, MySqlDbContextFactory>();

			return services;
		}

		protected override ServiceCollection GetDependencyRegistrations()
		{
			var utilityInstaller = new Common.Utility.Ioc.ContainerInstaller();
			return utilityInstaller.GetServices();
		}
	}
}
