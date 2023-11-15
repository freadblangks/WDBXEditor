using Acmil.Common.Utility.Configuration.Interfaces;
using Acmil.Common.Utility.Interfaces;
using Acmil.Common.Utility.Localization.Interfaces;
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
		public ILocalizationHelper LocalizationHelper { get; private set; }

		/// <summary>
		/// Initializes a new instance of <see cref="UtilityHelper"/>
		/// </summary>
		/// <param name="configManager">An implementation of <see cref="IConfigurationManager"/>.</param>
		/// <param name="logger">An implementation of <see cref="ILogger"/>.</param>
		/// <param name="localizationHelper">An implementation of <see cref="ILocalizationHelper"/>.</param>
		public UtilityHelper(IConfigurationManager configManager, ILogger logger, ILocalizationHelper localizationHelper)
		{
			ConfigurationManager = configManager;
			Logger = logger;
			LocalizationHelper = localizationHelper;
		}
	}
}
