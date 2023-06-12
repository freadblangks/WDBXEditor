using Acmil.Api.Managers;
using Acmil.Api.Managers.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Acmil.PowerShell.Engines.Ioc
{
	internal static class DependencyInjector
	{
		private static IServiceProvider _serviceProvider;

		public static IServiceProvider ServiceProvider { 
			get
			{
				if (_serviceProvider is null)
				{
					_serviceProvider = BuildServiceProvider();
				}
				return _serviceProvider;
			}
		}

		//internal static void ConfigureServices()
		//{
		//	if (_serviceProvider is null)
		//	{
		//		_serviceProvider = BuildServiceProvider();
		//	}
		//}

		private static IServiceProvider BuildServiceProvider()
		{
			var services = new ServiceCollection();

			// SERVICE DEFINITIONS //

			services.AddTransient<IItemTemplateManager, ItemTemplateManager>();
			services.AddTransient<IDbcManager, DbcManager>();

			return services.BuildServiceProvider();
		}
	}
}
