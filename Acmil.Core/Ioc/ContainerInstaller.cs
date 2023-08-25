using Acmil.Common.Ioc;
using Acmil.Core.Contexts;
using Acmil.Core.Contexts.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Acmil.Core.Ioc
{
	public class ContainerInstaller : BaseDependentContainerInstaller
	{
		protected override ServiceCollection GetProjectRegistrations()
		{
			var services = new ServiceCollection() { GetDependencyRegistrations() };

			// SERVICE DEFINITIONS //
			services.AddTransient<IDbcContext, DbcContext>();

			return services;
		}

		protected override ServiceCollection GetDependencyRegistrations()
		{
			var dataInstaller = new Data.Ioc.ContainerInstaller();
			return dataInstaller.GetServices();
		}
	}
}
