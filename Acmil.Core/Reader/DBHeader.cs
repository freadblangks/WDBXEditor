﻿using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System;
using Acmil.Core.Reader.FileTypes;
using Acmil.Core.Reader.Enums;
using Acmil.Core.Storage;

namespace Acmil.Core.Reader
{
	public class DBHeader
	{
		//Standard Fields
		public string Signature { get; set; }
		public uint RecordCount { get; set; }
		public uint UnknownWCH7 { get; set; } = 0;
		public uint FieldCount { get; set; }
		public uint RecordSize { get; set; }
		public uint StringBlockSize { get; set; }

		public uint TableHash { get; set; }
		public int LayoutHash { get; set; }
		public int MinId { get; set; }
		public int MaxId { get; set; }
		public int Locale { get; set; }
		public int CopyTableSize { get; set; } = 0;
		public HeaderFlags Flags { get; set; } = HeaderFlags.None;
		public List<FieldStructureEntry> FieldStructure { get; set; } = new List<FieldStructureEntry>();
		public Table TableStructure { get; set; }

		public ushort IdIndex { get; set; } = 0;
		public uint TotalFieldSize { get; set; } = 0;
		public uint CommonDataTableSize { get; set; } = 0;
		public uint InternalRecordSize { get; set; }

		public int AutoGeneratedColumns { get; set; } = 0;
		public int[] OffsetLengths { get; set; } = new int[0];
		public int HeaderSize = 0x20;
		public int StringTableOffset = 0x10;

		public bool IsTypeOf<T>() => this is T;
		public bool IsLegionFile => this is WDB5 || this is WCH5 || this is WCH7 || this is WCH8;
		public bool IsValidFile => IsTypeOf<WDB>() || IsTypeOf<WDB2>() || IsTypeOf<WDBC>() || IsTypeOf<HTFX>() || IsLegionFile;

		public virtual bool ExtendedStringTable => false;
		public virtual bool HasIndexTable => false;
		public virtual bool HasOffsetTable => false;
		public virtual bool HasRelationshipData => false;

		public virtual bool CheckRecordCount => true;
		public virtual bool CheckRecordSize => true;
		public virtual bool CheckTableStructure => true;

		public Dictionary<int, int> OffsetDuplicates = new Dictionary<int, int>();


		#region Read Functions

		public virtual void ReadHeader(ref BinaryReader dbReader, string signature)
		{
			Signature = signature;
			RecordCount = dbReader.ReadUInt32();

			if (IsTypeOf<WCH7>())
				UnknownWCH7 = dbReader.ReadUInt32();

			FieldCount = dbReader.ReadUInt32();
			RecordSize = dbReader.ReadUInt32();
			StringBlockSize = dbReader.ReadUInt32();
		}

		public virtual byte[] ReadData(BinaryReader dbReader, long pos) => new byte[0];

		public virtual int GetStringOffset(BinaryReader dbReader, int j, uint i) => dbReader.ReadInt32();

		#endregion

		#region Write Functions

		public virtual void WriteHeader(BinaryWriter bw, DBEntry entry)
		{
			//Signature
			bw.Write(Encoding.UTF8.GetBytes(Signature));

			//Record count
			if (IsTypeOf<WDB5>() && !(this as WDB5).HasOffsetTable && entry.Header.CopyTableSize > 0)
				bw.Write(entry.GetUniqueRows().Count());
			else
				bw.Write(entry.Data.Rows.Count);

			//WCH7 specific field
			if (IsTypeOf<WCH7>())
				bw.Write(UnknownWCH7);

			//FieldCount
			if (IsTypeOf<WDB5>() && !IsTypeOf<WDC1>())
				bw.Write(((WDB5)this).HasIndexTable ? FieldCount - 1 : FieldCount); //Index Table
			else
				bw.Write(FieldCount);

			//Record size
			bw.Write(RecordSize);

			//StringBlockSize placeholder
			if (IsTypeOf<WDB5>() || IsTypeOf<WCH7>())
				bw.Write((uint)2);
			else
				bw.Write((uint)1);
		}

		public virtual void WriteOffsetMap(BinaryWriter bw, DBEntry entry, List<Tuple<int, short>> OffsetMap, int record_offset = 0) { }

		public virtual void WriteIndexTable(BinaryWriter bw, DBEntry entry) { }

		public virtual void WriteRecordPadding(BinaryWriter bw, DBEntry entry, long offset)
		{
			if (bw.BaseStream.Position - offset < RecordSize)
				bw.BaseStream.Position += RecordSize - (bw.BaseStream.Position - offset);
		}

		#endregion

		public virtual void Clear() { }
	}
}