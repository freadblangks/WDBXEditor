using Acmil.Common.Utility.Configuration.Interfaces;
using Acmil.Common.Utility.Localization.Interfaces;
using Acmil.Common.Utility.Logging.Interfaces;

namespace Acmil.Common.Utility.Interfaces
{
	/// <summary>
	/// Helper for getting instances of commonly used utility classes, like implementations of <see cref="ILogger"/> and <see cref="IConfigurationManager"/>.
	/// </summary>
	public interface IUtilityHelper
	{
		/// <summary>
		/// An implementation of <see cref="IConfigurationManager"/> for accessing configured values.
		/// </summary>
		public IConfigurationManager ConfigurationManager { get; }

		/// <summary>
		/// An implementation of <see cref="ILogger"/> for logging.
		/// </summary>
		public ILogger Logger { get; }


		/// <summary>
		/// An implementation of <see cref="ILocalizationHelper"/> for localization.
		/// </summary>
		public ILocalizationHelper LocalizationHelper { get; }
	}
}
