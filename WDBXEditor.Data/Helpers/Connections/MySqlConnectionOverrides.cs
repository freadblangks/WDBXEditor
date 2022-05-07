namespace WDBXEditor.Data.Helpers.Connections
{
	/// <summary>
	/// Class used for creating instances of <see cref="MySqlConnectionInfo"/> using different values than the default.
	/// </summary>
	public class MySqlConnectionOverrides
	{
		/// <summary>
		/// The name of the database to connect to instead of the default.
		/// </summary>
		public readonly string DatabaseOverride;

		/// <summary>
		/// The hostname of the server to connect to instead of the default.
		/// </summary>
		public readonly string ServerOverride;

		/// <summary>
		/// The user to connect as instead of the default.
		/// </summary>
		public readonly string UserOverride;

		/// <summary>
		/// Initializes a new instance of <see cref="MySqlConnectionOverrides"/>.
		/// This provides a way to override values from the main connection string to create new connection strings.
		/// </summary>
		/// <param name="database">The name of the database to connect to instead of the default.</param>
		/// <param name="server">The hostname of the server to connect to instead of the default.</param>
		/// <param name="user">The user to connect as instead of the default.</param>
		public MySqlConnectionOverrides(string database = "", string server = "", string user = "")
		{
			DatabaseOverride = database;
			ServerOverride = server;
			UserOverride = user;
		}

		/// <summary>
		/// Gets a new unpopulated instance of <see cref="MySqlConnectionOverrides"/>.
		/// </summary>
		/// <returns></returns>
		public static MySqlConnectionOverrides Empty() => new MySqlConnectionOverrides();

		/// <inheritdoc/>
		public override string ToString() => $"{DatabaseOverride}:{ServerOverride}:{UserOverride}";
	}
}
