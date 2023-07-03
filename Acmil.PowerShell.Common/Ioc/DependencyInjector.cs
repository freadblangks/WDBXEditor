using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Acmil.PowerShell.Common.Helpers.Interfaces;
using Acmil.PowerShell.Common.Helpers;

namespace Acmil.PowerShell.Common.Ioc
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
			var services = new ServiceCollection() { GetDependencyServices() };

			// SERVICE DEFINITIONS //
			services.AddTransient<ICmdletHelper, CmdletHelper>();

			return services;
		}

		private static ServiceCollection GetDependencyServices()
		{
			var internalApiInjector = new Api.Ioc.DependencyInjector();
			return internalApiInjector.GetServices();
		}
	}
}
