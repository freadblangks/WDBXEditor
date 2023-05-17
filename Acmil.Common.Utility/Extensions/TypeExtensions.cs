using System;
using System.Linq;

namespace Acmil.Common.Utility.Extensions
{
	/// <summary>
	/// Class for <see cref="Type"/> extension methods.
	/// </summary>
	public static class TypeExtensions
	{
		/// <summary>
		/// Returns true if <paramref name="type"/> implements the interface specified by <typeparamref name="T"/>. Otherwise, returns false.
		/// </summary>
		/// <typeparam name="T">The interface type to check for implementation of.</typeparam>
		/// <param name="type">The <see cref="Type"/> instance to check.</param>
		/// <param name="checkTypeArgumentsForInterface">
		/// Whether type arguments should be taken into account when checking for an implementation of a generic interface.
		/// </param>
		/// <returns>True if <paramref name="type"/> implements the interface specified by <typeparamref name="T"/>. Otherwise, false.</returns>
		/// <exception cref="ArgumentException">Thrown when <typeparamref name="T"/> is not an interface.</exception>
		public static bool ImplementsInterface<T>(this Type type, bool checkTypeArgumentsForInterface = false) where T : class
		{
			Type interfaceType = typeof(T);
			if (!interfaceType.IsInterface)
			{
				throw new ArgumentException($"Type parameter {typeof(T)} is not an interface.");
			}

			bool result = false;
			if (interfaceType.IsGenericType)
			{
				if (checkTypeArgumentsForInterface && interfaceType.GetGenericArguments().Length > 0)
				{
					result = type.GetInterfaces().Any(implementedInterface => implementedInterface.IsGenericType && implementedInterface == interfaceType);
				}
				else
				{
					result = type.GetInterfaces().Any(implementedInterface => implementedInterface.IsGenericType && implementedInterface.GetGenericTypeDefinition() == interfaceType.GetGenericTypeDefinition());
				}
			}
			else
			{
				Type[] implementedInterfaces = type.GetInterfaces();
				result = implementedInterfaces.Any(implementedInterface => implementedInterface.FullName == interfaceType.FullName);
			}

			return result;
		}
	}
}
