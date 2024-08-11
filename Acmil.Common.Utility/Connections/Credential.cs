using System.Security;

namespace Acmil.Common.Utility.Connections
{
	/// <summary>
	/// An object representing a credential.
	/// </summary>
	public class Credential
	{
		/// <summary>
		/// The name of the user.
		/// </summary>
		public string UserName { get; set; }

		/// <summary>
		/// The user's password.
		/// </summary>
		public SecureString Password { get; set; }
	}
}
