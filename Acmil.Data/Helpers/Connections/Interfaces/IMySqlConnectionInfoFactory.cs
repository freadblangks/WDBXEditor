using Acmil.Data.Helpers.Connections;
using Acmil.Data.Helpers.Connections.Dtos;

namespace Acmil.Data.Helpers.Connections.Interfaces
{
	/// <summary>
	/// Factory for getting and creating <see cref="MySqlConnectionInfoInternal"/> instances.
	/// </summary>
	public interface IMySqlConnectionInfoFactory
	{
		/// <summary>
		/// Gets a fully populated instance of <see cref="MySqlConnectionInfoInternal"/> for the connection string.
		/// </summary>
		/// <param name="connectionString">The connection string.</param>
		/// <returns>A fully populated instance of <see cref="MySqlConnectionInfoInternal"/> based on the connection string.</returns>
		MySqlConnectionInfoInternal GetConnectionInfo(string connectionString);


		/// <summary>
		/// Method to be called when the connection string is updated.
		/// </summary>
		void OnConnectionStringUpdated();

		/// <summary>
		/// Registers a login provider with the <see cref="MySqlConnectionEnricher"/>.
		/// </summary>
		/// <param name="loginProvider">The login provider to register.</param>
		void RegisterLogin(IMySqlLoginProvider loginProvider);
	}
}
