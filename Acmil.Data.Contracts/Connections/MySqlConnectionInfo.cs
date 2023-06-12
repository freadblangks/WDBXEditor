namespace Acmil.Data.Contracts.Connections
{
	/// <summary>
	/// Class for storing information needed to connect to a MySQL database.
	/// </summary>
	public class MySqlConnectionInfo
	{
		/// <summary>
		/// The hostname of the server.
		/// </summary>
		/// <remarks>Can include port.</remarks>
		public string Hostname { get; set; } = "";

		/// <summary>
		/// A credential containing the username and password for login.
		/// </summary>
		public Credential? Credential { get; set; } = null;
	}
}
