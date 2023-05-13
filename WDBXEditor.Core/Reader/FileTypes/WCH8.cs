using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WDBXEditor.Core.Storage;

namespace WDBXEditor.Core.Reader.FileTypes
{
	class WCH8 : WCH7
	{
		public WCH8()
		{
			StringTableOffset = 0x14;
			HeaderSize = 0x34;
		}

		public WCH8(string filename)
		{
			StringTableOffset = 0x14;
			HeaderSize = 0x34;
			FileName = filename;
		}

		public override void WriteRecordPadding(BinaryWriter writer, DBEntry entry, long offset)
		{
			if (!HasOffsetTable && writer.BaseStream.Position - offset < RecordSize)
			{
				writer.BaseStream.Position += RecordSize - (writer.BaseStream.Position - offset);
			}
		}
	}
}
