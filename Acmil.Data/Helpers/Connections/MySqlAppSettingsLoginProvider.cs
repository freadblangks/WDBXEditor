using Acmil.Data.Helpers.Connections.Interfaces;
using System;
using System.Configuration;

namespace Acmil.Data.Helpers.Connections
{
	/// <summary>
	/// Class for reading login info from this project's app.config.
	/// </summary>
	internal class MySqlAppSettingsLoginProvider : IMySqlLoginProvider
	{
		private const string _USE_APP_SETTINGS_FOR_LOGIN_KEY = "UseAppSettingsForLogin";
		private const string _MY_SQL_USER_ID_KEY = "MySqlUserId";
		private const string _MY_SQL_PASSWORD = "MySqlPassword";

		private readonly Lazy<Tuple<string, string>> _login;

		/// <summary>
		/// Initializes a new instance of <see cref="MySqlAppSettingsLoginProvider"/> using the values in this project's app.config.
		/// The "UseAppSettingsForLogin" setting must be set for this to work.
		/// </summary>
		public MySqlAppSettingsLoginProvider()
		{
			_login = new Lazy<Tuple<string, string>>(() => GetAppSettingsLogin());
		}

		public string Username => throw new NotImplementedException();

		public string GetLoginUserId()
		{
			return _login.Value.Item1;
		}

		public string GetLoginPassword()
		{
			return _login.Value.Item2;
		}

		private Tuple<string, string> GetAppSettingsLogin()
		{
			Tuple<string, string> loginTuple = null;
			string useAppSettingsForLogin = ConfigurationManager.AppSettings.Get(_USE_APP_SETTINGS_FOR_LOGIN_KEY);
			if (useAppSettingsForLogin != null && useAppSettingsForLogin.Equals("true", StringComparison.OrdinalIgnoreCase))
			{
				string username = ConfigurationManager.AppSettings.Get(_MY_SQL_USER_ID_KEY) ?? throw new ArgumentNullException(_MY_SQL_USER_ID_KEY);
				string password = ConfigurationManager.AppSettings.Get(_MY_SQL_PASSWORD) ?? throw new ArgumentNullException(_MY_SQL_PASSWORD);

				loginTuple = new Tuple<string, string>(username, password);
			}

			return loginTuple;
		}
	}
}
