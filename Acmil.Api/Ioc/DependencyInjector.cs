using Acmil.Api.Managers;
using Acmil.Api.Managers.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Acmil.Api.Ioc
{
	public class DependencyInjector
	{
		//private static IServiceProvider _serviceProvider;
		private ServiceCollection _services;

		//public T Resolve<T>() where T : class
		//{
		//	return _serviceProvider.GetService<T>();
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
			services.AddTransient<IItemTemplateManager, ItemTemplateManager>();
			services.AddTransient<IDbcManager, DbcManager>();

			return services;
		}

		private static ServiceCollection GetDependencyServices()
		{
			var servicesInjector = new Data.Services.Ioc.DependencyInjector();
			return servicesInjector.GetServices();
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

		//internal static void ConfigureServices()
		//{
		//	if (_serviceProvider is null)
		//	{
		//		_serviceProvider = BuildServiceProvider();
		//	}
		//}

		//private static IServiceProvider BuildContainer()
		//{

		//	//builder.RegisterType<ItemTemplateService>().As<IItemTemplateService>();
		//	//builder.RegisterType<IDbcManager>().As<DbcManager>();

			
		//	//container.Register<IItemTemplateManager, ItemTemplateManager>(Lifestyle.Transient);
		//	//container.Register<IDbcManager, DbcManager>(Lifestyle.Transient);
		//	//services.AddTransient<IItemTemplateManager, ItemTemplateManager>();
		//	//services.AddTransient<IDbcManager, DbcManager>();

		//	return services.BuildServiceProvider();
		//}
	}
}
