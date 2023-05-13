using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WDBXEditor.Core.Reader.Enums;
using WDBXEditor.Core.Storage;

namespace WDBXEditor.Core.Reader.FileTypes
{
	public class WCH5 : DBHeader
	{
		public uint Build { get; set; }
		public uint TimeStamp { get; set; }
		public override bool ExtendedStringTable => true;

		public string FileName { get; set; }
		public override bool HasOffsetTable => Flags.HasFlag(HeaderFlags.OffsetMap);
		public override bool HasIndexTable => Flags.HasFlag(HeaderFlags.IndexMap);
		public override bool HasRelationshipData => Flags.HasFlag(HeaderFlags.RelationshipData);

		protected WDB5 WDB5CounterPart;
		protected int OffsetMapOffset = 0x30;

		public WCH5()
		{
			HeaderSize = 0x30;
		}

		public WCH5(string filename)
		{
			HeaderSize = 0x30;
			FileName = filename;
		}

		#region Read

		public override void ReadHeader(ref BinaryReader dbReader, string signature)
		{
			string _filename = Path.GetFileNameWithoutExtension(FileName).ToLower();
			WDB5CounterPart = Database.Entries
							.FirstOrDefault(x => x.Header.IsTypeOf<WDB5>() && Path.GetFileNameWithoutExtension(x.FileName).ToLower() == _filename)?
							.Header as WDB5;

			if (WDB5CounterPart == null)
			{
				throw new InvalidOperationException("You must have the DB2 counterpart open first to be able to read this file.");
			}

			Flags = WDB5CounterPart.Flags;
			IdIndex = WDB5CounterPart.IdIndex;
			FieldStructure = WDB5CounterPart.FieldStructure;

			if (HasOffsetTable)
			{
				Flags = HeaderFlags.OffsetMap;
			}

			base.ReadHeader(ref dbReader, signature);
			TableHash = dbReader.ReadUInt32();
			LayoutHash = dbReader.ReadInt32();
			Build = dbReader.ReadUInt32();
			TimeStamp = dbReader.ReadUInt32();
			MinId = dbReader.ReadInt32();
			MaxId = dbReader.ReadInt32();
			Locale = dbReader.ReadInt32();
		}

		public Dictionary<int, byte[]> ReadOffsetData(BinaryReader dbReader, long pos)
		{
			Dictionary<int, byte[]> CopyTable = new Dictionary<int, byte[]>();
			List<OffsetEntry> offsetmap = new List<OffsetEntry>();

			long indexTablePos = dbReader.BaseStream.Length - (HasIndexTable ? RecordCount * 4 : 0);
			int[] m_indexes = null;

			// Offset Map - Contains the index, offset and length so the index table is not used
			if (HasOffsetTable)
			{
				// Records table
				if (StringBlockSize > 0)
				{
					dbReader.Scrub(StringBlockSize);
				}

				for (int i = 0; i < RecordCount; i++)
				{
					int id = dbReader.ReadInt32();
					int offset = dbReader.ReadInt32();
					short length = dbReader.ReadInt16();

					if (offset == 0 || length == 0)
					{
						continue;
					}

					offsetmap.Add(new OffsetEntry(id, offset, length));
				}
			}

			// Index table
			if (HasIndexTable)
			{
				if (!HasOffsetTable || HasRelationshipData)
				{
					dbReader.Scrub(indexTablePos);
				}

				m_indexes = new int[RecordCount];
				for (int i = 0; i < RecordCount; i++)
				{
					m_indexes[i] = dbReader.ReadInt32();
				}
			}

			// Extract record data
			for (int i = 0; i < Math.Max(RecordCount, offsetmap.Count); i++)
			{
				if (HasOffsetTable)
				{
					OffsetEntry map = offsetmap[i];
					dbReader.Scrub(map.Offset);

					IEnumerable<byte> recordbytes = BitConverter.GetBytes(map.Id).Concat(dbReader.ReadBytes(map.Length));
					CopyTable.Add(map.Id, recordbytes.ToArray());
				}
				else
				{
					dbReader.Scrub(pos + i * RecordSize);
					byte[] recordbytes = dbReader.ReadBytes((int)RecordSize);

					if (HasIndexTable)
					{
						IEnumerable<byte> newrecordbytes = BitConverter.GetBytes(m_indexes[i]).Concat(recordbytes);
						CopyTable.Add(m_indexes[i], newrecordbytes.ToArray());
					}
					else
					{
						int bytecount = FieldStructure[IdIndex].ByteCount;
						int offset = FieldStructure[IdIndex].Offset;

						int id = 0;
						for (int j = 0; j < bytecount; j++)
						{
							id |= recordbytes[offset + j] << j * 8;
						}

						CopyTable.Add(id, recordbytes);
					}
				}
			}

			return CopyTable;
		}

		public override byte[] ReadData(BinaryReader dbReader, long pos)
		{
			Dictionary<int, byte[]> CopyTable = ReadOffsetData(dbReader, pos);
			OffsetLengths = CopyTable.Select(x => x.Value.Length).ToArray();
			return CopyTable.Values.SelectMany(x => x).ToArray();
		}

		internal struct OffsetEntry
		{
			public int Id { get; set; }
			public int Offset { get; set; }
			public short Length { get; set; }

			public OffsetEntry(int id, int offset, short length)
			{
				Id = id;
				Offset = offset;
				Length = length;
			}
		}
		#endregion

		#region Write
		public override void WriteHeader(BinaryWriter writer, DBEntry entry)
		{
			Tuple<int, int> minmax = entry.MinMax();
			writer.BaseStream.Position = 0;

			base.WriteHeader(writer, entry);

			writer.Write(TableHash);
			writer.Write(LayoutHash);
			writer.Write(Build);
			writer.Write(TimeStamp);
			writer.Write(minmax.Item1); //MinId
			writer.Write(minmax.Item2); //MaxId
			writer.Write(Locale);

			//WCH5 has the offsetmap BEFORE the data, create placeholder data
			if (HasOffsetTable)
			{
				OffsetMapOffset = (int)writer.BaseStream.Position;
				writer.BaseStream.Position += entry.GetPrimaryKeys().Count() * (sizeof(int) + sizeof(int) + sizeof(short));
			}

		}

		public override void WriteOffsetMap(BinaryWriter writer, DBEntry entry, List<Tuple<int, short>> OffsetMap, int record_offset = 0)
		{
			// Scrub to after header
			writer.Scrub(OffsetMapOffset); 

			// Write the offset map.
			List<int> ids = entry.GetPrimaryKeys().ToList();
			for (int x = 0; x < ids.Count; x++)
			{
				Tuple<int, short> kvp = OffsetMap[x];
				writer.Write(ids[x]);
				writer.Write(kvp.Item1);
				writer.Write(kvp.Item2);
			}
			ids.Clear();

			// Clear string table size.
			long pos = writer.BaseStream.Position;
			writer.Scrub(entry.Header.StringTableOffset);
			writer.Write(0);
			writer.Scrub(pos);
		}

		public override void WriteIndexTable(BinaryWriter writer, DBEntry entry)
		{
			int m = 0;
			int[] ids = entry.GetPrimaryKeys().ToArray();

			if (entry.Header.HasRelationshipData)
			{
				ushort[] secondids = entry.Data.Rows.Cast<DataRow>().Select(x => x.Field<ushort>(2)).ToArray();

				// Write all of the secondary ids
				foreach (ushort id in secondids)
				{
					// Populate missing secondary ids with 0
					if (m > 0 && ids[m] - ids[m - 1] > 1)
					{
						writer.BaseStream.Position += sizeof(int) * (ids[m] - ids[m - 1] - 1);
					}

					writer.Write((int)id);
					m++;
				}
			}

			//Write all the primary IDs
			writer.WriteArray(ids);
		}

		public override void WriteRecordPadding(BinaryWriter writer, DBEntry entry, long offset) { }

		#endregion
	}
}
