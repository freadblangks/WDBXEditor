using WDBXEditor.Data.Helpers.Connections.Dtos;

namespace WDBXEditor.Data.Helpers.Connections.Interfaces
{
	/// <summary>
	/// Factory for getting and creating <see cref="MySqlConnectionInfo"/> instances.
	/// </summary>
	public interface IMySqlConnectionInfoFactory
	{
		/// <summary>
		/// Gets a fully populated instance of <see cref="MySqlConnectionInfo"/> for the connection string.
		/// </summary>
		/// <param name="connectionString">The connection string.</param>
		/// <returns>A fully populated instance of <see cref="MySqlConnectionInfo"/> based on the connection string.</returns>
		MySqlConnectionInfo GetConnectionInfo(string connectionString);


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
