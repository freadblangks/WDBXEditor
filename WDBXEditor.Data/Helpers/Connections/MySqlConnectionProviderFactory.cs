using System;
using WDBXEditor.Data.Helpers.Connections.Dtos;
using WDBXEditor.Data.Helpers.Connections.Interfaces;

namespace WDBXEditor.Data.Helpers.Connections
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
			MySqlConnectionInfo mySqlconnectionInfo = _mySqlConnectionInfoFactory.GetConnectionInfo(connectionString);
			return new MySqlConnectionProvider(mySqlconnectionInfo, _toggle());
		}

		//public IMySqlConnectionProvider GetMySqlConnectionProvider(MySqlConnectionOverrides overrides)
		//{
		//	MySqlConnectionInfo mySqlConnectionInfo = _mySqlConnectionInfoFactory.GetConnectionInfo(overrides);
		//	return new MySqlConnectionProvider(mySqlConnectionInfo, _toggle());
		//}
	}
}
