using Acmil.Common.Ioc;
using Acmil.Data.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Acmil.Data.Services.Ioc
{
	public sealed class ContainerInstaller : BaseDependentContainerInstaller
	{
		protected override ServiceCollection GetProjectRegistrations()
		{
			var services = new ServiceCollection { GetDependencyRegistrations() };

			// SERVICE DEFINITIONS //
			services.AddTransient<IItemTemplateService, ItemTemplateService>();
			services.AddTransient<IDbcService, DbcService>();

			return services;
		}

		protected override ServiceCollection GetDependencyRegistrations()
		{
			var repositoriesInjector = new Repositories.Ioc.ContainerInstaller();
			return repositoriesInjector.GetServices();
		}
	}
}
