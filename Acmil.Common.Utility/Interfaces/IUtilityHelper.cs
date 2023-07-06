using Acmil.Common.Utility.Configuration.Interfaces;
using Acmil.Common.Utility.Logging.Interfaces;

namespace Acmil.Common.Utility.Interfaces
{
	/// <summary>
	/// Helper for getting instances of commonly used utility classes, like implementations of <see cref="ILogger"/> and <see cref="IConfigurationManager"/>.
	/// </summary>
	public interface IUtilityHelper
	{
		/// <summary>
		/// An implementation of <see cref="IConfigurationManager"/>.
		/// </summary>
		public IConfigurationManager ConfigurationManager { get; }

		/// <summary>
		/// An implementation of <see cref="ILogger"/>.
		/// </summary>
		public ILogger Logger { get; }
	}
}
