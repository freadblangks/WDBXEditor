using Acmil.Core.Reader;
using Acmil.Core.Reader.Enums;
using Acmil.Core.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Acmil.Core.Common.Constants;

namespace Acmil.Core.Reader.FileTypes
{
	public class WDB6 : WDB5
	{
		#region Read

		public override void ReadHeader(ref BinaryReader dbReader, string signature)
		{
			ReadBaseHeader(ref dbReader, signature);

			TableHash = dbReader.ReadUInt32();
			LayoutHash = dbReader.ReadInt32();
			MinId = dbReader.ReadInt32();
			MaxId = dbReader.ReadInt32();
			Locale = dbReader.ReadInt32();
			CopyTableSize = dbReader.ReadInt32();
			Flags = (HeaderFlags)dbReader.ReadUInt16();
			IdIndex = dbReader.ReadUInt16();
			TotalFieldSize = dbReader.ReadUInt32();
			CommonDataTableSize = dbReader.ReadUInt32();

			// Ignored if Index Table.
			if (HasIndexTable)
			{
				IdIndex = 0;
			}

			//RecordSize header field is not right anymore.
			InternalRecordSize = RecordSize;

			// Gather field structures.
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
				InternalRecordSize += 4;
				FieldCount++;
				FieldStructure.Insert(0, new FieldStructureEntry(0, 0));

				if (FieldCount > 1)
				{
					FieldStructure[1].SetLength(FieldStructure[0]);
				}
			}
		}

		public new Dictionary<int, byte[]> ReadOffsetData(BinaryReader dbReader, long pos)
		{
			var copyTable = new Dictionary<int, byte[]>();
			var offsetMap = new List<Tuple<int, short>>();
			var firstIndex = new Dictionary<int, OffsetDuplicate>();

			long commonDataTablePos = dbReader.BaseStream.Length - CommonDataTableSize;
			long copyTablePos = commonDataTablePos - CopyTableSize;
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

					// Special case, may contain duplicates in the offset map that we don't want.
					if (CopyTableSize == 0)
					{
						if (!firstIndex.ContainsKey(offset))
						{
							firstIndex.Add(offset, new OffsetDuplicate(offsetMap.Count, firstIndex.Count));
						}
						else
						{
							OffsetDuplicates.Add(MinId + i, firstIndex[offset].VisibleIndex);
						}
					}

					offsetMap.Add(new Tuple<int, short>(offset, length));
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
			for (int i = 0; i < Math.Max(RecordCount, offsetMap.Count); i++)
			{
				if (HasOffsetTable && m_indexes != null)
				{
					int id = m_indexes[Math.Min(copyTable.Count, m_indexes.Length - 1)];
					Tuple<int, short> map = offsetMap[i];

					// Ignore duplicates.
					if (CopyTableSize == 0 && firstIndex[map.Item1].HiddenIndex != i)
					{
						continue;
					}

					dbReader.Scrub(map.Item1);

					IEnumerable<byte> recordbytes = BitConverter.GetBytes(id)
													.Concat(dbReader.ReadBytes(map.Item2));

					copyTable.Add(id, recordbytes.ToArray());
				}
				else
				{
					dbReader.Scrub(pos + i * RecordSize);
					byte[] recordbytes = dbReader.ReadBytes((int)RecordSize).ToArray();

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

			// CopyTable.
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

			// CommonDataTable.
			if (CommonDataTableSize > 0)
			{
				dbReader.Scrub(commonDataTablePos);
				int columncount = dbReader.ReadInt32();

				var commondatalookup = new Dictionary<int, byte[]>[columncount];

				// Initial Data extraction.
				for (int i = 0; i < columncount; i++)
				{
					int count = dbReader.ReadInt32();
					byte type = dbReader.ReadByte();
					short bit = CommonDataBits[type];
					int size = 32 - bit >> 3;

					commondatalookup[i] = new Dictionary<int, byte[]>();

					// New field not defined in header.
					if (i > FieldStructure.Count - 1)
					{
						var offset = (ushort)(FieldStructure.Count == 0 ? 0 : FieldStructure[i - 1].Offset + FieldStructure[i - 1].ByteCount);
						FieldStructure.Add(new FieldStructureEntry(bit, offset, type));

						if (FieldStructure.Count > 1)
						{
							FieldStructure[i - 1].SetLength(FieldStructure[i]);
						}
					}

					for (int x = 0; x < count; x++)
					{
						commondatalookup[i].Add(dbReader.ReadInt32(), dbReader.ReadBytes(size));

						if (TableStructure == null || TableStructure?.Build >= 24492)
						{
							dbReader.ReadBytes(4 - size);
						}
					}
				}

				int[] ids = copyTable.Keys.ToArray();
				foreach (var id in ids)
				{
					for (int i = 0; i < commondatalookup.Length; i++)
					{
						if (!FieldStructure[i].CommonDataColumn)
						{
							continue;
						}

						Dictionary<int, byte[]> col = commondatalookup[i];
						string defaultValue = TableStructure?.Fields?[i]?.DefaultValue;
						defaultValue = string.IsNullOrEmpty(defaultValue) ? "0" : defaultValue;

						FieldStructureEntry field = FieldStructure[i];
						byte[] zeroData = new byte[field.ByteCount];
						if (defaultValue != "0")
						{
							switch (field.CommonDataType)
							{
								case 1:
									zeroData = BitConverter.GetBytes(ushort.Parse(defaultValue));
									break;
								case 2:
									zeroData = new[] { byte.Parse(defaultValue) };
									break;
								case 3:
									zeroData = BitConverter.GetBytes(float.Parse(defaultValue));
									break;
								case 4:
									zeroData = BitConverter.GetBytes(int.Parse(defaultValue));
									break;
							}
						}

						byte[] currentData = copyTable[id];
						byte[] data = col.ContainsKey(id) ? col[id] : zeroData;
						Array.Resize(ref currentData, currentData.Length + data.Length);
						Array.Copy(data, 0, currentData, field.Offset, data.Length);
						copyTable[id] = currentData;
					}
				}

				commondatalookup = null;
				InternalRecordSize = (uint)copyTable.Values.First().Length;
			}

			return copyTable;
		}

		public override byte[] ReadData(BinaryReader dbReader, long pos)
		{
			Dictionary<int, byte[]> CopyTable = ReadOffsetData(dbReader, pos);
			OffsetLengths = CopyTable.Select(x => x.Value.Length).ToArray();
			return CopyTable.Values.SelectMany(x => x).ToArray();
		}

		#endregion

		#region Write

		public override void WriteHeader(BinaryWriter writer, DBEntry entry)
		{
			Tuple<int, int> minMax = entry.MinMax();
			writer.BaseStream.Position = 0;

			WriteBaseHeader(writer, entry);

			writer.Write((int)TableHash);
			writer.Write(LayoutHash);
			writer.Write(minMax.Item1); //MinId
			writer.Write(minMax.Item2); //MaxId
			writer.Write(Locale);
			writer.Write(0); //CopyTableSize
			writer.Write((ushort)Flags); //Flags
			writer.Write(IdIndex); //IdColumn
			writer.Write(TotalFieldSize);
			writer.Write(0); //CommonDataTableSize

			// Write the field_structure bits.
			for (int i = 0; i < FieldStructure.Count; i++)
			{
				if (HasIndexTable && i == 0 || FieldStructure[i].CommonDataColumn)
				{
					continue;
				}

				writer.Write(FieldStructure[i].Bits);
				writer.Write(HasIndexTable ? (ushort)(FieldStructure[i].Offset - 4) : FieldStructure[i].Offset);
			}
		}

		public virtual void WriteCommonDataTable(BinaryWriter writer, DBEntry entry)
		{
			if (CommonDataTableSize != 0)
			{
				long start = writer.BaseStream.Position; //Current position
				IEnumerable<DataRow> rows = entry.Data.Rows.Cast<DataRow>();

				writer.WriteUInt32((uint)FieldStructure.Count); //Field count
				for (int i = 0; i < FieldStructure.Count; i++)
				{
					string field = TableStructure.Fields[i].InternalName;
					string defaultValue = TableStructure.Fields[i].DefaultValue;
					TypeCode typeCode = Type.GetTypeCode(entry.Data.Columns[field].DataType);
					DataColumn pk = entry.Data.PrimaryKey[0];

					string numberDefault = string.IsNullOrEmpty(defaultValue) ? "0" : defaultValue;
					var data = new Dictionary<int, byte[]>();
					int padding = 0;

					// Only get data if CommonDataTable.
					if (FieldStructure[i].CommonDataColumn)
					{
						switch (typeCode)
						{
							case TypeCode.String:
								data = rows.Where(x => (string)x[field] != defaultValue).ToDictionary(x => (int)x[pk], y => Encoding.UTF8.GetBytes((string)y[field]));
								break;
							case TypeCode.UInt16:
								data = rows.Where(x => (ushort)x[field] != ushort.Parse(numberDefault)).ToDictionary(x => (int)x[pk], y => BitConverter.GetBytes((ushort)y[field]));
								padding = 2;
								break;
							case TypeCode.Int16:
								data = rows.Where(x => (short)x[field] != short.Parse(numberDefault)).ToDictionary(x => (int)x[pk], y => BitConverter.GetBytes((short)y[field]));
								padding = 2;
								break;
							case TypeCode.Single:
								data = rows.Where(x => (float)x[field] != float.Parse(numberDefault)).ToDictionary(x => (int)x[pk], y => BitConverter.GetBytes((float)y[field]));
								break;
							case TypeCode.Int32:
								data = rows.Where(x => (int)x[field] != int.Parse(numberDefault)).ToDictionary(x => (int)x[pk], y => BitConverter.GetBytes((int)y[field]));
								break;
							case TypeCode.UInt32:
								data = rows.Where(x => (uint)x[field] != uint.Parse(numberDefault)).ToDictionary(x => (int)x[pk], y => BitConverter.GetBytes((uint)y[field]));
								break;
							case TypeCode.Byte:
								data = rows.Where(x => (byte)x[field] != byte.Parse(numberDefault)).ToDictionary(x => (int)x[pk], y => new byte[] { (byte)y[field] });
								padding = 3;
								break;
							default:
								continue;
						}
					}

					writer.WriteInt32(data.Count); // Count
					writer.Write(CommonDataTypes[typeCode]); // Type code
					foreach (KeyValuePair<int, byte[]> datum in data)
					{
						writer.WriteInt32(datum.Key); //Id
						writer.Write(datum.Value); //Value

						if ((TableStructure == null || TableStructure?.Build >= 24492) && padding > 0)
						{
							writer.BaseStream.Position += padding;
						}
					}
				}

				//Set CommonDataTableSize
				long pos = writer.BaseStream.Position;
				writer.Scrub(0x34);
				writer.WriteInt32((int)(pos - start));
				writer.Scrub(pos);
			}
		}

		#endregion
	}
}
