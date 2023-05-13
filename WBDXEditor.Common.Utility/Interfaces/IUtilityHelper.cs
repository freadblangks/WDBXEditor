using WDBXEditor.Common.Utility.Configuration.Interfaces;
using WDBXEditor.Common.Utility.Logging.Interfaces;

namespace WDBXEditor.Common.Utility.Interfaces
{
	/// <summary>
	/// Helper for getting instances of commonly used utility classes, like implementations of <see cref="ILogger"/> and <see cref="IConfigurationManager"/>.
	/// </summary>
	public interface IUtilityHelper
	{
		/// <summary>
		/// Gets an implementation of <see cref="IConfigurationManager"/>.
		/// </summary>
		/// <returns>An implementation of <see cref="IConfigurationManager"/>.</returns>
		IConfigurationManager GetConfigurationManager();

		/// <summary>
		/// Gets an implementation of <see cref="ILogger"/>.
		/// </summary>
		/// <returns>An implementation of <see cref="ILogger"/>.</returns>
		ILogger GetLogger();
	}
}
