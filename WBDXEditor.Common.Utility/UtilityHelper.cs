using WDBXEditor.Common.Utility.Configuration;
using WDBXEditor.Common.Utility.Configuration.Interfaces;
using WDBXEditor.Common.Utility.Interfaces;
using WDBXEditor.Common.Utility.Logging;
using WDBXEditor.Common.Utility.Logging.Interfaces;

namespace WDBXEditor.Common.Utility
{
	/// <summary>
	/// Helper for getting instances of commonly used utility classes, like implementations of <see cref="ILogger"/> and <see cref="IConfigurationManager"/>.
	/// </summary>
	public class UtilityHelper : IUtilityHelper
	{
		public IConfigurationManager GetConfigurationManager()
		{
			return ConfigurationManager.Instance;
		}

		public ILogger GetLogger()
		{
			IConfigurationManager configurationManager = GetConfigurationManager();
			return new Logger(configurationManager);
		}
	}
}
