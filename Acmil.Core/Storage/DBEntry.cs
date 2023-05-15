using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using static Acmil.Core.Common.Constants;
using System.Text.RegularExpressions;
using static Acmil.Core.Common.Extensions;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO.MemoryMappedFiles;
using CompressionType = Acmil.Core.Common.Enums.CompressionType;
using System.Text.Json;
using WDBXEditor.Common.Utility.Interfaces;
using WDBXEditor.Common.Utility.Logging.Interfaces;
using WDBXEditor.Common.Utility.Configuration.Interfaces;
using Acmil.Core.Reader.FileTypes;
using Acmil.Core.Common;
using Acmil.Core.Common.Enums;
using Acmil.Core.Reader;
using Acmil.Core.Exceptions;
using Acmil.Data.Contexts;
using Acmil.Data.Contracts.IO.Enums;

namespace Acmil.Core.Storage
{
	public class DBEntry : IDisposable
	{
		public DBHeader Header { get; private set; }
		public DataTable Data { get; set; }
		public bool Changed { get; set; } = false;
		public string FilePath { get; private set; }
		public string FileName => Path.GetFileName(FilePath);
		public string SavePath { get; set; }
		public Table TableStructure => Header.TableStructure;

		public string Key { get; private set; }
		public int Build { get; private set; }
		public string BuildName { get; private set; }
		public string Tag { get; private set; }


		// Temp table fields.
		private int _minId = -1;
		private int _maxId = -1;
		private IEnumerable<int> _unqiueRowIndices;
		private IEnumerable<int> _primaryKeys;

		private readonly ILogger _logger;
		private readonly IConfigurationManager _configManager;

		public DBEntry(IUtilityHelper utilityHelper, DBHeader header, string filepath)
		{
			_logger = utilityHelper.GetLogger();
			_configManager = utilityHelper.GetConfigurationManager();

			Header = header;
			FilePath = filepath;
			SavePath = filepath;
			Header.TableStructure = Database.Definitions.Tables.FirstOrDefault(x =>
				x.Name.Equals(Path.GetFileNameWithoutExtension(filepath), IGNORECASE) &&
				x.Build == Database.BuildNumber);

			LoadDefinition();
		}


		/// <summary>
		/// Converts the XML definition to an empty DataTable
		/// </summary>
		public void LoadDefinition()
		{
			if (TableStructure != null)
			{
				Build = TableStructure.Build;
				Key = TableStructure.Key.Name;
				BuildName = BuildText(Build);
				Tag = Guid.NewGuid().ToString();

				// Column name check.
				IGrouping<string, Field> duplicateColumns = TableStructure.Fields.GroupBy(x => x.Name).FirstOrDefault(y => y.Count() > 1);
				if (duplicateColumns != default)
				{
					string columnName = duplicateColumns.Key;
					throw new InvalidDefinitionException($"Duplicate column name '{columnName}' detected in {FileName} - {Build} definition.");
					//MessageBox.Show($"Duplicate column names for {FileName} - {Build} definition");
					//return;
				}
				LoadTableStructure();
			}
		}

		public void LoadTableStructure()
		{
			Data = new DataTable()
			{
				TableName = Tag,
				CaseSensitive = false,
				RemotingFormat = SerializationFormat.Binary
			};

			var LocalizationCount = Build <= (int)ExpansionFinalBuild.Classic ? 9 : 17; //Pre TBC had 9 locales

			foreach (var col in TableStructure.Fields)
			{
				var languages = new Queue<TextWowEnum>(Enum.GetValues(typeof(TextWowEnum)).Cast<TextWowEnum>());
				string[] columnsNames = col.ColumnNames.Split(',');

				for (int i = 0; i < col.ArraySize; i++)
				{
					string columnName = col.Name;
					if (col.ArraySize > 1)
					{
						if (columnsNames.Length >= i + 1 && !string.IsNullOrWhiteSpace(columnsNames[i]))
						{
							columnName = columnsNames[i];
						}
						else
						{
							columnName += "_" + (i + 1);
						}
					}

					col.InternalName = columnName;

					switch (col.Type.ToLower())
					{
						case "sbyte":
							Data.Columns.Add(columnName, typeof(sbyte));
							Data.Columns[columnName].DefaultValue = 0;
							break;
						case "byte":
							Data.Columns.Add(columnName, typeof(byte));
							Data.Columns[columnName].DefaultValue = 0;
							break;
						case "int32":
						case "int":
							Data.Columns.Add(columnName, typeof(int));
							Data.Columns[columnName].DefaultValue = 0;
							break;
						case "uint32":
						case "uint":
							Data.Columns.Add(columnName, typeof(uint));
							Data.Columns[columnName].DefaultValue = 0;
							break;
						case "int64":
						case "long":
							Data.Columns.Add(columnName, typeof(long));
							Data.Columns[columnName].DefaultValue = 0;
							break;
						case "uint64":
						case "ulong":
							Data.Columns.Add(columnName, typeof(ulong));
							Data.Columns[columnName].DefaultValue = 0;
							break;
						case "single":
						case "float":
							Data.Columns.Add(columnName, typeof(float));
							Data.Columns[columnName].DefaultValue = 0;
							break;
						case "boolean":
						case "bool":
							Data.Columns.Add(columnName, typeof(bool));
							Data.Columns[columnName].DefaultValue = 0;
							break;
						case "string":
							Data.Columns.Add(columnName, typeof(string));
							Data.Columns[columnName].DefaultValue = string.Empty;
							break;
						case "int16":
						case "short":
							Data.Columns.Add(columnName, typeof(short));
							Data.Columns[columnName].DefaultValue = 0;
							break;
						case "uint16":
						case "ushort":
							Data.Columns.Add(columnName, typeof(ushort));
							Data.Columns[columnName].DefaultValue = 0;
							break;
						case "loc":
							//Special case for localized strings, build up all locales and add string mask
							for (int x = 0; x < LocalizationCount; x++)
							{
								if (x == LocalizationCount - 1)
								{
									Data.Columns.Add(col.Name + "_Mask", typeof(uint)); //Last column is a mask
									Data.Columns[col.Name + "_Mask"].AllowDBNull = false;
									Data.Columns[col.Name + "_Mask"].DefaultValue = 0;
								}
								else
								{
									columnName = col.Name + "_" + languages.Dequeue().ToString(); //X columns for local strings
									Data.Columns.Add(columnName, typeof(string));
									Data.Columns[columnName].AllowDBNull = false;
									Data.Columns[columnName].DefaultValue = string.Empty;
								}
							}
							break;
						default:
							throw new Exception($"Unknown field type {col.Type} for {col.Name}.");
					}

					//AutoGenerated Id for CharBaseInfo
					if (col.AutoGenerate)
					{
						Data.Columns[0].ExtendedProperties.Add(AUTO_GENERATED, true);
						Header.AutoGeneratedColumns++;
					}

					Data.Columns[columnName].AllowDBNull = false;
				}
			}

			// Set up the Primary Key.
			Data.Columns[Key].DefaultValue = null; // Clear default value.
			Data.PrimaryKey = new DataColumn[] { Data.Columns[Key] };
			Data.Columns[Key].AutoIncrement = true;
			Data.Columns[Key].Unique = true;
		}

		public void Detach()
		{
			Data?.Detach(Path.Combine(TEMP_FOLDER, Tag + ".cache"));
			Data?.Clear();
			Data?.Dispose();
			Data = null;
		}

		public void Attach()
		{
			if (Data is null || Data.Rows.Count == 0)
			{
				using (var fs = new FileStream(Path.Combine(TEMP_FOLDER, Tag + ".cache"), FileMode.Open))
				using (MemoryMappedFile mmf = MemoryMappedFile.CreateFromFile(fs, Tag, fs.Length, MemoryMappedFileAccess.ReadWrite, HandleInheritability.None, false))
				using (MemoryMappedViewStream stream = mmf.CreateViewStream(0, fs.Length, MemoryMappedFileAccess.Read))
				{
					var formatter = new BinaryFormatter();
					Data = (DataTable)formatter.Deserialize(stream);
				}
			}
		}


		/// <summary>
		/// Checks if the file is of Name and Expansion
		/// </summary>
		/// <param name="filename"></param>
		/// <param name="expansion"></param>
		/// <returns></returns>
		/// 
		public bool IsFileOf(string filename, Expansion expansion)
		{
			return TableStructure.Name.Equals(filename, IGNORECASE) && IsBuild(Build, expansion);
		}

		public bool IsFileOf(string filename)
		{
			return TableStructure.Name.Equals(filename, IGNORECASE);
		}


		/// <summary>
		/// Generates a Bit map for all columns as the Blizzard one combines array columns
		/// </summary>
		/// <returns></returns>
		public FieldStructureEntry[] GetBits()
		{
			FieldStructureEntry[] retVal;
			if (!Header.IsTypeOf<WDB5>())
			{
				retVal = new FieldStructureEntry[Data.Columns.Count];
			}
			else
			{
				var bits = new List<FieldStructureEntry>();
				if (Header is WDC1 header)
				{
					List<ColumnStructureEntry> fields = header.ColumnMeta;
					for (int i = 0; i < fields.Count; i++)
					{
						short bitcount = (short)(Header.FieldStructure[i].BitCount == 64 ? Header.FieldStructure[i].BitCount : 0); // force bitcounts
						for (int x = 0; x < fields[i].ArraySize; x++)
						{
							bits.Add(new FieldStructureEntry(bitcount, 0));
						}
					}
				}
				else
				{
					List<FieldStructureEntry> fields = Header.FieldStructure;
					for (int i = 0; i < TableStructure.Fields.Count; i++)
					{
						Field f = TableStructure.Fields[i];
						for (int x = 0; x < f.ArraySize; x++)
						{
							bits.Add(new FieldStructureEntry(fields[i]?.Bits ?? 0, 0, fields[i]?.CommonDataType ?? 0xFF));
						}
					}
				}
				retVal = bits.ToArray();
			}

			return retVal;
		}

		public int[] GetPadding()
		{
			int[] padding = new int[Data.Columns.Count];

			var bytecounts = new Dictionary<Type, int>()
			{
				{ typeof(byte), 1 },
				{ typeof(short), 2 },
				{ typeof(ushort), 2 },
			};

			if (Header is WDC1 header)
			{

				int c = 0;

				foreach (var field in header.ColumnMeta)
				{
					Type type = Data.Columns[c].DataType;
					bool isneeded = field.CompressionType >= CompressionType.Sparse;

					if (bytecounts.ContainsKey(type) && isneeded)
					{
						for (int x = 0; x < field.ArraySize; x++)
						{
							padding[c++] = 4 - bytecounts[type];
						}
					}
					else
					{
						c += field.ArraySize;
					}
				}
			}

			return padding;
		}

		public void UpdateColumnTypes()
		{
			if (Header.IsTypeOf<WDB6>())
			{
				List<FieldStructureEntry> fields = ((WDB6)Header).FieldStructure;
				int c = 0;
				for (int i = 0; i < TableStructure.Fields.Count; i++)
				{
					int arraySize = TableStructure.Fields[i].ArraySize;

					if (!fields[i].CommonDataColumn)
					{
						c += arraySize;
						continue;
					}

					Type columnType;
					switch (fields[i].CommonDataType)
					{
						case 0:
							columnType = typeof(string);
							break;
						case 1:
							columnType = typeof(ushort);
							break;
						case 2:
							columnType = typeof(byte);
							break;
						case 3:
							columnType = typeof(float);
							break;
						case 4:
							columnType = typeof(int);
							break;
						default:
							c += arraySize;
							continue;
					}

					for (int x = 0; x < arraySize; x++)
					{
						Data.Columns[c++].DataType = columnType;
					}
				}
			}
		}


		#region Special Data

		/// <summary>
		/// Gets the Min and Max ids
		/// </summary>
		/// <returns></returns>
		public Tuple<int, int> MinMax()
		{
			if (_minId == -1 || _maxId == -1)
			{
				_minId = int.MaxValue;
				_maxId = int.MinValue;
				foreach (DataRow dr in Data.Rows)
				{
					int val = dr.Field<int>(Key);
					_minId = Math.Min(_minId, val);
					_maxId = Math.Max(_maxId, val);
				}
			}

			return new Tuple<int, int>(_minId, _maxId);
		}

		/// <summary>
		/// Gets the Primary Keys uniquely identifying rows in the DBEntry.
		/// </summary>
		/// <returns>The Primary Keys uniquely identifying rows in the DBEntry.</returns>
		public IEnumerable<int> GetPrimaryKeys()
		{
			if (_primaryKeys == null)
			{
				_primaryKeys = Data.AsEnumerable().Select(x => x.Field<int>(Key));
			}

			return _primaryKeys;
		}

		/// <summary>
		/// Produces a list of unique rows (excludes key values)
		/// </summary>
		/// <returns></returns>
		public IEnumerable<DataRow> GetUniqueRows()
		{
			if (_unqiueRowIndices == null)
			{
				DataTable tempTable = Data.Copy();
				tempTable.PrimaryKey = null;
				tempTable.Columns.Remove(Key);

				var comp = new ORowComparer();
				_unqiueRowIndices = tempTable.AsEnumerable()
								 .Select((t, i) => new ORow(i, t.ItemArray))
								 .Distinct(comp)
								 .Select(x => x.Index);
			}

			foreach (int u in _unqiueRowIndices)
			{
				yield return Data.Rows[u];
			}
		}

		/// <summary>
		/// Generates a map of unqiue rows and grouped count
		/// </summary>
		/// <returns></returns>
		public IEnumerable<IEnumerable<int>> GetCopyRows()
		{
			int[] primaryKeys = GetPrimaryKeys().ToArray();

			DataTable tempTable = Data.Copy();
			tempTable.PrimaryKey = null;
			tempTable.Columns.Remove(Key);

			var comp = new OArrayComparer();
			return tempTable.AsEnumerable()
					   .Select((Name, Index) => new { Name.ItemArray, Index })
					   .GroupBy(x => x.ItemArray, comp)
					   .Select(xg => xg.Select(x => primaryKeys[x.Index]))
					   .Where(x => x.Count() > 1);
		}

		/// <summary>
		/// Extracts the id and the total length of strings for each row
		/// </summary>
		/// <returns></returns>
		public Dictionary<int, short> GetStringLengths()
		{
			var result = new Dictionary<int, short>();
			IEnumerable<string> columnNames = Data.Columns.Cast<DataColumn>()
											  .Where(x => x.DataType == typeof(string))
											  .Select(x => x.ColumnName);

			foreach (DataRow row in Data.Rows)
			{
				short total = 0;
				foreach (string columnName in columnNames)
				{
					short len = (short)Encoding.UTF8.GetByteCount(row[columnName].ToString());
					total += (short)(len > 0 ? len + 1 : 0);
				}
				result.Add(row.Field<int>(Key), total);
			}

			return result;
		}

		public void ResetTemp()
		{
			_minId = -1;
			_maxId = -1;
			_unqiueRowIndices = null;
			_primaryKeys = null;
		}

		#endregion


		#region Exports
		/// <summary>
		/// Generates a SQL string to DROP and ADD a table then INSERT the records
		/// </summary>
		/// <returns></returns>
		public string ToSQL()
		{
			string tableName = $"db_{TableStructure.Name}_{Build}";

			StringBuilder sb = new StringBuilder();
			sb.AppendLine($"DROP TABLE IF EXISTS `{tableName}`; ");
			sb.AppendLine($"CREATE TABLE `{tableName}` ({Data.Columns.ToSql(Key)}) ENGINE=MyISAM DEFAULT CHARSET=utf8; ");
			foreach (DataRow row in Data.Rows)
				sb.AppendLine($"INSERT INTO `{tableName}` VALUES ({row.ToSql()}); ");

			return sb.ToString();
		}

		///// <summary>
		///// Uses MysqlBulkCopy to import the data directly into a database.
		///// </summary>
		///// <param name="connectionString">The connection string used to open the MySQL connection.</param>
		//public void ToSQLTable(string connectionString)
		//{
		//	using (var connection = new MySqlConnection(connectionString))
		//	{
		//		try
		//		{
		//			connection.Open();
		//		}
		//		catch (Exception ex)
		//		{
		//			string debugMessage = $"Error opening SQL connection: {ex.Message}";
		//			Debug.WriteLine(debugMessage, "Error");

		//			// Throwing a different exception message because I didn't write this code
		//			// and I think it might get displayed in the UI.
		//			throw new Exception("   Incorrect MySQL login details.");
		//		}

		//		ToSQLTable(connection);
		//	}
		//}


		/// <summary>
		/// Uses MysqlBulkCopy to import the data directly into a database.
		/// </summary>
		public void ToSQLTable(IDbContext dbContext)
		{
			string tableName = $"db_{TableStructure.Name}_{Build}";

			var sqlBuilder = new StringBuilder();
			sqlBuilder.AppendLine("SET SESSION sql_mode = 'NO_ENGINE_SUBSTITUTION';");
			sqlBuilder.AppendLine($"DROP TABLE IF EXISTS `{tableName}`; ");
			sqlBuilder.AppendLine($"CREATE TABLE `{tableName}` ({Data.Columns.ToSql(Key)}) ENGINE=MyISAM DEFAULT CHARACTER SET = utf8 COLLATE = utf8_unicode_ci; ");

			// This allows the user to get around the "secure_file_priv" setting in MySQL.
			// If a value has been provided for this appSetting, we load from that directory instead.
			string mySqlSecureFilePrivDirectory = GetMySqlSecureFilePrivSetting();
			string stagingDirectory = !string.IsNullOrWhiteSpace(mySqlSecureFilePrivDirectory) ? mySqlSecureFilePrivDirectory : TEMP_FOLDER;
			string csvName = Path.Combine(stagingDirectory, tableName + ".csv");

			// Write data to a temporary CSV file to be loaded into SQL.
			using (var csv = new StreamWriter(csvName))
			{
				csv.Write(ToCSV());
			}

			dbContext.ExecuteNonQuerySqlStatement(sqlBuilder.ToString());

			var bulkLoadSettings = new MySqlBulkLoadSettings()
			{
				TableName = $"`{tableName}`",
				FieldTerminator = ",",
				NumberOfLinesToSkip = 1,
				FilePath = csvName,
				FieldQuotationCharacter = '"',
				CharacterSet = "UTF8"
			};

			dbContext.ExecuteSqlBulkLoad(bulkLoadSettings);

			try
			{
				File.Delete(csvName);
			}
			catch (Exception ex)
			{
				string message = $"Error deleting temporary file '{csvName}'";
				_logger.LogWarning(ex, message);
			}
		}

		/// <summary>
		/// Generates a CSV file string
		/// </summary>
		/// <returns></returns>
		public string ToCSV()
		{
			Func<string, string> encodeCsv = s => { return string.Concat("\"", s.Replace(Environment.NewLine, string.Empty).Replace("\"", "\"\""), "\""); };

			var stringBuilder = new StringBuilder();
			IEnumerable<string> columnNames = Data.Columns.Cast<DataColumn>().Select(column => encodeCsv(column.ColumnName));
			stringBuilder.AppendLine(string.Join(",", columnNames));

			foreach (DataRow row in Data.Rows)
			{
				IEnumerable<string> fields = row.ItemArray.Select(field => encodeCsv(field.ToString()));
				stringBuilder.AppendLine(string.Join(",", fields));
			}

			return stringBuilder.ToString();
		}

		// TODO: Rewrite this to work without needing a UI.
		///// <summary>
		///// Appends to or creates a MPQ file
		///// <para>Picks the appropiate version based on the build number.</para>
		///// </summary>
		///// <param name="filename"></param>
		///// <param name="version"></param>
		//public void ToMPQ(string filename)
		//{
		//	MpqArchiveVersion version = MpqArchiveVersion.Version2;
		//	if (Build <= (int)ExpansionFinalBuild.WotLK)
		//	{
		//		version = MpqArchiveVersion.Version2;
		//	}
		//	else if (Build <= (int)ExpansionFinalBuild.MoP)
		//	{
		//		version = MpqArchiveVersion.Version4;
		//	}
		//	else
		//	{
		//		MessageBox.Show("Only clients before WoD support MPQ archives.");
		//		return;
		//	}

		//	try
		//	{
		//		MpqArchive archive = null;
		//		if (File.Exists(filename))
		//		{
		//			switch (ShowOverwriteDialog("You've selected an existing MPQ archive.\r\nWhich action would you like to take?", "Existing MPQ"))
		//			{
		//				case DialogResult.Yes: //Append
		//					archive = new MpqArchive(filename, FileAccess.Write);
		//					break;
		//				case DialogResult.No: //Overwrite
		//					archive = MpqArchive.CreateNew(filename, version);
		//					break;
		//				default:
		//					return;
		//			}
		//		}
		//		else
		//		{
		//			archive = MpqArchive.CreateNew(filename, version);
		//		}

		//		string tmpPath = Path.Combine(TEMP_FOLDER, TableStructure.Name);
		//		string fileName = Path.GetFileName(FilePath);
		//		string filePath = Path.Combine("DBFilesClient", fileName);

		//		new DBReader().Write(this, tmpPath);
		//		archive.AddFileFromDisk(tmpPath, filePath);

		//		int retval = archive.AddListFile(filePath);
		//		archive.Compact(filePath);
		//		archive.Flush();
		//		archive.Dispose();
		//	} //Save the file
		//	catch (Exception ex)
		//	{
		//		MessageBox.Show($"Error exporting to MPQ archive {ex.Message}");
		//	}
		//}

		/// <summary>
		/// Generates a JSON string
		/// </summary>
		/// <returns></returns>
		public string ToJSON()
		{
			string[] columns = Data.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
			var rows = new ConcurrentBag<Dictionary<string, object>>();
			Parallel.For(0, Data.Rows.Count, r =>
			{
				object[] data = Data.Rows[r].ItemArray;

				var row = new Dictionary<string, object>();
				for (int x = 0; x < columns.Length; x++)
				{
					row.Add(columns[x], data[x]);
				}

				rows.Add(row);
			});

			// TODO: Figure out an alternative to the max length setting here.
			return JsonSerializer.Serialize(rows);
			//return new JavaScriptSerializer() { MaxJsonLength = int.MaxValue }.Serialize(Rows);
		}

		#endregion


		#region Imports

		public bool ImportCSV(string filename, bool headerRow, UpdateMode mode, out string error, ImportFlags flags)
		{
			error = string.Empty;
			bool importSuccessful = false;

			// Clone table structure to help with mapping
			DataTable importTable = Data.Clone();

			var usedIds = new HashSet<int>();
			int idcolumn = Data.Columns[Key].Ordinal;
			int maxId = int.MinValue;

			string pathOnly = Path.GetDirectoryName(filename);
			string fileName = Path.GetFileName(filename);

			Func<string, string> unescape = s =>
			{
				if (s.StartsWith("\"") && s.EndsWith("\""))
				{
					s = s.Substring(1, s.Length - 2);
					if (s.Contains("\"\""))
					{
						s = s.Replace("\"\"", "\"");
					}
				}
				return s;
			};

			try
			{
				using (var sr = new StreamReader(File.OpenRead(filename)))
				{
					if (headerRow)
					{
						sr.ReadLine();
					}

					while (!sr.EndOfStream)
					{
						string line = sr.ReadLine();
						string[] rows = Regex.Split(line, ",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))", RegexOptions.Compiled);
						DataRow dr = importTable.NewRow();

						for (int i = 0; i < Data.Columns.Count; i++)
						{
							string value = unescape(rows[i]);
							switch (Data.Columns[i].DataType.Name.ToLower())
							{
								case "sbyte":
									dr[i] = Convert.ToSByte(value);
									break;
								case "byte":
									dr[i] = Convert.ToByte(value);
									break;
								case "int32":
								case "int":
									dr[i] = Convert.ToInt32(value);
									break;
								case "uint32":
								case "uint":
									dr[i] = Convert.ToUInt32(value);
									break;
								case "int64":
								case "long":
									dr[i] = Convert.ToInt64(value);
									break;
								case "uint64":
								case "ulong":
									dr[i] = Convert.ToUInt64(value);
									break;
								case "single":
								case "float":
									dr[i] = Convert.ToSingle(value);
									break;
								case "boolean":
								case "bool":
									dr[i] = Convert.ToBoolean(value);
									break;
								case "string":
									dr[i] = value;
									break;
								case "int16":
								case "short":
									dr[i] = Convert.ToInt16(value);
									break;
								case "uint16":
								case "ushort":
									dr[i] = Convert.ToUInt16(value);
									break;
							}

							// Double check our Ids.
							if (i == idcolumn)
							{
								int id = (int)dr[i];
								if (flags.HasFlag(ImportFlags.TakeNewest) && usedIds.Contains(id))
								{
									var prev = importTable.Rows.Find(id);
									if (prev != null)
									{
										importTable.Rows.Remove(prev);
									}
								}
								else if (flags.HasFlag(ImportFlags.FixIds) && usedIds.Contains(id))
								{
									dr[i] = ++maxId;
									id = (int)dr[i];
								}

								usedIds.Add(id); //Add to list
								maxId = Math.Max(maxId, id); // Update maxid
							}
						}

						importTable.Rows.Add(dr);
					}
				}

				importSuccessful = true;
			}
			catch (FormatException)
			{
				error = $"Mismatch of data to datatype in row index {usedIds.Count + 1}";
				importSuccessful = false;
			}
			catch (Exception ex)
			{
				error = ex.Message;
				importSuccessful = false;
			}

			if (importSuccessful)
			{
				switch (Data.ShallowCompare(importTable, false))
				{
					case CompareResult.Type:
						error = "Import Failed: Imported data has one or more incorrect column types.";
						importSuccessful = false;
						break;
					case CompareResult.Count:
						error = "Import Failed: Imported data has an incorrect number of columns.";
						importSuccessful = false;
						break;
					default:
						UpdateData(importTable, mode);
						break;
				}

				//if (!ValidateMinMaxValues(importTable, out error))
				//	return false;
			}

			return importSuccessful;
		}

		public bool ImportSQL(IDbContext dbContext, UpdateMode mode, string table, out string error, string columns = "*")
		{
			// TODO: Determine 2 things:
			// 1. Do we need to enforce the schema?
			// 2. Do we need to explicitly allow DB null on all the columns?

			error = "";
			bool importSuccessful;
			DataTable importTable = null; /* = Data.Clone(); // Clone table structure to help with mapping.*/
			//Parallel.For(0, importTable.Columns.Count, c => importTable.Columns[c].AllowDBNull = true); // Allow null values

			//using (var connection = new MySqlConnection(connectionstring))
			//using (var command = new MySqlCommand($"SELECT {columns} FROM `{table}`", connection))
			//using (var adapter = new MySqlDataAdapter(command))
			{
				try
				{
					string sql = $"SELECT {columns} FROM `{table}`";
					importTable = dbContext.ExecuteSqlStatementAsDataTable(sql);
					//adapter.FillSchema(importTable, SchemaType.Source); //Enforce schema
					//adapter.Fill(importTable);
					importSuccessful = true;
				}

				// TODO: Figure out what we need to do to have this hit now that we're calling the IDbContext method.
				catch (ConstraintException ex)
				{
					error = ex.Message;
					importSuccessful = false;
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "");
					importSuccessful = false;
				}
			}

			if (importSuccessful)
			{
				// Replace DBNulls with default value.
				object[] defaultVals = importTable.Columns.Cast<DataColumn>().Select(x => x.DefaultValue).ToArray();
				Parallel.For(0, importTable.Rows.Count, r =>
				{
					for (int i = 0; i < importTable.Columns.Count; i++)
					{
						if (importTable.Rows[r][i] == DBNull.Value)
						{
							importTable.Rows[r][i] = defaultVals[i];
						}
					}
				});

				switch (Data.ShallowCompare(importTable))
				{
					// TODO: Determine how this could ever happen.
					case CompareResult.DBNull:
						error = "Import Failed: Imported data contains NULL values.";
						importSuccessful = false;
						break;
					case CompareResult.Type:
						error = "Import Failed: Imported data has incorrect column types.";
						importSuccessful = false;
						break;
					case CompareResult.Count:
						error = "Import Failed: Imported data has an incorrect number of columns.";
						importSuccessful = false;
						break;
					default:
						UpdateData(importTable, mode);
						break;
				}

				//if (!ValidateMinMaxValues(importTable, out error))
				//	return false;
			}

			return importSuccessful;
		}

		private bool ValidateMinMaxValues(DataTable importTable, out string error)
		{
			error = "";

			//if (Header is WDC1 header)
			//{
			//	foreach (var minmax in header.MinMaxValues)
			//	{
			//		Func<dynamic, dynamic, dynamic, bool> compare = (x, min, max) => x < min || x > max;

			//		bool errored = false;

			//		var values = importTable.Rows.Cast<DataRow>().Select(x => x.ItemArray[minmax.Key]);
			//		if (minmax.Value.IsSingle)
			//		{
			//			errored = values.Any(x => compare((float)Convert.ChangeType(x, typeof(float)), minmax.Value.MinVal, minmax.Value.MaxVal));
			//		}
			//		else if (minmax.Value.Signed)
			//		{
			//			errored = values.Any(x => compare((long)Convert.ChangeType(x, typeof(long)), minmax.Value.MinVal, minmax.Value.MaxVal));
			//		}
			//		else
			//		{
			//			errored = values.Any(x => compare((ulong)Convert.ChangeType(x, typeof(ulong)), minmax.Value.MinVal, minmax.Value.MaxVal));
			//		}

			//		if (errored)
			//		{
			//			error = $"Import Failed: Imported data has out of range values for {Data.Columns[minmax.Key].ColumnName}.\n" +
			//					$"(Min: {minmax.Value.MinVal}, Max: {minmax.Value.MaxVal})";

			//			return false;
			//		}
			//	}
			//}

			return true;
		}

		private void UpdateData(DataTable importTable, UpdateMode mode)
		{
			switch (mode)
			{
				case UpdateMode.Insert:

					// Insert all rows where the ID doesn't already exist already into the existing datatable.
					IEnumerable<object[]> rows = Data.Except(importTable, Key);
					DataTable source = Data.Copy();

					source.BeginLoadData();
					foreach (object[] r in rows)
					{
						source.Rows.Add(r);
					}

					source.EndLoadData();

					Data.Clear();
					Data = source;

					break;

				case UpdateMode.Replace:

					// Simply change the datatable.
					Data = importTable.Copy();
					break;

				case UpdateMode.Update:

					// Insert all the missing existing rows into the new dataset then change the datatable
					IEnumerable<object[]> rows2 = importTable.Except(Data, Key);

					importTable.BeginLoadData();
					foreach (object[] r in rows2)
						importTable.Rows.Add(r);
					importTable.EndLoadData();

					Data = importTable.Copy();
					break;
			}

			Parallel.For(0, Data.Columns.Count, c => Data.Columns[c].AllowDBNull = false); //Disallow null values

			importTable.Clear();
			importTable.Dispose();
			Database.ForceGC();
		}

		#endregion


		public void Dispose()
		{
			Data?.Dispose();
			Data = null;
		}

		private string GetMySqlSecureFilePrivSetting()
		{
			return _configManager.GetAppSettings().MySql.SecureFilePrivDirectoryAbsolutePath;
		}
	}
}
