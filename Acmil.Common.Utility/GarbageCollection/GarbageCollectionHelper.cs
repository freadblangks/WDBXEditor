using System.Diagnostics;

namespace Acmil.Common.Utility.GarbageCollection
{
	public static class GarbageCollectionHelper
	{
		public static void ForceGC()
		{
			GC.Collect();
			GC.WaitForFullGCComplete();

#if DEBUG
			Debug.WriteLine((GC.GetTotalMemory(false) / 1024 / 1024).ToString() + "mb");
#endif
		}
	}
}
