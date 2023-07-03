using Acmil.Data.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Acmil.Data.Services.Ioc
{
	public class DependencyInjector
	{
		//private static IServiceProvider _serviceProvider;
		private ServiceCollection _services;

		//public static T Resolve<T>() where T : class
		//{
		//	return ServiceProvider.GetService<T>();
		//}

		public ServiceCollection GetServices()
		{
			_services ??= GetRegisteredServices();
			return _services;
		}

		private static ServiceCollection GetRegisteredServices()
		{
			var services = new ServiceCollection { GetDependencyServices() };

			// SERVICE DEFINITIONS //
			services.AddTransient<IItemTemplateService, ItemTemplateService>();
			services.AddTransient<IDbcService, DbcService>();

			return services;
		}

		private static ServiceCollection GetDependencyServices()
		{
			var repositoriesInjector = new Repositories.Ioc.DependencyInjector();
			return repositoriesInjector.GetServices();
		}

		//private static IServiceProvider ServiceProvider
		//{
		//	get
		//	{
		//		if (_serviceProvider is null)
		//		{
		//			_serviceProvider = BuildContainer();
		//		}
		//		return _serviceProvider;
		//	}
		//}

		//private static IServiceProvider BuildContainer()
		//{
		//	var services = new ServiceCollection();
		//	Repositories.Ioc.DependencyInjector.



		//	return services.BuildServiceProvider();
		//}


	}
}
