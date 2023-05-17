﻿namespace Acmil.Common.Utility.Configuration.SettingsModels.MySql
{
	/// <summary>
	/// An object representing the contents of the "MySql" property in appsettings.json.
	/// </summary>
	public class MySqlConfiguration
	{
		/// <summary>
		/// The absolute file path to the directory configured in the MySQL server 
		/// instance's "secure_file_priv" variable. By default, this is required for SQL bulk
		/// load to work in MySQL Server 8.0+.
		/// </summary>
		/// <remarks>
		/// This represents the "MySql.SecureFilePrivDirectoryAbsolutePath" property in appsettings.json.
		/// </remarks>
		public string SecureFilePrivDirectoryAbsolutePath { get; set; }
	}
}