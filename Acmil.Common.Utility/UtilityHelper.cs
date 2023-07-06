using Acmil.Common.Utility.Configuration.Interfaces;
using Acmil.Common.Utility.Interfaces;
using Acmil.Common.Utility.Logging.Interfaces;

namespace Acmil.Common.Utility
{
	/// <summary>
	/// Helper for getting instances of commonly used utility classes, like implementations of <see cref="ILogger"/> and <see cref="IConfigurationManager"/>.
	/// </summary>
	public class UtilityHelper : IUtilityHelper
	{
		public IConfigurationManager ConfigurationManager { get; private set; }
		public ILogger Logger { get; private set; }

		public UtilityHelper(IConfigurationManager configManager, ILogger logger)
		{
			ConfigurationManager = configManager;
			Logger = logger;
		}
	}
}
