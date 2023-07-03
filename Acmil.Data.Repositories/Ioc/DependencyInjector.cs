using Acmil.Data.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Acmil.Data.Repositories.Ioc
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
			var services = new ServiceCollection() { GetDependencyServices() };

			// SERVICE DEFINITIONS //
			services.AddTransient<IItemTemplateRepository, ItemTemplateRepository>();
			services.AddTransient<IDbcRepository, DbcRepository>();

			return services;
		}

		private static ServiceCollection GetDependencyServices()
		{
			var dataInjector = new Data.Ioc.DependencyInjector();
			return dataInjector.GetServices();
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

		//	return services.BuildServiceProvider();
		//}
	}
}
