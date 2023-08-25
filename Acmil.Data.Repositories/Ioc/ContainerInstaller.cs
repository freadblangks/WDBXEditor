using Acmil.Common.Ioc;
using Acmil.Data.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Acmil.Data.Repositories.Ioc
{
	public sealed class ContainerInstaller : BaseDependentContainerInstaller
	{
		protected override ServiceCollection GetProjectRegistrations()
		{
			var services = new ServiceCollection() { GetDependencyRegistrations() };

			// SERVICE DEFINITIONS //
			services.AddTransient<IDbcRepository, DbcRepository>();
			services.AddTransient<IItemTemplateRepository, ItemTemplateRepository>();
			services.AddTransient<IDbcRepository, DbcRepository>();

			return services;
		}

		protected override ServiceCollection GetDependencyRegistrations()
		{
			var coreInstaller = new Core.Ioc.ContainerInstaller();
			return coreInstaller.GetServices();
		}
	}
}
