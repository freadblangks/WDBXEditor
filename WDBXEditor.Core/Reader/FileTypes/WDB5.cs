using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using WDBXEditor.Core.Storage;
using WDBXEditor.Core.Reader.Enums;

namespace WDBXEditor.Core.Reader.FileTypes
{
	public class WDB5 : DBHeader
	{
		public override bool ExtendedStringTable => true;

		public override bool HasOffsetTable => Flags.HasFlag(HeaderFlags.OffsetMap);
		public override bool HasIndexTable => Flags.HasFlag(HeaderFlags.IndexMap);
		public override bool HasRelationshipData => Flags.HasFlag(HeaderFlags.RelationshipData);

		#region Read

		public void ReadHeader(BinaryReader dbReader, string signature)
		{
			ReadHeader(ref dbReader, signature);
		}

		public void ReadBaseHeader(ref BinaryReader dbReader, string signature)
		{
			base.ReadHeader(ref dbReader, signature);
		}

		public override void ReadHeader(ref BinaryReader dbReader, string signature)
		{
			base.ReadHeader(ref dbReader, signature);

			TableHash = dbReader.ReadUInt32();
			LayoutHash = dbReader.ReadInt32();
			MinId = dbReader.ReadInt32();
			MaxId = dbReader.ReadInt32();
			Locale = dbReader.ReadInt32();
			CopyTableSize = dbReader.ReadInt32();
			Flags = (HeaderFlags)dbReader.ReadUInt16();
			IdIndex = dbReader.ReadUInt16();

			if (Flags.HasFlag(HeaderFlags.IndexMap))
			{
				// Ignored if Index Table.
				IdIndex = 0; 
			}

			//Gather field structures
			FieldStructure = new List<FieldStructureEntry>();
			for (int i = 0; i < FieldCount; i++)
			{
				var field = new FieldStructureEntry(dbReader.ReadInt16(), (ushort)(dbReader.ReadUInt16() + (HasIndexTable ? 4 : 0)));
				FieldStructure.Add(field);

				if (i > 0)
				{
					FieldStructure[i - 1].SetLength(field);
				}
			}

			if (HasIndexTable)
			{
				FieldCount++;
				FieldStructure.Insert(0, new FieldStructureEntry(0, 0));

				if (FieldCount > 1)
				{
					FieldStructure[1].SetLength(FieldStructure[0]);
				}
			}
		}

		public Dictionary<int, byte[]> ReadOffsetData(BinaryReader dbReader, long pos)
		{
			var copyTable = new Dictionary<int, byte[]>();
			var offsetmap = new List<Tuple<int, short>>();
			var firstindex = new Dictionary<int, OffsetDuplicate>();

			long copyTablePos = dbReader.BaseStream.Length - CopyTableSize;
			long indexTablePos = copyTablePos - (HasIndexTable ? RecordCount * 4 : 0);
			int[] m_indexes = null;

			// Offset Map.
			if (HasOffsetTable)
			{
				// Records table.
				dbReader.Scrub(StringBlockSize);

				for (int i = 0; i < MaxId - MinId + 1; i++)
				{
					int offset = dbReader.ReadInt32();
					short length = dbReader.ReadInt16();

					if (offset == 0 || length == 0)
					{
						continue;
					}

					//Special case, may contain duplicates in the offset map that we don't want
					if (CopyTableSize == 0)
					{
						if (!firstindex.ContainsKey(offset))
						{
							firstindex.Add(offset, new OffsetDuplicate(offsetmap.Count, firstindex.Count));
						}
						else
						{
							OffsetDuplicates.Add(MinId + i, firstindex[offset].VisibleIndex);
						}
					}

					offsetmap.Add(new Tuple<int, short>(offset, length));
				}
			}

			if (HasRelationshipData)
			{
				dbReader.BaseStream.Position += (MaxId - MinId + 1) * 4;
			}

			// Index table.
			if (HasIndexTable)
			{
				// Offset map alone reads straight into this. Others may not.
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

			// Extract record data.
			for (int i = 0; i < Math.Max(RecordCount, offsetmap.Count); i++)
			{
				if (HasOffsetTable)
				{
					int id = m_indexes[copyTable.Count];
					Tuple<int, short> map = offsetmap[i];

					// Ignore duplicates.
					if (CopyTableSize == 0 && firstindex[map.Item1].HiddenIndex != i)
					{
						continue;
					}

					dbReader.Scrub(map.Item1);

					IEnumerable<byte> recordbytes = BitConverter.GetBytes(id).Concat(dbReader.ReadBytes(map.Item2));
					copyTable.Add(id, recordbytes.ToArray());
				}
				else
				{
					dbReader.Scrub(pos + i * RecordSize);
					byte[] recordbytes = dbReader.ReadBytes((int)RecordSize);

					if (HasIndexTable)
					{
						IEnumerable<byte> newrecordbytes = BitConverter.GetBytes(m_indexes[i]).Concat(recordbytes);
						copyTable.Add(m_indexes[i], newrecordbytes.ToArray());
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

						copyTable.Add(id, recordbytes);
					}
				}
			}

			// CopyTable
			if (CopyTableSize != 0 && copyTablePos != dbReader.BaseStream.Length)
			{
				dbReader.Scrub(copyTablePos);
				while (dbReader.BaseStream.Position != dbReader.BaseStream.Length)
				{
					int id = dbReader.ReadInt32();
					int idcopy = dbReader.ReadInt32();

					byte[] copyRow = copyTable[idcopy];
					byte[] newRow = new byte[copyRow.Length];
					Array.Copy(copyRow, newRow, newRow.Length);
					Array.Copy(BitConverter.GetBytes(id), newRow, sizeof(int));

					copyTable.Add(id, newRow);
				}
			}

			return copyTable;
		}

		public override byte[] ReadData(BinaryReader dbReader, long pos)
		{
			Dictionary<int, byte[]> CopyTable = ReadOffsetData(dbReader, pos);
			OffsetLengths = CopyTable.Select(x => x.Value.Length).ToArray();
			return CopyTable.Values.SelectMany(x => x).ToArray();
		}

		internal struct OffsetDuplicate
		{
			public int HiddenIndex { get; set; }
			public int VisibleIndex { get; set; }

			public OffsetDuplicate(int hidden, int visible)
			{
				HiddenIndex = hidden;
				VisibleIndex = visible;
			}
		}

		#endregion

		#region Write

		public virtual void WriteBaseHeader(BinaryWriter writer, DBEntry entry)
		{
			base.WriteHeader(writer, entry);
		}

		public override void WriteHeader(BinaryWriter writer, DBEntry entry)
		{
			Tuple<int, int> minmax = entry.MinMax();
			writer.BaseStream.Position = 0;

			base.WriteHeader(writer, entry);

			writer.Write((int)TableHash);
			writer.Write(LayoutHash);
			writer.Write(minmax.Item1); //MinId
			writer.Write(minmax.Item2); //MaxId
			writer.Write(Locale);
			writer.Write(0); //CopyTableSize
			writer.Write((ushort)Flags); //Flags
			writer.Write(IdIndex); //IdColumn

			// Write the field_structure bits.
			for (int i = 0; i < FieldStructure.Count; i++)
			{
				if (HasIndexTable && i == 0)
				{
					continue;
				}

				writer.Write(FieldStructure[i].Bits);
				writer.Write(HasIndexTable ? (ushort)(FieldStructure[i].Offset - 4) : FieldStructure[i].Offset);
			}
		}

		public override void WriteOffsetMap(BinaryWriter writer, DBEntry entry, List<Tuple<int, short>> OffsetMap, int record_offset = 0)
		{
			Tuple<int, int> minmax = entry.MinMax();
			Dictionary<int, int> duplicates = entry.Header.OffsetDuplicates;
			var ids = new HashSet<int>(entry.GetPrimaryKeys());

			int m = 0;
			for (int x = minmax.Item1; x <= minmax.Item2; x++)
			{
				// Insert the offset map.
				if (ids.Contains(x)) 
				{
					Tuple<int, short> kvp = OffsetMap[m];
					writer.Write(kvp.Item1 + record_offset);
					writer.Write(kvp.Item2);
					m++;
				}

				// Re-insert our duplicates.
				else if (duplicates.ContainsKey(x)) 
				{
					Tuple<int, short> hiddenkvp = OffsetMap[duplicates[x]];
					writer.Write(hiddenkvp.Item1 + record_offset);
					writer.Write(hiddenkvp.Item2);
				}
				else
				{
					// 0 fill.
					writer.BaseStream.Position += sizeof(int) + sizeof(short); 
				}
			}

			ids.Clear();
		}

		public override void WriteIndexTable(BinaryWriter writer, DBEntry entry)
		{
			int m = 0;
			int[] ids;
			int index = entry.Data.Columns.IndexOf(entry.Key);

			if (!HasOffsetTable && entry.Header.CopyTableSize > 0)
			{
				ids = entry.GetUniqueRows().Select(x => x.Field<int>(index)).ToArray();
			}
			else
			{
				ids = entry.GetPrimaryKeys().ToArray();
			}

			if (entry.Header.HasRelationshipData)
			{
				//TODO figure out if it is always the 2nd column
				ushort[] secondIds = entry.Data.Rows.Cast<DataRow>().Select(x => x.Field<ushort>(2)).ToArray();

				// Write all of the secondary ids.
				foreach (ushort id in secondIds)
				{
					// Populate missing secondary ids with 0.
					if (m > 0 && ids[m] - ids[m - 1] > 1)
					{
						writer.BaseStream.Position += sizeof(int) * (ids[m] - ids[m - 1] - 1);
					}

					writer.Write((int)id);
					m++;
				}
			}

			// Write all the IDs.
			writer.WriteArray(ids);
		}

		public virtual void WriteCopyTable(BinaryWriter writer, DBEntry entry)
		{
			if (!HasOffsetTable && CopyTableSize != 0)
			{
				int index = entry.Data.Columns.IndexOf(entry.Key);
				var copyRows = entry.GetCopyRows();
				if (copyRows.Count() > 0)
				{
					int size = 0;
					foreach (IEnumerable<int> copies in copyRows)
					{
						int keyindex = copies.First();
						foreach (int copy in copies.Skip(1))
						{
							writer.Write(copy);
							writer.Write(keyindex);
							size += sizeof(int) + sizeof(int);
						}
					}

					// Set CopyTableSize.
					long pos = writer.BaseStream.Position;
					writer.Scrub(0x28);
					writer.Write(size);
					writer.Scrub(pos);
				}
			}
		}

		public override void WriteRecordPadding(BinaryWriter writer, DBEntry entry, long offset)
		{
			if (IsTypeOf<WDB6>() && HasOffsetTable && HasIndexTable)
			{
				writer.BaseStream.Position += 2;
			}
			else if (!IsTypeOf<WDB6>() && HasOffsetTable)
			{
				// Offset map always has 2 bytes padding.
				writer.BaseStream.Position += 2; 
			}
			else
			{
				// Scrub to the end of the record if necessary.
				base.WriteRecordPadding(writer, entry, offset); 
			}
		}

		#endregion
	}
}
