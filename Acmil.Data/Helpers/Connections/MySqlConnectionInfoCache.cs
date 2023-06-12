using Acmil.Data.Helpers.Connections.Dtos;
using Acmil.Data.Helpers.Connections.Interfaces;
using System;
using System.Collections.Concurrent;

namespace Acmil.Data.Helpers.Connections
{
	/// <summary>
	/// Cache for <see cref="MySqlConnectionInfoInternal"/> instances.
	/// </summary>
	internal class MySqlConnectionInfoCache : IMySqlConnectionInfoCache
	{
		private readonly ConcurrentDictionary<string, MySqlConnectionInfoInternal> _connectionStringCache = new ConcurrentDictionary<string, MySqlConnectionInfoInternal>();

		public MySqlConnectionInfoInternal GetOrAdd(string key, Func<MySqlConnectionInfoInternal> factory)
		{
			if (factory is null)
			{
				throw new ArgumentNullException(nameof(factory));
			}

			return _connectionStringCache.GetOrAdd(key, x => factory());
		}

		public void OnConnectionStringUpdated()
		{
			_connectionStringCache.Clear();
		}
	}
}
