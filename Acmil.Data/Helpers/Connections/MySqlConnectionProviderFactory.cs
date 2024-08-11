using Acmil.Data.Helpers.Connections.Dtos;
using Acmil.Data.Helpers.Connections.Interfaces;
using System;

namespace Acmil.Data.Helpers.Connections
{
	/// <summary>
	/// Factory class for obtaining implementations of <see cref="IMySqlConnectionProvider"/>.
	/// </summary>
	internal class MySqlConnectionProviderFactory : IMySqlConnectionProviderFactory
	{
		private static Lazy<MySqlConnectionProviderFactory> _instance = new Lazy<MySqlConnectionProviderFactory>(() => new MySqlConnectionProviderFactory(MySqlConnectionInfoFactory.Instance));
		private static Func<bool> _toggle = () => false;
		public static IMySqlConnectionProviderFactory Instance => _instance.Value;

		private IMySqlConnectionInfoFactory _mySqlConnectionInfoFactory;

		/// <summary>
		/// Initializes a new instance of <see cref="MySqlConnectionProviderFactory"/>.
		/// </summary>
		/// <param name="connectionInfoFactory">An implementation of <see cref="IMySqlConnectionInfoFactory"/>.</param>
		protected MySqlConnectionProviderFactory(IMySqlConnectionInfoFactory connectionInfoFactory)
		{
			_mySqlConnectionInfoFactory = connectionInfoFactory;
		}

		public IMySqlConnectionProvider GetMySqlConnectionProvider(string connectionString)
		{
			MySqlConnectionInfoInternal mySqlconnectionInfo = _mySqlConnectionInfoFactory.GetConnectionInfo(connectionString);
			return new MySqlConnectionProvider(mySqlconnectionInfo, _toggle());
		}

		//public IMySqlConnectionProvider GetMySqlConnectionProvider(MySqlConnectionOverrides overrides)
		//{
		//	MySqlConnectionInfo mySqlConnectionInfo = _mySqlConnectionInfoFactory.GetConnectionInfo(overrides);
		//	return new MySqlConnectionProvider(mySqlConnectionInfo, _toggle());
		//}
	}
}
