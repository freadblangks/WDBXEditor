using Microsoft.Extensions.DependencyInjection;

namespace Acmil.PowerShell.Common.Ioc
{
	internal class RootDependencyInjector
	{
		private IServiceProvider _serviceProvider;

		public RootDependencyInjector()
		{
			_serviceProvider = BuildServiceProvider();
		}

		public T Resolve<T>() where T : class
		{
			return _serviceProvider.GetService<T>();
		}

		private ServiceProvider BuildServiceProvider()
		{
			var injector = new DependencyInjector();
			var services = injector.GetServices();
			return services.BuildServiceProvider(true);
		}

		//private static ServiceCollection GetRegisteredServices()
		//{
		//	var services = new ServiceCollection() { GetDependencyServices() };
		//	return services;
		//}
		
		//private static ServiceCollection GetDependencyServices()
		//{
		//	var internalApiInjector = new Api.Ioc.DependencyInjector();
		//	return internalApiInjector.GetServices();
		//}
	}
}
