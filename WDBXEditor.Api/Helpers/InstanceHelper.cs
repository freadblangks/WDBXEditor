using System;

namespace WDBXEditor.Extended.Api.Helpers
{
	public class InstanceHelper
	{
		public static void InstanceCheck()
		{
			InstanceManager.InstanceCheck(new string[] { "-console" });
		}

		public static void LoadStormLib()
		{
			InstanceManager.LoadDll("Stormlib.dll");
		}

		public static void Stop()
		{
			InstanceManager.Stop();
		}
	}
}
