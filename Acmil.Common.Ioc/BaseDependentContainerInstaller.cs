using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Acmil.Common.Ioc
{
	/// <summary>
	/// Base class for container installer classes that depend on other container installer classes.
	/// </summary>
	public abstract class BaseDependentContainerInstaller
	{
		private ServiceCollection _services;

		/// <summary>
		/// Gets a <see cref="ServiceCollection"/> containing service registrations
		/// for the project and its dependencies.
		/// </summary>
		/// <returns>
		/// A <see cref="ServiceCollection"/> containing service registrations
		/// for the project and its dependencies.
		/// </returns>
		public ServiceCollection GetServices()
		{
			if (_services is null)
			{
				_services = new ServiceCollection
				{
					GetDependencyRegistrations(),
					GetProjectRegistrations()
				};
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

		/// <summary>
		/// Gets a <see cref="ServiceCollection"/> of registered services whose
		/// implementations are defined in projects or assemblies that this
		/// project depends on.
		/// </summary>
		/// <returns>
		/// A <see cref="ServiceCollection"/> of registered services whose
		/// implementations are defined in projects or assemblies that this
		/// project depends on.
		/// </returns>
		protected abstract ServiceCollection GetDependencyRegistrations();
	}
}