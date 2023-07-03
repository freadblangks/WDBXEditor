using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Acmil.Common.Ioc
{
	/// <summary>
	/// Base class for container installer classes that do not depend on other container installer classes.
	/// </summary>
	public abstract class BaseIndependentContainerInstaller
	{
		private ServiceCollection _services;

		/// <summary>
		/// Gets a <see cref="ServiceCollection"/> containing service registrations
		/// for the project.
		/// </summary>
		/// <returns>
		/// A <see cref="ServiceCollection"/> containing service registrations
		/// for the project.
		/// </returns>
		public ServiceCollection GetServices()
		{
			if (_services is null)
			{
				_services = new ServiceCollection { GetProjectRegistrations() };
			}

			return _services;
		}

		/// <summary>
		/// Gets a <see cref="ServiceCollection"/> of registered services whose
		/// implementations are defined in the project.
		/// </summary>
		/// <returns>
		/// A <see cref="ServiceCollection"/> of registered services whose implementations
		/// are in the project.
		/// </returns>
		protected abstract ServiceCollection GetProjectRegistrations();
	}
}
