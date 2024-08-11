using Microsoft.Extensions.DependencyInjection;

namespace Acmil.Common.Ioc
{
	/// <summary>
	/// Base class for container installer classes at the top of the dependency hierarchy.
	/// </summary>
	public abstract class BaseRootContainerInstaller
	{
		private IServiceProvider _serviceProvider;

		public BaseRootContainerInstaller()
		{
			_serviceProvider = BuildServiceProvider();
		}

		/// <summary>
		/// Gets an instance of the registered implementation for an interface <typeparamref name="T"/>.
		/// </summary>
		/// <typeparam name="T">The service interface whose implementation should be resolved.</typeparam>
		/// <returns>An instance of the registered implementation for the interface <typeparamref name="T"/>.</returns>
		public T Resolve<T>() where T : class
		{
			return _serviceProvider.GetService<T>();
		}

		/// <summary>
		/// Builds and returns an instance of <see cref="ServiceProvider"/> using the registrations
		/// of container installers all down the dependency tree.
		/// </summary>
		/// <returns>
		/// An instance of <see cref="ServiceProvider"/> using the registrations
		/// of container installers all down the dependency tree.
		/// </returns>
		protected abstract ServiceProvider BuildServiceProvider();
	}
}
