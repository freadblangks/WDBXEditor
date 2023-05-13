using System;

namespace WDBXEditor.Core.Reader.Enums
{
	[Flags]
	public enum HeaderFlags : short
	{
		None = 0x0,
		OffsetMap = 0x1,
		RelationshipData = 0x2,
		IndexMap = 0x4,
		Unknown = 0x8,
		Compressed = 0x10,
	}
}
