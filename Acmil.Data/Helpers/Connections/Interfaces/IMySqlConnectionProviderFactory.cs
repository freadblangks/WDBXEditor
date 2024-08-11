namespace Acmil.Data.Helpers.Connections.Interfaces
{
	/// <summary>
	/// Factory for obtaining implementations of <see cref="IMySqlConnectionProvider"/>.
	/// </summary>
	internal interface IMySqlConnectionProviderFactory
	{
		/// <summary>
		/// Gets an implementation of <see cref="IMySqlConnectionProvider"/> created using the provided connection string.
		/// </summary>
		/// <param name="connectionString">The connection string.</param>
		/// <returns>An implementation of <see cref="IMySqlConnectionProvider"/> created using the provided connection string.</returns>
		IMySqlConnectionProvider GetMySqlConnectionProvider(string connectionString);

		///// <summary>
		///// Gets an implementation of <see cref="IMySqlConnectionProvider"/> created using the provided connection overrides.
		///// </summary>
		///// <param name="overrides">An instance of <see cref="MySqlConnectionOverrides"/>.</param>
		///// <returns>An implementation of <see cref="IMySqlConnectionProvider"/> created using the provided connection overrides.</returns>
		//IMySqlConnectionProvider GetMySqlConnectionProvider(MySqlConnectionOverrides overrides);
	}
}
