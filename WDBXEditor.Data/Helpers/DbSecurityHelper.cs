using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using WDBXEditor.Data.Helpers.Interfaces;

namespace WDBXEditor.Data.Helpers
{
	public class DbSecurityHelper : IDbSecurityHelper
	{
		public bool SafelyReadBooleanValue(IDictionary settings, string key, bool defaultValue)
		{
			bool retVal = defaultValue;
			if (settings != null)
			{
				object val;
				try
				{
					val = settings[key];
					if (val != null)
					{
						if (bool.TryParse(val.ToString().Trim(), out bool parsedVal))
						{
							retVal = parsedVal;
						}
					}
				}
				catch
				{
					// Do nothing. The default value will be returned.
				}
			}

			return retVal;
		}

		public int SafelyReadIntValue(IDictionary<string, object> settings, string key, int defaultValue)
		{
			int retVal = defaultValue;
			if (settings != null)
			{
				object val;
				try
				{
					val = settings[key];
					if (val != null)
					{
						if (int.TryParse(val.ToString().Trim(), out int parsedVal))
						{
							retVal = parsedVal;
						}
					}
				}
				catch
				{
					// Do nothing. The default value will be returned.
				}
			}

			return retVal;
		}

		public long SafelyReadLongValue(IDictionary<string, object> settings, string key, long defaultValue)
		{
			long retVal = defaultValue;
			if (settings != null)
			{
				object val;
				try
				{
					val = settings[key];
					if (val != null)
					{
						if (long.TryParse(val.ToString().Trim(), out long parsedVal))
						{
							retVal = parsedVal;
						}
					}
				}
				catch
				{
					// Do nothing. The default value will be returned.
				}
			}

			return retVal;
		}
	}
}
