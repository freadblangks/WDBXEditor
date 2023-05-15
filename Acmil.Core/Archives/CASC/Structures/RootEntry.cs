using Acmil.Core.Archives.CASC.Constants;

namespace Acmil.Core.Archives.CASC.Structures
{
	public struct RootEntry
	{
		public byte[] MD5 { get; set; }
		public ulong Hash { get; set; }
		public Locales Locales { get; set; }
	}
}
