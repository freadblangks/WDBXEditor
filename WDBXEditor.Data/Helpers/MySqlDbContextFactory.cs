using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using WDBXEditor.Data.Contexts;
using WDBXEditor.Data.Helpers.Interfaces;

namespace WDBXEditor.Data.Helpers
{
	/// <summary>
	/// Factory class for creating instances of <see cref="MySqlContext"/>.
	/// </summary>
	public class MySqlDbContextFactory : IDbContextFactory
	{
		public IDbContext GetContext(string hostname, string username, SecureString password, string database)
		{
			string connectionString = BuildConnectionStringWithSecuredPassword(hostname, username, password, database);
			return new MySqlContext(connectionString);
		}

		// It would be a good idea to not convert the password to plaintext,
		// but we'd need to come up with a reasonable cross-platform solution.
		private string BuildConnectionStringWithSecuredPassword(string hostname, string username, SecureString password, string database)
		{
			var valuePtr = Marshal.SecureStringToBSTR(password);
			string unsecuredPassword = Marshal.PtrToStringUni(valuePtr);
			string connectionString = $"Server={hostname};Uid={username};Pwd={unsecuredPassword};";

			if (!string.IsNullOrWhiteSpace(database))
			{
				connectionString += $"Database={database};";
			}

			return connectionString;
		}
	}
}
