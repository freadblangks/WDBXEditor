using System;
using WDBXEditor.Data.Helpers.Connections.Dtos;

namespace WDBXEditor.Data.Helpers.Connections.Interfaces
{
	/// <summary>
	/// Cache for <see cref="MySqlConnectionInfo"/> instances.
	/// </summary>
	internal interface IMySqlConnectionInfoCache
	{
		/// <summary>
		/// Adds a key/value pair to the <see cref="IMySqlConnectionInfoCache"/> instance
		/// by using the specified function if the key does not already exist. Returns the
		/// new value, or the existing value if the key exists.
		/// </summary>
		/// <param name="key">The key of the element to add or get.</param>
		/// <param name="factory">The function used to generate a value of the key if it doesn't already exist.</param>
		/// <returns>
		/// The value for the key. This will be either the existing value for the key if
		/// the key is already in the cache, or the new value for the key as returned
		/// by <paramref name="factory"/> if the key was not in the cache.
		/// </returns>
		MySqlConnectionInfo GetOrAdd(string key, Func<MySqlConnectionInfo> factory);

		/// <summary>
		/// Method to be called when the connection string is updated.
		/// </summary>
		void OnConnectionStringUpdated();
	}
}
