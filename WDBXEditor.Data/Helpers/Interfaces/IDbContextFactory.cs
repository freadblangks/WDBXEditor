using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using WDBXEditor.Data.Contexts;

namespace WDBXEditor.Data.Helpers.Interfaces
{
	/// <summary>
	/// Interface defining a factory class for creating implementations of <see cref="IDbContext"/>.
	/// </summary>
	public interface IDbContextFactory
	{
		/// <summary>
		/// Gets an implementation of <see cref="IDbContext"/>.
		/// </summary>
		/// <param name="hostname">The server's hostname.</param>
		/// <param name="username">The name of the user to log in as.</param>
		/// <param name="password">The password to log in with.</param>
		/// <param name="database">The name of the database to connect to.</param>
		/// <returns>An implementation of <see cref="IDbContext"/>.</returns>
		public IDbContext GetContext(string hostname, string username, SecureString password, string database);
	}
}
