using Acmil.Core.Archives.CASC.Handlers;
using Acmil.Core.Archives.MPQ;
using Acmil.Core.Storage;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using WDBXEditor.Data.Contracts.IO.Enums;
using static Acmil.Core.Common.Constants;

namespace Acmil.Core.ConsoleHandler
{
	/// <summary>
	/// Class containing implementations for console commands.
	/// </summary>
	public class ConsoleCommands
	{
		#region Load

		/// <summary>
		/// Loads a file into the console
		/// <para>load -f "*.dbc" -s ".mpq/wow dir" -b 11802</para>
		/// </summary>
		/// <param name="args"></param>
		/// 
		public static void LoadCommand(string[] args)
		{
			Dictionary<string, string> pmap = ConsoleManager.ParseCommand(args);
			string file = ParamCheck<string>(pmap, "-f");
			string filename = Path.GetFileName(file);
			string filenoext = Path.GetFileNameWithoutExtension(file);
			string source = ParamCheck<string>(pmap, "-s", false);
			int build = ParamCheck<int>(pmap, "-b");
			SourceType sType = GetSourceType(source);

			// Check file exists if loaded from the filesystem.
			if (!File.Exists(file) && sType == SourceType.File)
			{
				throw new Exception($"   File not found '{file}'.");
			}

			// Check the required definition exists.
			var def = Database.Definitions.Tables.FirstOrDefault(x => x.Build == build && x.Name.Equals(filenoext, IGNORECASE));
			if (def == null)
			{
				throw new Exception($"   Could not find definition for {Path.GetFileName(file)} build {build}.");
			}

			Database.BuildNumber = build;
			var fileStreams = new ConcurrentDictionary<string, MemoryStream>();
			string error = string.Empty;

			switch (sType)
			{
				case SourceType.MPQ:
					Console.WriteLine("Loading from MPQ archive...");
					using (var archive = new MpqArchive(source, FileAccess.Read))
					{
						string line = string.Empty;
						bool loop = true;
						using (MpqFileStream listfile = archive.OpenFile("(listfile)"))
						using (var sr = new StreamReader(listfile))
						{
							while ((line = sr.ReadLine()) != null && loop)
							{
								if (line.EndsWith(filename, IGNORECASE))
								{
									loop = false;
									var ms = new MemoryStream();
									archive.OpenFile(line).CopyTo(ms);
									fileStreams.TryAdd(filename, ms);

									error = Database.LoadFiles(fileStreams).Result.FirstOrDefault();
								}
							}
						}
					}
					break;
				case SourceType.CASC:
					Console.WriteLine("Loading from CASC directory...");
					using (var casc = new CASCHandler(source))
					{
						string fullname = filename;
						if (!fullname.StartsWith("DBFilesClient", IGNORECASE))
						{
							fullname = "DBFilesClient\\" + filename; //Ensure we have the current file name structure
						}

						var stream = casc.ReadFile(fullname);
						if (stream != null)
						{
							fileStreams.TryAdd(filename, stream);
							error = Database.LoadFiles(fileStreams).Result.FirstOrDefault();
						}
					}
					break;
				default:
					error = Database.LoadFiles(new string[] { file }).Result.FirstOrDefault();
					break;
			}

			fileStreams.Clear();

			if (!string.IsNullOrWhiteSpace(error))
			{
				throw new Exception("   " + error);
			}

			if (Database.Entries.Count == 0)
			{
				throw new Exception("   File could not be loaded.");
			}

			Console.WriteLine($"{Path.GetFileName(file)} loaded.");
			Console.WriteLine("");
		}

		public static void ExtractCommand(string[] args)
		{
			Dictionary<string, string> pmap = ConsoleManager.ParseCommand(args);
			string filter = ParamCheck<string>(pmap, "-f", false);
			string source = ParamCheck<string>(pmap, "-s");
			string output = ParamCheck<string>(pmap, "-o");
			SourceType sType = GetSourceType(source);

			if (string.IsNullOrWhiteSpace(filter))
				filter = "*";

			string regexfilter = "(" + Regex.Escape(filter).Replace(@"\*", @".*").Replace(@"\?", ".") + ")";
			Func<string, bool> TypeCheck = t => Path.GetExtension(t).ToLower() == ".dbc" || Path.GetExtension(t).ToLower() == ".db2";

			var fileStreams = new ConcurrentDictionary<string, MemoryStream>();
			switch (sType)
			{
				case SourceType.MPQ:
					Console.WriteLine("Loading from MPQ archive...");
					using (var archive = new MpqArchive(source, FileAccess.Read))
					{
						string line = string.Empty;
						using (MpqFileStream listfile = archive.OpenFile("(listfile)"))
						using (var sr = new StreamReader(listfile))
						{
							while ((line = sr.ReadLine()) != null)
							{
								if (TypeCheck(line) && Regex.IsMatch(line, regexfilter, RegexOptions.Compiled | RegexOptions.IgnoreCase))
								{
									var ms = new MemoryStream();
									archive.OpenFile(line).CopyTo(ms);
									fileStreams.TryAdd(Path.GetFileName(line), ms);
								}
							}
						}
					}
					break;
				case SourceType.CASC:
					Console.WriteLine("Loading from CASC directory...");
					using (var casc = new CASCHandler(source))
					{
						var fileNames = ClientDBFileNames.Where(x => Regex.IsMatch(Path.GetFileName(x), regexfilter, RegexOptions.Compiled | RegexOptions.IgnoreCase));
						foreach (string fileName in fileNames)
						{
							var stream = casc.ReadFile(fileName);
							if (stream != null)
							{
								fileStreams.TryAdd(Path.GetFileName(fileName), stream);
							}

						}
					}
					break;
			}

			if (fileStreams.Count == 0)
			{
				throw new Exception("   No matching files found.");
			}

			if (!Directory.Exists(output))
			{
				Directory.CreateDirectory(output);
			}

			foreach (KeyValuePair<string, MemoryStream> d in fileStreams)
			{
				using (var fs = new FileStream(Path.Combine(output, d.Key), FileMode.Create))
				{
					fs.Write(d.Value.ToArray(), 0, (int)d.Value.Length);
					fs.Close();
				}
			}

			fileStreams.Clear();

			Console.WriteLine($"   Successfully extracted files.");
			Console.WriteLine("");
		}
		#endregion

		#region Export

		/// <summary>
		/// Exports a file to either SQL, JSON or CSV
		/// <para>-export -f "*.dbc" -s ".mpq/wow dir" -b 11802 -o "*.sql|*.csv"</para>
		/// </summary>
		/// <param name="args"></param>
		public static void ExportArgCommand(string[] args)
		{
			Dictionary<string, string> pmap = ConsoleManager.ParseCommand(args);
			string output = ParamCheck<string>(pmap, "-o");
			OutputType oType = GetOutputType(output);

			LoadCommand(args);

			DBEntry entry = Database.Entries[0];
			using (var fs = new FileStream(output, FileMode.Create))
			{
				byte[] data = new byte[0];
				switch (oType)
				{
					case OutputType.CSV:
						data = Encoding.UTF8.GetBytes(entry.ToCSV());
						break;
					case OutputType.JSON:
						data = Encoding.UTF8.GetBytes(entry.ToJSON());
						break;
					case OutputType.SQL:
						data = Encoding.UTF8.GetBytes(entry.ToSQL());
						break;
				}

				fs.Write(data, 0, data.Length);

				Console.WriteLine($"Successfully exported to {output}.");
			}
		}

		#endregion

		#region SQL Dump

		/// <summary>
		/// Exports a file directly into a SQL database
		/// <para>-sqldump -f "*.dbc" -s ".mpq/wow dir" -b 11802 -c "Server=myServerAddress;Database=myDataBase;Uid=myUsername;Pwd=myPassword;"</para>
		/// </summary>
		/// <param name="args"></param>
		public static void SqlDumpArgCommand(string[] args)
		{
			//Dictionary<string, string> pmap = ConsoleManager.ParseCommand(args);
			//string connection = ParamCheck<string>(pmap, "-c");

			//LoadCommand(args);

			//DBEntry entry = Database.Entries[0];
			//entry.ToSQLTable();

			// TODO: Figure out how to do this post-refactor.
			//Console.WriteLine($"Successfully exported to {conn.Database}.");
		}

		#endregion

		#region SQL Import

		/// <summary>
		/// Imports a DBC file from a SQL database table.
		/// <para>-sqlimport -f "*.dbc" -b 11802 -c "Server=myServerAddress;Database=myDataBase;Uid=myUsername;Pwd=myPassword;" -t "table_name" -u "insert/update/replace" -w "true"</para>
		/// </summary>
		/// <param name="args"></param>
		public static void SqlImportArgCommand(string[] args)
		{
			//Dictionary<string, string> pmap = ConsoleManager.ParseCommand(args);
			//string filePath = ParamCheck<string>(pmap, "-f", required: true);
			//string connectionString = ParamCheck<string>(pmap, "-c");
			//UpdateMode updateMode = ParamCheck<UpdateMode>(pmap, "-u");
			//string tableName = ParamCheck<string>(pmap, "-t");
			//bool writeChangesToFile = ParamCheck<bool>(pmap, "-w");

			//LoadCommand(args);

			//DBEntry entry = Database.Entries[0];
			//using (var conn = new MySqlConnection(connectionString))
			//{
			//	try
			//	{
			//		conn.Open();
			//	}
			//	catch (Exception ex)
			//	{
			//		string debugMessage = $"Error opening SQL connection: {ex.Message}";
			//		Debug.WriteLine(debugMessage, "Error");

			//		// Just going to assume this is why we're throwing cause that's
			//		// what we're doing everywhere else (AAAAAAAAAAAAAAAAAAAAAAAAAAAAA).
			//		throw new Exception("   Incorrect MySQL login details.");
			//	}

			//	// TODO: Eventually add support for multiple files through an MPQ or CASC file.
			//	string errorMessage;
			//	if (!entry.ImportSQL(updateMode, tableName, out errorMessage))
			//	{
			//		throw new Exception("Error importing data from SQL", new IOException(errorMessage));
			//	}

			//	Console.WriteLine($"'{tableName}' successfully imported from {conn.Database}.");

			//	if (writeChangesToFile)
			//	{
			//		try
			//		{
			//			new DBReader().Write(entry, filePath);
			//		}
			//		catch (Exception ex)
			//		{
			//			throw new Exception($"Error writing changes to file '{filePath}'", ex);
			//		}

			//		Console.WriteLine($"Changes successfully written to '{filePath}'.");
			//	}
			//}
		}

		#endregion

		#region Helpers

		private static T ParamCheck<T>(Dictionary<string, string> map, string field, bool required = true)
		{
			T retVal = default;
			if (map.ContainsKey(field))
			{
				try
				{
					if (typeof(T).IsEnum)
					{
						retVal = (T)Enum.Parse(typeof(T), map[field], true);
					}
					else
					{
						retVal = (T)Convert.ChangeType(map[field], typeof(T));
					}
				}
				catch
				{
					if (required)
					{
						throw new Exception($"   Parameter {field} is invalid");
					}
				}
			}
			else if (required)
			{
				throw new Exception($"   Missing parameter '{field}'");
			}
			else
			{
				object defaultval = typeof(T) == typeof(string) ? string.Empty : 0;
				retVal = (T)Convert.ChangeType(defaultval, typeof(T));
			}

			return retVal;
		}

		private static SourceType GetSourceType(string source)
		{
			if (string.IsNullOrWhiteSpace(source)) //No source
				return SourceType.File;

			string extension = Path.GetExtension(source).ToLower().TrimStart('.');

			if (File.Exists(source) && extension == "mpq") //MPQ
				return SourceType.MPQ;
			else if (Directory.Exists(source)) //CASC
				return SourceType.CASC;

			throw new Exception($"   Invalid source selected. Options are .MPQ, WoW Directory or blank.");
		}

		private static OutputType GetOutputType(string output)
		{
			string extension = Path.GetExtension(output).ToLower();
			switch (extension)
			{
				case ".csv":
					return OutputType.CSV;
				case ".sql":
					return OutputType.SQL;
				case ".json":
					return OutputType.JSON;
			}

			throw new Exception("   Invalid output type. Options are CSV, JSON or SQL.");
		}


		internal enum SourceType
		{
			File,
			MPQ,
			CASC
		}

		internal enum OutputType
		{
			CSV,
			SQL,
			MPQ,
			JSON
		}
		#endregion
	}
}
