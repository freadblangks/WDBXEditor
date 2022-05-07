namespace WDBXEditor.Data.Helpers.Connections.Interfaces
{
	/// <summary>
	/// Provider for getting username and password information for non-defaul SQL logins.
	/// </summary>
	public interface IMySqlLoginProvider
	{
		/// <summary>
		/// The username the provider will be used for.
		/// </summary>
		string Username { get; }

		/// <summary>
		/// Provides the SQL connection string UserID parameter based on the provided username.
		/// </summary>
		/// <returns></returns>
		string GetLoginUserId();

		/// <summary>
		/// Provides the SQL connection string Password parameter based on the provided name.
		/// </summary>
		/// <returns></returns>
		string GetLoginPassword();
	}
}
