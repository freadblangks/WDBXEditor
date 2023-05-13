using WDBXEditor.Core.Archives.CASC.Constants;

namespace WDBXEditor.Core.Archives.CASC.Structures
{
	public struct RootEntry
	{
		public byte[] MD5 { get; set; }
		public ulong Hash { get; set; }
		public Locales Locales { get; set; }
	}
}
