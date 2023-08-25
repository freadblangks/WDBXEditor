using Acmil.Api.Managers;
using Acmil.Api.Managers.Interfaces;
using Acmil.Common.Ioc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Acmil.Api.Ioc
{
	public sealed class ContainerInstaller : BaseDependentContainerInstaller
	{
		protected override ServiceCollection GetProjectRegistrations()
		{
			var services = new ServiceCollection() { GetDependencyRegistrations() };

			// SERVICE DEFINITIONS //
			services.AddTransient<IDbcManager, DbcManager>();
			services.AddTransient<IItemTemplateManager, ItemTemplateManager>();
			services.AddTransient<IDbcManager, DbcManager>();

			return services;
		}

		protected override ServiceCollection GetDependencyRegistrations()
		{
			var servicesInstaller = new Data.Services.Ioc.ContainerInstaller();
			return servicesInstaller.GetServices();
		}
	}
}
