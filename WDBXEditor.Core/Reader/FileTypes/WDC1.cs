using Acmil.Core.Common.Enums;
using Acmil.Core.Reader.Enums;
using Acmil.Core.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acmil.Core.Reader.FileTypes
{
	public class WDC1 : WDB6
	{
		public int PackedDataOffset;
		public uint RelationshipCount;
		public int OffsetTableOffset;
		public int IndexSize;
		public int ColumnMetadataSize;
		public int SparseDataSize;
		public int PalletDataSize;
		public int RelationshipDataSize;

		public List<ColumnStructureEntry> ColumnMeta;
		public RelationShipData RelationShipData;

		protected int[] columnSizes;
		protected byte[] recordData;

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

			PackedDataOffset = dbReader.ReadInt32();
			RelationshipCount = dbReader.ReadUInt32();
			OffsetTableOffset = dbReader.ReadInt32();
			IndexSize = dbReader.ReadInt32();
			ColumnMetadataSize = dbReader.ReadInt32();
			SparseDataSize = dbReader.ReadInt32();
			PalletDataSize = dbReader.ReadInt32();
			RelationshipDataSize = dbReader.ReadInt32();

			// Gather field structures.
			FieldStructure = new List<FieldStructureEntry>();
			for (int i = 0; i < FieldCount; i++)
			{
				var field = new FieldStructureEntry(dbReader.ReadInt16(), dbReader.ReadUInt16());
				FieldStructure.Add(field);
			}

			recordData = dbReader.ReadBytes((int)(RecordCount * RecordSize));
			Array.Resize(ref recordData, recordData.Length + 8);

			Flags &= ~HeaderFlags.RelationshipData; // appears to be obsolete now
		}

		public new Dictionary<int, byte[]> ReadOffsetData(BinaryReader dbReader, long pos)
		{
			var copyTable = new Dictionary<int, byte[]>();
			var offsetMap = new List<Tuple<int, short>>();
			var firstIndex = new Dictionary<int, OffsetDuplicate>();
			var offsetDuplicates = new Dictionary<int, int>();
			var copies = new Dictionary<int, List<int>>();

			int[] m_indexes = null;

			// OffsetTable.
			if (HasOffsetTable && OffsetTableOffset > 0)
			{
				dbReader.BaseStream.Position = OffsetTableOffset;
				for (int i = 0; i < MaxId - MinId + 1; i++)
				{
					int offset = dbReader.ReadInt32();
					short length = dbReader.ReadInt16();

					if (offset == 0 || length == 0)
					{
						continue;
					}

					// Special case: May contain duplicates in the offset map that we don't want.
					if (CopyTableSize == 0)
					{
						if (!firstIndex.ContainsKey(offset))
						{
							firstIndex.Add(offset, new OffsetDuplicate(offsetMap.Count, firstIndex.Count));
						}
						else
						{
							offsetDuplicates.Add(MinId + i, firstIndex[offset].VisibleIndex);
							continue;
						}
					}

					offsetMap.Add(new Tuple<int, short>(offset, length));
				}
			}

			// IndexTable.
			if (HasIndexTable)
			{
				m_indexes = new int[RecordCount];
				for (int i = 0; i < RecordCount; i++)
				{
					m_indexes[i] = dbReader.ReadInt32();
				}
			}

			// CopyTable.
			if (CopyTableSize > 0)
			{
				long end = dbReader.BaseStream.Position + CopyTableSize;
				while (dbReader.BaseStream.Position < end)
				{
					int id = dbReader.ReadInt32();
					int idCopy = dbReader.ReadInt32();

					if (!copies.ContainsKey(idCopy))
					{
						copies.Add(idCopy, new List<int>());
					}

					copies[idCopy].Add(id);
				}
			}

			// ColumnMeta.
			ColumnMeta = new List<ColumnStructureEntry>();
			for (int i = 0; i < FieldCount; i++)
			{
				var column = new ColumnStructureEntry()
				{
					RecordOffset = dbReader.ReadUInt16(),
					Size = dbReader.ReadUInt16(),
					AdditionalDataSize = dbReader.ReadUInt32(), // size of pallet / sparse values
					CompressionType = (CompressionType)dbReader.ReadUInt32(),
					BitOffset = dbReader.ReadInt32(),
					BitWidth = dbReader.ReadInt32(),
					Cardinality = dbReader.ReadInt32()
				};

				// Pre-load arraysizes.
				if (column.CompressionType == CompressionType.None)
				{
					column.ArraySize = Math.Max(column.Size / FieldStructure[i].BitCount, 1);
				}
				else if (column.CompressionType == CompressionType.PalletArray)
				{
					column.ArraySize = Math.Max(column.Cardinality, 1);
				}

				ColumnMeta.Add(column);
			}

			// Pallet values.
			for (int i = 0; i < ColumnMeta.Count; i++)
			{
				if (ColumnMeta[i].CompressionType == CompressionType.Pallet || ColumnMeta[i].CompressionType == CompressionType.PalletArray)
				{
					int elements = (int)ColumnMeta[i].AdditionalDataSize / 4;
					int cardinality = Math.Max(ColumnMeta[i].Cardinality, 1);

					ColumnMeta[i].PalletValues = new List<byte[]>();
					for (int j = 0; j < elements / cardinality; j++)
					{
						ColumnMeta[i].PalletValues.Add(dbReader.ReadBytes(cardinality * 4));
					}
				}
			}

			// Sparse values.
			for (int i = 0; i < ColumnMeta.Count; i++)
			{
				if (ColumnMeta[i].CompressionType == CompressionType.Sparse)
				{
					ColumnMeta[i].SparseValues = new Dictionary<int, byte[]>();
					for (int j = 0; j < ColumnMeta[i].AdditionalDataSize / 8; j++)
					{
						ColumnMeta[i].SparseValues[dbReader.ReadInt32()] = dbReader.ReadBytes(4);
					}
				}
			}

			// Relationships.
			if (RelationshipDataSize > 0)
			{
				RelationShipData = new RelationShipData()
				{
					Records = dbReader.ReadUInt32(),
					MinId = dbReader.ReadUInt32(),
					MaxId = dbReader.ReadUInt32(),
					Entries = new Dictionary<uint, byte[]>()
				};

				for (int i = 0; i < RelationShipData.Records; i++)
				{
					byte[] foreignKey = dbReader.ReadBytes(4);
					uint index = dbReader.ReadUInt32();

					// Has duplicates just like the copy table does... why?
					if (!RelationShipData.Entries.ContainsKey(index))
					{
						RelationShipData.Entries.Add(index, foreignKey);
					}
				}

				FieldStructure.Add(new FieldStructureEntry(0, 0));
				ColumnMeta.Add(new ColumnStructureEntry());
			}

			// Record Data.
			BitStream bitStream = new BitStream(recordData);
			for (int i = 0; i < RecordCount; i++)
			{
				int id = 0;
				if (HasOffsetTable && HasIndexTable)
				{
					id = m_indexes[copyTable.Count];
					Tuple<int, short> map = offsetMap[i];

					if (CopyTableSize == 0 && firstIndex[map.Item1].HiddenIndex != i) //Ignore duplicates
					{
						continue;
					}

					dbReader.BaseStream.Position = map.Item1;
					byte[] data = dbReader.ReadBytes(map.Item2);
					IEnumerable<byte> recordbytes = BitConverter.GetBytes(id).Concat(data);

					// Append relationship id.
					if (RelationShipData != null)
					{
						// Seen cases of missing indicies.
						if (RelationShipData.Entries.TryGetValue((uint)i, out byte[] foreignData))
						{
							recordbytes = recordbytes.Concat(foreignData);
						}
						else
						{
							recordbytes = recordbytes.Concat(new byte[4]);
						}
					}

					copyTable.Add(id, recordbytes.ToArray());

					if (copies.ContainsKey(id))
					{
						foreach (int copy in copies[id])
						{
							copyTable.Add(copy, BitConverter.GetBytes(copy).Concat(data).ToArray());
						}
					}
				}
				else
				{
					bitStream.Seek(i * RecordSize, 0);
					int idOffset = 0;

					var data = new List<byte>();
					if (HasIndexTable)
					{
						id = m_indexes[i];
						data.AddRange(BitConverter.GetBytes(id));
					}

					int c = HasIndexTable ? 1 : 0;
					for (int f = 0; f < FieldCount; f++)
					{
						int bitOffset = ColumnMeta[f].BitOffset;
						int bitWidth = ColumnMeta[f].BitWidth;
						int cardinality = ColumnMeta[f].Cardinality;
						uint palletIndex;
						int take = columnSizes[c] * ColumnMeta[f].ArraySize;

						switch (ColumnMeta[f].CompressionType)
						{
							case CompressionType.None:
								int bitSize = FieldStructure[f].BitCount;
								if (!HasIndexTable && f == IdIndex)
								{
									idOffset = data.Count;
									id = bitStream.ReadInt32(bitSize); // always read Ids as ints
									data.AddRange(BitConverter.GetBytes(id));
								}
								else
								{
									data.AddRange(bitStream.ReadBytes(bitSize * ColumnMeta[f].ArraySize, false, take));
								}
								break;

							case CompressionType.Immediate:
							case CompressionType.SignedImmediate:
								if (!HasIndexTable && f == IdIndex)
								{
									idOffset = data.Count;
									id = bitStream.ReadInt32(bitWidth); // always read Ids as ints
									data.AddRange(BitConverter.GetBytes(id));
								}
								else
								{
									data.AddRange(bitStream.ReadBytes(bitWidth, false, take));
								}
								break;

							case CompressionType.Sparse:
								if (ColumnMeta[f].SparseValues.TryGetValue(id, out byte[] valBytes))
								{
									data.AddRange(valBytes.Take(take));
								}
								else
								{
									data.AddRange(BitConverter.GetBytes(ColumnMeta[f].BitOffset).Take(take));
								}
								break;

							case CompressionType.Pallet:
							case CompressionType.PalletArray:
								palletIndex = bitStream.ReadUInt32(bitWidth);
								data.AddRange(ColumnMeta[f].PalletValues[(int)palletIndex].Take(take));
								break;

							default:
								throw new Exception($"Unknown compression {ColumnMeta[f].CompressionType}");

						}

						c += ColumnMeta[f].ArraySize;
					}

					// Append relationship id.
					if (RelationShipData != null)
					{
						// Seen cases of missing indicies.
						if (RelationShipData.Entries.TryGetValue((uint)i, out byte[] foreignData))
						{
							data.AddRange(foreignData);
						}
						else
						{
							data.AddRange(new byte[4]);
						}
					}

					copyTable.Add(id, data.ToArray());

					if (copies.ContainsKey(id))
					{
						foreach (int copy in copies[id])
						{
							byte[] newrecord = copyTable[id].ToArray();
							Buffer.BlockCopy(BitConverter.GetBytes(copy), 0, newrecord, idOffset, 4);
							copyTable.Add(copy, newrecord);
						}
					}
				}
			}

			if (HasIndexTable)
			{
				FieldStructure.Insert(0, new FieldStructureEntry(0, 0));
				ColumnMeta.Insert(0, new ColumnStructureEntry());
			}

			offsetMap.Clear();
			firstIndex.Clear();
			offsetDuplicates.Clear();
			copies.Clear();
			Array.Resize(ref recordData, 0);
			bitStream.Dispose();
			ColumnMeta.ForEach(x => { x.PalletValues?.Clear(); x.SparseValues?.Clear(); });

			InternalRecordSize = (uint)copyTable.First().Value.Length;

			if (CopyTableSize > 0)
			{
				copyTable = copyTable.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
			}

			return copyTable;
		}

		public override byte[] ReadData(BinaryReader dbReader, long pos)
		{
			Dictionary<int, byte[]> CopyTable = ReadOffsetData(dbReader, pos);
			OffsetLengths = CopyTable.Select(x => x.Value.Length).ToArray();
			return CopyTable.Values.SelectMany(x => x).ToArray();
		}

		public virtual Dictionary<int, string> ReadStringTable(BinaryReader dbReader)
		{
			long pos = dbReader.BaseStream.Position;
			return new StringTable().Read(dbReader, pos, pos + StringBlockSize);
		}


		public void LoadDefinitionSizes(DBEntry entry)
		{
			if (!HasOffsetTable)
			{
				var typeLookup = new Dictionary<TypeCode, int>()
				{
					{ TypeCode.Byte, 1 },
					{ TypeCode.SByte, 1 },
					{ TypeCode.UInt16, 2 },
					{ TypeCode.Int16, 2 },
					{ TypeCode.Int32, 4 },
					{ TypeCode.UInt32, 4 },
					{ TypeCode.Int64, 8 },
					{ TypeCode.UInt64, 8 },
					{ TypeCode.String, 4 },
					{ TypeCode.Single, 4 }
				};
				columnSizes = entry.Data.Columns.Cast<DataColumn>().Select(x => typeLookup[Type.GetTypeCode(x.DataType)]).ToArray();
			}
		}

		public void AddRelationshipColumn(DBEntry entry)
		{
			if (RelationShipData != null && !entry.Data.Columns.Cast<DataColumn>().Any(x => x.ExtendedProperties.ContainsKey("RELATIONSHIP")))
			{
				DataColumn dataColumn = new DataColumn("RelationshipData", typeof(uint));
				dataColumn.ExtendedProperties.Add("RELATIONSHIP", true);
				entry.Data.Columns.Add(dataColumn);
			}
		}

		#endregion

		#region Write

		public override void WriteHeader(BinaryWriter writer, DBEntry entry)
		{
			Tuple<int, int> minMax = entry.MinMax();
			writer.BaseStream.Position = 0;

			// Fix the bitlimits.
			RemoveBitLimits();

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

			writer.Write(PackedDataOffset);
			writer.Write(RelationshipCount);
			writer.Write(0);  // OffsetTableOffset
			writer.Write(0);  // IndexSize
			writer.Write(0);  // ColumnMetadataSize
			writer.Write(0);  // SparseDataSize
			writer.Write(0);  // PalletDataSize
			writer.Write(0);  // RelationshipDataSize

			// Write the field_structure bits
			for (int i = 0; i < FieldStructure.Count; i++)
			{
				if (HasIndexTable && i == 0 || RelationShipData != null && i == FieldStructure.Count - 1)
				{
					continue;
				}

				writer.Write(FieldStructure[i].Bits);
				writer.Write(FieldStructure[i].Offset);
			}

			WriteData(writer, entry);
		}

		/// <summary>
		/// WDC1 writing is entirely different so has been moved to inside the class.
		/// Will work on inheritence when WDC2 comes along - can't wait...
		/// </summary>
		/// <param name="writer"></param>
		/// <param name="entry"></param>

		public virtual void WriteData(BinaryWriter writer, DBEntry entry)
		{
			var offsetMap = new List<Tuple<int, short>>();
			var stringTable = new StringTable(true);
			bool isSparse = HasIndexTable && HasOffsetTable;
			var copyRecords = new Dictionary<int, IEnumerable<int>>();
			var copyIdsSet = new HashSet<int>();

			long pos = writer.BaseStream.Position;

			// Get a list of identical records.
			if (CopyTableSize > 0)
			{
				IEnumerable<int> copyIds = Enumerable.Empty<int>();
				var copies = entry.GetCopyRows();
				foreach (var c in copies)
				{
					int id = c.First();
					copyRecords.Add(id, c.Skip(1).ToList());
					copyIds = copyIds.Concat(copyRecords[id]);
				}

				copyIdsSet = new HashSet<int>(copyIds);
			}

			// Get relationship data.
			DataColumn relationshipColumn = entry.Data.Columns.Cast<DataColumn>().FirstOrDefault(x => x.ExtendedProperties.ContainsKey("RELATIONSHIP"));
			if (relationshipColumn != null)
			{
				int index = entry.Data.Columns.IndexOf(relationshipColumn);

				Dictionary<int, uint> relationData = new Dictionary<int, uint>();
				foreach (DataRow r in entry.Data.Rows)
				{
					int id = r.Field<int>(entry.Key);
					if (!copyIdsSet.Contains(id))
					{
						relationData.Add(id, r.Field<uint>(index));
					}
				}

				RelationShipData = new RelationShipData()
				{
					Records = (uint)relationData.Count,
					MinId = relationData.Values.Min(),
					MaxId = relationData.Values.Max(),
					Entries = relationData.Values.Select((x, i) => new { Index = (uint)i, Id = x }).ToDictionary(x => x.Index, x => BitConverter.GetBytes(x.Id))
				};

				relationData.Clear();
			}

			// Temporarily remove the fake records.
			if (HasIndexTable)
			{
				FieldStructure.RemoveAt(0);
				ColumnMeta.RemoveAt(0);
			}
			if (RelationShipData != null)
			{
				FieldStructure.RemoveAt(FieldStructure.Count - 1);
				ColumnMeta.RemoveAt(ColumnMeta.Count - 1);
			}

			// Remove any existing column values.
			ColumnMeta.ForEach(x => { x.PalletValues?.Clear(); x.SparseValues?.Clear(); });

			// RecordData - this can still all be done via one function, except inline strings.
			BitStream bitStream = new BitStream(entry.Data.Rows.Count * ColumnMeta.Max(x => x.RecordOffset));
			for (int rowIndex = 0; rowIndex < entry.Data.Rows.Count; rowIndex++)
			{
				var rowData = new Queue<object>(entry.Data.Rows[rowIndex].ItemArray);

				int id = entry.Data.Rows[rowIndex].Field<int>(entry.Key);
				bool isCopyRecord = copyIdsSet.Contains(id);

				if (HasIndexTable) // dump the id from the row data
				{
					rowData.Dequeue();
				}

				bitStream.SeekNextOffset(); // each row starts at a 0 bit position
				long offset = pos + bitStream.Offset; // used for offset map calcs
				for (int fieldIndex = 0; fieldIndex < FieldCount; fieldIndex++)
				{
					int bitWidth = ColumnMeta[fieldIndex].BitWidth;
					int bitSize = FieldStructure[fieldIndex].BitCount;
					int arraySize = ColumnMeta[fieldIndex].ArraySize;

					// Get the values for the current record, array size may require more than 1.
					object[] values = ExtractFields(rowData, stringTable, bitStream, fieldIndex, out bool isString);
					byte[][] data = values.Select(x => (byte[])BitConverter.GetBytes((dynamic)x)).ToArray(); // shameful hack

					if (data.Length == 0)
					{
						continue;
					}

					CompressionType compression = ColumnMeta[fieldIndex].CompressionType;

					// Copy records still store the sparse data.
					if (isCopyRecord && compression != CompressionType.Sparse)
					{
						continue;
					}

					switch (compression)
					{
						case CompressionType.None:
							for (int i = 0; i < arraySize; i++)
								bitStream.WriteBits(data[i], bitSize);
							break;

						case CompressionType.Immediate:
						case CompressionType.SignedImmediate:
							bitStream.WriteBits(data[0], bitWidth);
							break;

						case CompressionType.Sparse:
							{
								Array.Resize(ref data[0], 4);
								if (BitConverter.ToInt32(data[0], 0) != ColumnMeta[fieldIndex].BitOffset)
								{
									ColumnMeta[fieldIndex].SparseValues.Add(id, data[0]);
								}
							}
							break;

						case CompressionType.Pallet:
						case CompressionType.PalletArray:
							{
								byte[] combined = data.SelectMany(x => x.Concat(new byte[4]).Take(4)).ToArray(); // enforce int size rule

								int index = ColumnMeta[fieldIndex].PalletValues.FindIndex(x => x.SequenceEqual(combined));
								if (index > -1)
								{
									bitStream.WriteUInt32((uint)index, bitWidth);
								}
								else
								{
									bitStream.WriteUInt32((uint)ColumnMeta[fieldIndex].PalletValues.Count, bitWidth);
									ColumnMeta[fieldIndex].PalletValues.Add(combined);
								}
							}
							break;

						default:
							throw new Exception("Unsupported compression type " + ColumnMeta[fieldIndex].CompressionType);

					}
				}

				// Copy records aren't real rows so skip the padding
				if (isCopyRecord)
				{
					continue;
				}

				bitStream.SeekNextOffset();
				short size = (short)(pos + bitStream.Offset - offset);

				// Matches itemsparse padding.
				if (isSparse)
				{
					int remaining = size % 8 == 0 ? 0 : 8 - size % 8;
					if (remaining > 0)
					{
						size += (short)remaining;
						bitStream.WriteBytes(new byte[remaining], remaining);
					}

					offsetMap.Add(new Tuple<int, short>((int)offset, size));
				}

				// Needs to be padded to the record size regardless of the byte count - weird eh?
				else
				{
					if (size < RecordSize)
					{
						bitStream.WriteBytes(new byte[RecordSize - size], RecordSize - size);
					}
				}
			}

			// Write to the filestream.
			bitStream.CopyStreamTo(writer.BaseStream);
			bitStream.Dispose();

			// OffsetTable / StringTable, either or.
			if (isSparse)
			{
				// OffsetTable.
				OffsetTableOffset = (int)writer.BaseStream.Position;
				WriteOffsetMap(writer, entry, offsetMap);
				offsetMap.Clear();
			}
			else
			{
				// StringTable.
				StringBlockSize = (uint)stringTable.Size;
				stringTable.CopyTo(writer.BaseStream);
				stringTable.Dispose();
			}

			// IndexTable.
			if (HasIndexTable)
			{
				pos = writer.BaseStream.Position;
				WriteIndexTable(writer, entry);
				IndexSize = (int)(writer.BaseStream.Position - pos);
			}

			// Copytable.
			if (CopyTableSize > 0)
			{
				pos = writer.BaseStream.Position;
				foreach (KeyValuePair<int, IEnumerable<int>> copyRecord in copyRecords)
				{
					foreach (int val in copyRecord.Value)
					{
						writer.Write(val);
						writer.Write(copyRecord.Key);
					}
				}
				CopyTableSize = (int)(writer.BaseStream.Position - pos);
				copyRecords.Clear();
				copyIdsSet.Clear();
			}

			// ColumnMeta.
			pos = writer.BaseStream.Position;
			foreach (ColumnStructureEntry meta in ColumnMeta)
			{
				writer.Write(meta.RecordOffset);
				writer.Write(meta.Size);

				if (meta.SparseValues != null)
				{
					writer.Write((uint)meta.SparseValues.Count * 8); // (k<4>, v<4>)
				}
				else if (meta.PalletValues != null)
				{
					writer.Write((uint)meta.PalletValues.Sum(x => x.Length));
				}
				else
				{
					writer.WriteUInt32(0);
				}

				writer.Write((uint)meta.CompressionType);
				writer.Write(meta.BitOffset);
				writer.Write(meta.BitWidth);
				writer.Write(meta.Cardinality);
			}
			ColumnMetadataSize = (int)(writer.BaseStream.Position - pos);

			// Pallet values.
			pos = writer.BaseStream.Position;
			foreach (ColumnStructureEntry meta in ColumnMeta)
			{
				if (meta.CompressionType == CompressionType.Pallet || meta.CompressionType == CompressionType.PalletArray)
				{
					writer.WriteArray(meta.PalletValues.SelectMany(x => x).ToArray());
				}

			}
			PalletDataSize = (int)(writer.BaseStream.Position - pos);

			// Sparse values.
			pos = writer.BaseStream.Position;
			foreach (ColumnStructureEntry meta in ColumnMeta)
			{
				if (meta.CompressionType == CompressionType.Sparse)
				{
					foreach (var sparse in meta.SparseValues)
					{
						writer.Write(sparse.Key);
						writer.WriteArray(sparse.Value);
					}
				}
			}
			SparseDataSize = (int)(writer.BaseStream.Position - pos);

			// Relationships.
			pos = writer.BaseStream.Position;
			if (RelationShipData != null)
			{
				writer.Write(RelationShipData.Records);
				writer.Write(RelationShipData.MinId);
				writer.Write(RelationShipData.MaxId);

				foreach (KeyValuePair<uint, byte[]> relation in RelationShipData.Entries)
				{
					writer.Write(relation.Value);
					writer.Write(relation.Key);
				}
			}
			RelationshipDataSize = (int)(writer.BaseStream.Position - pos);

			// Update header fields.
			writer.BaseStream.Position = 16;
			writer.Write(StringBlockSize);
			writer.BaseStream.Position = 40;
			writer.Write(CopyTableSize);
			writer.BaseStream.Position = 60;
			writer.Write(OffsetTableOffset);
			writer.Write(IndexSize);
			writer.Write(ColumnMetadataSize);
			writer.Write(SparseDataSize);
			writer.Write(PalletDataSize);
			writer.Write(RelationshipDataSize);

			// Reset indextable stuff.
			if (HasIndexTable)
			{
				FieldStructure.Insert(0, new FieldStructureEntry(0, 0));
				ColumnMeta.Insert(0, new ColumnStructureEntry());
			}
			if (RelationShipData != null)
			{
				FieldStructure.Add(new FieldStructureEntry(0, 0));
				ColumnMeta.Add(new ColumnStructureEntry());
			}
		}

		protected object[] ExtractFields(Queue<object> rowData, StringTable stringTable, BitStream bitStream, int fieldIndex, out bool isString)
		{
			object[] values = Enumerable.Range(0, ColumnMeta[fieldIndex].ArraySize).Select(x => rowData.Dequeue()).ToArray();
			isString = false;

			// Deal with strings.
			if (values.Any(x => x.GetType() == typeof(string)))
			{
				isString = true;

				if (HasIndexTable && HasOffsetTable)
				{
					foreach (object s in values)
					{
						bitStream.WriteCString((string)s);
					}

					return new object[0];
				}
				else
				{
					for (int i = 0; i < values.Length; i++)
					{
						values[i] = stringTable.Write((string)values[i], false, false);
					}
				}
			}

			return values;
		}

		#endregion


		protected void RemoveBitLimits()
		{
			if (!HasOffsetTable)
			{
				int c = HasIndexTable ? 1 : 0;
				int cm = ColumnMeta.Count - (RelationShipData != null ? 1 : 0);

				var skipType = new HashSet<CompressionType>(new[] { CompressionType.None, CompressionType.Sparse });

				for (int i = c; i < cm; i++)
				{
					ColumnStructureEntry col = ColumnMeta[i];
					CompressionType type = col.CompressionType;
					int oldsize = col.BitWidth;
					ushort newsize = (ushort)(columnSizes[c] * 8);

					c += col.ArraySize;
					if (skipType.Contains(col.CompressionType) || newsize == oldsize)
					{
						continue;
					}

					col.BitWidth = col.Size = newsize;
					for (int x = i + 1; x < cm; x++)
					{
						if (skipType.Contains(ColumnMeta[x].CompressionType))
						{
							continue;
						}

						ColumnMeta[x].RecordOffset += (ushort)(newsize - oldsize);
						ColumnMeta[x].BitOffset = ColumnMeta[x].RecordOffset - PackedDataOffset * 8;
					}
				}

				RecordSize = (uint)((ColumnMeta.Sum(x => x.Size) + 7) / 8);
			}
		}
	}

	public class MinMax
	{
		public object MinVal;
		public object MaxVal;
		public bool Signed;
		public bool IsSingle;
	}
}
