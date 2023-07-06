using Acmil.Common.Ioc;
using Acmil.Common.Utility.Configuration;
using Acmil.Common.Utility.Configuration.Interfaces;
using Acmil.Common.Utility.Interfaces;
using Acmil.Common.Utility.Logging;
using Acmil.Common.Utility.Logging.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Acmil.Common.Utility.Ioc
{
	public sealed class ContainerInstaller : BaseIndependentContainerInstaller
	{
		protected override ServiceCollection GetProjectRegistrations()
		{
			var services = new ServiceCollection();

			services.AddTransient<ILogger, Logger>();
			services.AddSingleton<IConfigurationManager, ConfigurationManager>();
			services.AddTransient<IUtilityHelper, UtilityHelper>();

			return services;
		}
	}
}
