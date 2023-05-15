using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Acmil.PowerShell.Common.OutputTypes
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
		/// A <see cref="PSCredential"/> for storing the username and password for login.
		/// </summary>
		public PSCredential? Credential { get; set; } = null;
	}
}
