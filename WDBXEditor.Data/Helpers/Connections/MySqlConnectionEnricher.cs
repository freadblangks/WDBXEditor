using MySql.Data.MySqlClient;
using System;
using System.Collections.Concurrent;
using WDBXEditor.Data.Helpers.Connections.Interfaces;

namespace WDBXEditor.Data.Helpers.Connections
{
	/// <summary>
	/// Class used for inserting values into MySQL connection strings.
	/// </summary>
	internal class MySqlConnectionEnricher : IMySqlConnectionEnricher
	{
		private static Lazy<MySqlConnectionEnricher> _instance = new Lazy<MySqlConnectionEnricher>(() => new MySqlConnectionEnricher());
		private ConcurrentDictionary<string, IMySqlLoginProvider> _loginLookup;

		/// <summary>
		/// Initializes a new instance of <see cref="MySqlConnectionEnricher"/>.
		/// </summary>
		internal MySqlConnectionEnricher()
		{
			_loginLookup = new ConcurrentDictionary<string, IMySqlLoginProvider>();
		}

		/// <summary>
		/// Gets the current instance of <see cref="MySqlconnectionEnricher"/>.
		/// </summary>
		public static IMySqlConnectionEnricher Instance => _instance.Value;

		public void EnsureConnectionString(MySqlConnectionStringBuilder connectionStringBuilder, MySqlConnectionOverrides overrides)
		{
			ApplyOverrides(connectionStringBuilder, overrides);
			EnsureConnectionStringContainsAdvancedValues(connectionStringBuilder);
		}

		public void RegisterLogin(IMySqlLoginProvider loginProvider)
		{
			_loginLookup.TryAdd(loginProvider.Username, loginProvider);
		}

		/// <summary>
		/// Ensures that the connection string is correctly populated with any advanced settings.
		/// </summary>
		/// <param name="connectionBuilder">The <see cref="MySqlConnectionStringBuilder"/> instance to ensure.</param>
		internal void EnsureConnectionStringContainsAdvancedValues(MySqlConnectionStringBuilder connectionBuilder)
		{
			if (!string.IsNullOrEmpty(connectionBuilder.ConnectionString))
			{
				if (connectionBuilder.PersistSecurityInfo)
				{
					connectionBuilder.PersistSecurityInfo = false;
				}
			}
		}

		private void ApplyOverrides(MySqlConnectionStringBuilder connectionStringBuilder, MySqlConnectionOverrides overrides)
		{

			if (overrides != null)
			{
				if (!string.IsNullOrEmpty(overrides.DatabaseOverride))
				{
					connectionStringBuilder.Database = overrides.DatabaseOverride;
				}

				if (!string.IsNullOrEmpty(overrides.ServerOverride))
				{
					connectionStringBuilder.Server = overrides.ServerOverride;
				}

				if (!string.IsNullOrEmpty(overrides.UserOverride))
				{
					string userNameOverride = overrides.UserOverride;
					if (_loginLookup.TryGetValue(overrides.UserOverride, out IMySqlLoginProvider loginProvider))
					{
						string updatedUserName = loginProvider?.GetLoginUserId();
						string password = loginProvider?.GetLoginPassword();
						if (!string.IsNullOrEmpty(password))
						{
							connectionStringBuilder.Password = password;
						}

						if (!string.IsNullOrEmpty(updatedUserName))
						{
							userNameOverride = updatedUserName;
						}
					}

					connectionStringBuilder.UserID = userNameOverride;
				}
			}
		}
	}
}
