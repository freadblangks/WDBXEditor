using Acmil.Data.Helpers;
using Acmil.Data.Helpers.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Acmil.Data.Ioc
{
	public class DependencyInjector
	{
		private ServiceCollection _services;

		public ServiceCollection GetServices()
		{
			_services ??= GetRegisteredServices();
			return _services;
		}

		private static ServiceCollection GetRegisteredServices()
		{
			//var services = new ServiceCollection() { GetDependencyServices() };
			var services = new ServiceCollection();

			// SERVICE DEFINITIONS //
			services.AddTransient<IDbContextFactory, MySqlDbContextFactory>();

			return services;
		}

		//private static ServiceCollection GetDependencyServices()
		//{
		//	var servicesInjector = new Data.Services.Ioc.DependencyInjector();
		//	return servicesInjector.GetServices();
		//}
	}
}
