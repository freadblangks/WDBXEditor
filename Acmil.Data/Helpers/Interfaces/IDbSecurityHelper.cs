using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Acmil.Data.Helpers.Interfaces
{
	/// <summary>
	/// Helper that provides methods for ensuring safe execution of SQL commands.
	/// </summary>
	public interface IDbSecurityHelper
	{
		/// <summary>
		/// Tries to read the Boolean value from the <paramref name="settings"/> Dictionary. If anything goes wrong, <paramref name="defaultValue"/> is returned.
		/// </summary>
		/// <param name="settings">A Dictionary of configuration values.</param>
		/// <param name="key">The name of the configuration value to read.</param>
		/// <param name="defaultValue">The value to be returned if the key is not present or something else goes wrong.</param>
		/// <returns>
		/// If the value was retrieved from the <paramref name="settings"/> Dictionary and parsed correctly, then the parsed value is returned.
		/// Otherwise, <paramref name="defaultValue"/> is returned.
		/// </returns>
		bool SafelyReadBooleanValue(IDictionary settings, string key, bool defaultValue);

		/// <summary>
		/// Tries to read the int value from the <paramref name="settings"/> Dictionary. If anything goes wrong, <paramref name="defaultValue"/> is returned.
		/// </summary>
		/// <param name="settings">A Dictionary of configuration values.</param>
		/// <param name="key">The name of the configuration value to read.</param>
		/// <param name="defaultValue">The value to be returned if the key is not present or something else goes wrong.</param>
		/// <returns>
		/// If the value was retrieved from the <paramref name="settings"/> Dictionary and parsed correctly, then the parsed value is returned.
		/// Otherwise, <paramref name="defaultValue"/> is returned.
		/// </returns>
		int SafelyReadIntValue(IDictionary<string, object> settings, string key, int defaultValue);

		/// <summary>
		/// Tries to read the long value from the <paramref name="settings"/> Dictionary. If anything goes wrong, <paramref name="defaultValue"/> is returned.
		/// </summary>
		/// <param name="settings">A Dictionary of configuration values.</param>
		/// <param name="key">The name of the configuration value to read.</param>
		/// <param name="defaultValue">The value to be returned if the key is not present or something else goes wrong.</param>
		/// If the value was retrieved from the <paramref name="settings"/> Dictionary and parsed correctly, then the parsed value is returned.
		/// Otherwise, <paramref name="defaultValue"/> is returned.
		/// </returns>
		long SafelyReadLongValue(IDictionary<string, object> settings, string key, long defaultValue);
	}
}
