using Acmil.Common.Utility.Configuration;
using Acmil.Common.Utility.Configuration.Interfaces;
using Acmil.Common.Utility.Interfaces;
using Acmil.Common.Utility.Logging.Interfaces;
using Acmil.Common.Utility.Logging;

namespace Acmil.Common.Utility
{
	/// <summary>
	/// Helper for getting instances of commonly used utility classes, like implementations of <see cref="ILogger"/> and <see cref="IConfigurationManager"/>.
	/// </summary>
	public class UtilityHelper : IUtilityHelper
	{
		public IConfigurationManager GetConfigurationManager()
		{
			return new ConfigurationManager();
		}

		public ILogger GetLogger()
		{
			IConfigurationManager configurationManager = GetConfigurationManager();
			return new Logger(configurationManager);
		}
	}
}
