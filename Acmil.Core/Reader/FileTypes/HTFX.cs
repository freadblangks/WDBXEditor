using Acmil.Core.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Acmil.Common.Utility;
using Acmil.Common.Utility.Interfaces;

namespace Acmil.Core.Reader.FileTypes
{
	public class HTFX : DBHeader
	{
		private IUtilityHelper _utilityHelper;

		public int Build { get; set; }
		public byte[] Hashes { get; set; }
		public List<HotfixEntry> Entries { get; private set; } = new List<HotfixEntry>();

		public override bool CheckRecordCount => false;
		public override bool CheckRecordSize => false;
		public override bool CheckTableStructure => false;

		public WDB6 WDB6CounterPart { get; private set; }

		public HTFX(IUtilityHelper utilityHelper)
		{
			_utilityHelper = utilityHelper;
		}

		public override void ReadHeader(ref BinaryReader dbReader, string signature)
		{
			Signature = signature;
			Locale = dbReader.ReadInt32();
			Build = dbReader.ReadInt32();

			string tempHeader = dbReader.ReadString(4);
			dbReader.BaseStream.Position -= 4;

			if (tempHeader != "XFTH")
			{
				Hashes = dbReader.ReadBytes(32);
			}

			while (dbReader.BaseStream.Position < dbReader.BaseStream.Length)
			{
				Entries.Add(new HotfixEntry(dbReader));
			}

			Entries.RemoveAll(x => x.IsValid != 1); //Remove old hotfix entries
		}

		public bool HasEntry(DBHeader counterpart) => Entries.Any(x => (x.Locale == counterpart.Locale || x.Locale == 0) && x.TableHash == counterpart.TableHash && x.IsValid == 1);

		public bool Read(DBHeader counterpart, DBEntry dbentry)
		{
			bool retVal = false;
			WDB6CounterPart = counterpart as WDB6;
			if (WDB6CounterPart != null)
			{
				var entries = Entries.Where(x => (x.Locale == counterpart.Locale || x.Locale == 0) && x.TableHash == counterpart.TableHash);
				if (entries.Any())
				{
					OffsetLengths = entries.Select(x => (int)x.Size + 4).ToArray();
					TableStructure = WDB6CounterPart.TableStructure;
					Flags = WDB6CounterPart.Flags;
					FieldStructure = WDB6CounterPart.FieldStructure;
					RecordCount = (uint)entries.Count();

					dbentry.LoadTableStructure();

					IEnumerable<byte> Data = new byte[0];
					foreach (HotfixEntry hotfixEntry in entries)
					{
						Data = Data.Concat(BitConverter.GetBytes(hotfixEntry.RowId)).Concat(hotfixEntry.Data);
					}

					using (var memoryStream = new MemoryStream(Data.ToArray()))
					using (var binaryReader = new BinaryReader(memoryStream))
					{
						// TODO: Refactor this to not new up a UtilityHelper.
						new DBReader(_utilityHelper).ReadIntoTable(ref dbentry, binaryReader, new Dictionary<int, string>());
					}

					retVal = true;
				}
			}

			return retVal;
		}
	}

	public class HotfixEntry
	{
		public uint Signature;
		public uint Locale;
		public uint PushId;
		public uint Size;
		public uint TableHash;
		public int RowId;
		public byte IsValid;
		public byte[] Padding;
		public byte[] Data;

		public HotfixEntry(BinaryReader reader)
		{
			Signature = reader.ReadUInt32();
			Locale = reader.ReadUInt32();
			PushId = reader.ReadUInt32();
			Size = reader.ReadUInt32();
			TableHash = reader.ReadUInt32();
			RowId = reader.ReadInt32();
			IsValid = reader.ReadByte();
			Padding = reader.ReadBytes(3);

			Data = reader.ReadBytes((int)Size);
		}
	}
}
