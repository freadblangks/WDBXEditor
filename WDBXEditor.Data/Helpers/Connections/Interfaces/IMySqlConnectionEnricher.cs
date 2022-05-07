using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace WDBXEditor.Data.Helpers.Connections.Interfaces
{
	/// <summary>
	/// An object used for inserting values into MySQL connection strings.
	/// </summary>
	internal interface IMySqlConnectionEnricher
	{
		/// <summary>
		/// Ensures that a connection string is populated with the provided overrides and any required advanced settings.
		/// </summary>
		/// <param name="connectionStringBuilder">The <see cref="MySqlConnectionStringBuilder"/> instance to ensure.</param>
		/// <param name="overrides">An instance of <see cref="MySqlConnectionOverrides"/> to override values in <paramref name="connectionStringBuilder"/>.</param>
		void EnsureConnectionString(MySqlConnectionStringBuilder connectionStringBuilder, MySqlConnectionOverrides overrides);

		/// <summary>
		/// Registers an implementation of <see cref="IMySqlLoginProvider"/> so that <see cref="EnsureConnectionString(MySqlConnectionStringBuilder, MySqlConnectionOverrides)"/>
		/// can be called with it to override the login info in a MySQL connection string.
		/// </summary>
		/// <param name="loginProvider"></param>
		void RegisterLogin(IMySqlLoginProvider loginProvider);
	}
}
