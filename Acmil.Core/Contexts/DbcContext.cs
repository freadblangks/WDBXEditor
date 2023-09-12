using Acmil.Common.Utility.Interfaces;
using Acmil.Common.Utility.Logging.Interfaces;
using Acmil.Core.Contexts.Interfaces;
using Acmil.Core.Storage;
using Acmil.Data.Contexts;
using Acmil.Data.Contracts.Connections;
using Acmil.Data.Helpers.Interfaces;
using System.Collections.Concurrent;
using System.Collections;
using System.Threading.Tasks.Dataflow;
using Acmil.Core.Reader;
using static Acmil.Core.Common.Constants;
using System.Data;
using Acmil.Common.Utility.GarbageCollection;
using Acmil.Data.Contracts.IO.Enums;
using Acmil.Core.Exceptions;
using Table = Acmil.Core.Storage.Table;
using Acmil.Core.Common.Enums;
using Acmil.Core.Common;
using System.Linq;

namespace Acmil.Core.Contexts
{
	/// <summary>
	/// A context for interacting with DBC files.
	/// </summary>
	public class DbcContext : IDbcContext
	{
		private IUtilityHelper _utilityHelper;
		private ILogger _logger;
		private IDbContextFactory _dbContextFactory;

		private static Database _processDbcDatabase;

		/// <summary>
		/// Initializes a new instance of <see cref="DbcContext"/>.
		/// </summary>
		/// <param name="utilityHelper">An implementation of <see cref="IUtilityHelper"/>.</param>
		/// <param name="dbContextFactory">An implementation of <see cref="IDbContext"/>.</param>
		public DbcContext(IUtilityHelper utilityHelper, IDbContextFactory dbContextFactory)
		{
			_utilityHelper = utilityHelper;
			_logger = utilityHelper.Logger;
			_dbContextFactory = dbContextFactory;

			_processDbcDatabase ??= GetInitializedDatabase();
		}

		public void LoadDbcIntoSql(MySqlConnectionInfo connectionInfo, string database, string dbcPath, string tableName = null)
		{
			var dbContext = _dbContextFactory.GetContext(connectionInfo, database);

			// TODO: Add proper error handling here.
			LoadFiles(new string[] { dbcPath }).Result.FirstOrDefault();

			if (!_processDbcDatabase.Entries.Any())
			{
				throw new IOException($"Failed to load file '{dbcPath}'");
			}

			_logger.LogInformation($"Successfully loaded file '{Path.GetFileName(dbcPath)}'.");
			DBEntry entry = _processDbcDatabase.GetDbEntry(Path.GetFileNameWithoutExtension(dbcPath));  // TODO: This should probably not work this way.

			if (entry is null)
			{
				throw new DbcReadException($"No entry found loaded for DBC at path '{dbcPath}'.");
			}
			entry.ImportDbcIntoDatabaseAsTable(dbContext, tableName);
		}

		public void LoadDbcDataFromSql(MySqlConnectionInfo connectionInfo, string database, string dbcDirectoryPath, string dbcName, string tableName)
		{
			var dbContext = _dbContextFactory.GetContext(connectionInfo, database);
			var dbEntry = _processDbcDatabase.GetDbEntry(dbcName);
			if (dbEntry is null)
			{
				LoadFileIntoDbcDatabase(Path.Combine(dbcDirectoryPath, $"{dbcName}.dbc"));
				dbEntry = _processDbcDatabase.GetDbEntry(dbcName);
			}

			ReadDbcDataFromSql(dbContext, dbEntry, UpdateMode.Replace, tableName);
		}

		public void WriteLoadedDbcToPath(string dbcDirectoryPath, string dbcName)
		{
			var dbEntry = _processDbcDatabase.GetDbEntry(dbcName);
			if (dbEntry is null)
			{
				throw new DbcWriteException($"No DBC with name '{dbcName}' has been loaded.");
			}

			WriteDbcEntriesToPathAsync(new List<DBEntry>() { dbEntry }, dbcDirectoryPath).ConfigureAwait(false).GetAwaiter().GetResult();
		}

		internal enum ErrorType
		{
			Warning,
			Error
		}

		private Database GetInitializedDatabase()
		{
			var database = new Database();
			LoadDefinitionsIntoDatabase(database);

			return database;
		}

		#region Load

		private async Task<List<string>> LoadFiles(IEnumerable<string> filenames)
		{
			var errors = new ConcurrentBag<string>();
			var files = new ConcurrentQueue<string>(filenames.Distinct().OrderBy(x => x).ThenByDescending(x => Path.GetExtension(x)));
			string firstFile = files.First();

			var batchBlock = new BatchBlock<string>(100, new GroupingDataflowBlockOptions { BoundedCapacity = 100 });
			var actionBlock = new ActionBlock<string[]>(t =>
			{
				for (int i = 0; i < t.Length; ++i)
				{
					files.TryDequeue(out string filepath);
					errors.Concat(LoadFileIntoDbcDatabase(filepath));
				}

				GarbageCollectionHelper.ForceGC();
			});
			batchBlock.LinkTo(actionBlock, new DataflowLinkOptions { PropagateCompletion = true });

			foreach (string file in files)
			{
				await batchBlock.SendAsync(file); // wait synchronously for the block to accept.
			}

			batchBlock.Complete();
			await actionBlock.Completion;

			files = null;
			return errors.ToList();
		}

		private ConcurrentBag<string> LoadFileIntoDbcDatabase(string filepath)
		{
			var errors = new ConcurrentBag<string>();
			try
			{
				var reader = new DBReader(_utilityHelper);
				DBEntry entry = reader.Read(GetTableStructureForFile(filepath), filepath);
				if (entry is not null)
				{
					var current = _processDbcDatabase.Entries.FirstOrDefault(x => x.FileName == entry.FileName && x.Build == entry.Build);
					if (current is not null)
					{
						_processDbcDatabase.Entries.Remove(current);
					}

					_processDbcDatabase.Entries.Add(entry);
					//if (file != firstFile)
					//    entry.Detach();

					if (!string.IsNullOrWhiteSpace(reader.ErrorMessage))
					{
						errors.Add(FormatError(filepath, ErrorType.Warning, reader.ErrorMessage));
					}
				}
			}
			catch (ConstraintException)
			{
				errors.Add(FormatError(filepath, ErrorType.Error, "Id column contains duplicates."));
			}
			catch (Exception ex)
			{
				errors.Add(FormatError(filepath, ErrorType.Error, ex.Message));
			}

			return errors;
		}

		private async Task<List<string>> LoadFiles(ConcurrentDictionary<string, MemoryStream> streams)
		{
			var errors = new List<string>();
			var files = new Queue<KeyValuePair<string, MemoryStream>>(streams);

			var batchBlock = new BatchBlock<KeyValuePair<string, MemoryStream>>(75, new GroupingDataflowBlockOptions { BoundedCapacity = 100 });
			var actionBlock = new ActionBlock<KeyValuePair<string, MemoryStream>[]>(t =>
			{
				for (int i = 0; i < t.Length; ++i)
				{
					var s = files.Dequeue();
					try
					{
						var reader = new DBReader(_utilityHelper);
						DBEntry entry = reader.Read(s.Value, GetTableStructureForFile(s.Key), s.Key);
						if (entry is not null)
						{
							var current = _processDbcDatabase.Entries.FirstOrDefault(x => x.FileName == entry.FileName && x.Build == entry.Build);
							if (current is not null)
							{
								_processDbcDatabase.Entries.Remove(current);
							}

							_processDbcDatabase.Entries.Add(entry);

							if (!string.IsNullOrWhiteSpace(reader.ErrorMessage))
							{
								errors.Add(FormatError(s.Key, ErrorType.Warning, reader.ErrorMessage));
							}
						}
					}
					catch (ConstraintException)
					{
						errors.Add(FormatError(s.Key, ErrorType.Error, "Id column contains duplicates."));
					}
					catch (Exception ex)
					{
						errors.Add(FormatError(s.Key, ErrorType.Error, ex.Message));
					}

					if (i % 100 == 0 && i > 0)
					{
						GarbageCollectionHelper.ForceGC();
					}
				}

				GarbageCollectionHelper.ForceGC();
			});
			batchBlock.LinkTo(actionBlock, new DataflowLinkOptions { PropagateCompletion = true });

			foreach (KeyValuePair<string, MemoryStream> stream in streams)
			{
				// Wait synchronously for the block to accept.
				await batchBlock.SendAsync(stream);
			}

			batchBlock.Complete();
			await actionBlock.Completion;

			GarbageCollectionHelper.ForceGC();

			return errors;
		}

		#endregion

		#region Save

		private void ReadDbcDataFromSql(IDbContext dbContext, DBEntry dbEntry, UpdateMode updateMode, string tableName, string columns = "*")
		{
			// TODO: Determine 2 things:
			// 1. Do we need to enforce the schema?
			// 2. Do we need to explicitly allow DB null on all the columns?
			try
			{
				DataTable importTable = null;
				string sql = $"SELECT {columns} FROM `{tableName}`";
				importTable = dbContext.ExecuteSqlStatementAsDataTable(sql);

				// Replace DBNulls with default value.
				object[] defaultVals = importTable.Columns.Cast<DataColumn>().Select(x => x.DefaultValue).ToArray();
				Parallel.For(0, importTable.Rows.Count, r =>
				{
					for (int i = 0; i < importTable.Columns.Count; ++i)
					{
						if (importTable.Rows[r][i] == DBNull.Value)
						{
							importTable.Rows[r][i] = defaultVals[i];
						}
					}
				});

				switch (dbEntry.Data.ShallowCompare(importTable))
				{
					// TODO: Determine how this could ever happen.
					case CompareResult.DBNull:
						throw new SqlTableReadException("Table data contains NULL values.");
					case CompareResult.Type:
						throw new SqlTableReadException("Table data has incorrect column types.");
					case CompareResult.Count:
						throw new SqlTableReadException("Table data has an incorrect number of columns.");
					default:
						dbEntry.UpdateData(importTable, updateMode);
						break;
				}
			}
			catch (Exception ex)
			{
				throw new SqlTableReadException("Error encountered reading from SQL table.", ex);
			}

			#region Old Code (Consider Deleting Eventually)

			//error = "";
			//bool dbReadSuccessful;
			//DataTable importTable = null; /* = Data.Clone(); // Clone table structure to help with mapping.*/
			////Parallel.For(0, importTable.Columns.Count, c => importTable.Columns[c].AllowDBNull = true); // Allow null values

			////using (var connection = new MySqlConnection(connectionstring))
			////using (var command = new MySqlCommand($"SELECT {columns} FROM `{table}`", connection))
			////using (var adapter = new MySqlDataAdapter(command))
			//{
			//	try
			//	{
			//		string sql = $"SELECT {columns} FROM `{table}`";
			//		importTable = dbContext.ExecuteSqlStatementAsDataTable(sql);
			//		//adapter.FillSchema(importTable, SchemaType.Source); //Enforce schema
			//		//adapter.Fill(importTable);
			//		dbReadSuccessful = true;
			//	}

			//	// TODO: Figure out what we need to do to have this hit now that we're calling the IDbContext method.
			//	// I think it's if you read a file with duplicate IDs.
			//	catch (ConstraintException ex)
			//	{
			//		error = ex.Message;
			//		dbReadSuccessful = false;
			//	}
			//	catch (Exception ex)
			//	{
			//		_logger.LogError(ex, "");
			//		dbReadSuccessful = false;
			//	}
			//}

			//if (dbReadSuccessful)
			//{
			//	// Replace DBNulls with default value.
			//	object[] defaultVals = importTable.Columns.Cast<DataColumn>().Select(x => x.DefaultValue).ToArray();
			//	Parallel.For(0, importTable.Rows.Count, r =>
			//	{
			//		for (int i = 0; i < importTable.Columns.Count; ++i)
			//		{
			//			if (importTable.Rows[r][i] == DBNull.Value)
			//			{
			//				importTable.Rows[r][i] = defaultVals[i];
			//			}
			//		}
			//	});

			//	switch (Data.ShallowCompare(importTable))
			//	{
			//		// TODO: Determine how this could ever happen.
			//		case CompareResult.DBNull:
			//			error = "Table data contains NULL values.";
			//			dbReadSuccessful = false;
			//			break;
			//		case CompareResult.Type:
			//			error = "Table data has incorrect column types.";
			//			dbReadSuccessful = false;
			//			break;
			//		case CompareResult.Count:
			//			error = "Table data has an incorrect number of columns.";
			//			dbReadSuccessful = false;
			//			break;
			//		default:
			//			UpdateData(importTable, mode);
			//			break;
			//	}

			//	//if (!ValidateMinMaxValues(importTable, out error))
			//	//	return false;
			//}

			//return dbReadSuccessful;

			#endregion
		}

		private async Task WriteDbcEntriesToPathAsync(List<DBEntry> entries, string path)
		{
			var errors = new List<DbcWriteException>();
			var entryQueue = new Queue<DBEntry>(entries);

			var batchBlock = new BatchBlock<int>(100, new GroupingDataflowBlockOptions { BoundedCapacity = 100 });
			var actionBlock = new ActionBlock<int[]>(t =>
			{
				for (int i = 0; i < t.Length; ++i)
				{
					DBEntry entry = entryQueue.Dequeue();
					try
					{
						new DBReader(_utilityHelper).Write(entry, Path.Combine(path, entry.FileName));
					}
					catch (Exception ex)
					{
						errors.Add(new DbcWriteException($"Error writing entry '{entry.EntryName}'.", ex));
					}
				}

				GarbageCollectionHelper.ForceGC();
			});
			batchBlock.LinkTo(actionBlock, new DataflowLinkOptions { PropagateCompletion = true });

			foreach (int i in Enumerable.Range(0, _processDbcDatabase.Entries.Count))
			{
				// Wait synchronously for the block to accept.
				await batchBlock.SendAsync(i);
			}

			batchBlock.Complete();
			await actionBlock.Completion;

			if (errors.Count > 0)
			{
				Exception exception;
				if (errors.Count > 1)
				{
					exception = new AggregateException(errors);
				}
				else
				{
					exception = errors[0];
				}
				_logger.LogError(exception, "Error(s) writing DBCs to disk");
			}
		}

		#endregion

		#region Definitions

		private void LoadDefinitionsIntoDatabase(Database database)
		{
			// If this doesn't work, look at https://stackoverflow.com/questions/2041000/loop-through-all-the-resources-in-a-resx-file
			// or https://stackoverflow.com/questions/3032592/resources-at-class-library-project
			// to see if that helps.
			var definitionResourceSet = Resources.Definitions.ResourceManager.GetResourceSet(System.Globalization.CultureInfo.InvariantCulture, true, false);
			foreach (DictionaryEntry definitionResource in definitionResourceSet)
			{
				if (definitionResource.Value is string)
				{
					database.LoadDefinition((string)definitionResource.Value);
				}
			}

			//await Task.Factory.StartNew(() =>
			//{
			//	var definitionResourceSet = Resources.Definitions.ResourceManager.GetResourceSet(System.Globalization.CultureInfo.InvariantCulture, true, false);
			//	foreach (DictionaryEntry definitionResource in definitionResourceSet)
			//	{
			//		database.LoadDefinition((string)definitionResource.Value);
			//	}
			//	//string[] Assembly.GetExecutingAssembly().GetManifestResourceNames();
			//	//var resourceManager = new ResourceManager(typeof(Resources.Definitions));
			//	//var resourceSet = resourceManager.GetResourceSet(System.Globalization.CultureInfo.InvariantCulture, false, false);

			//	//Definitions.LoadDefinition(filePath);
			//	//foreach (string filePath in Directory.GetFiles(DEFINITION_DIR, "*.xml"))
			//	//{
			//	//	Definitions.LoadDefinition(filePath);
			//	//}

			//});
		}

		#endregion

		private Table GetTableStructureForFile(string filepath)
		{
			return _processDbcDatabase.Definitions.Tables.FirstOrDefault(x =>
				x.Name.Equals(Path.GetFileNameWithoutExtension(filepath), IGNORECASE) &&
				x.Build == _processDbcDatabase.BuildNumber);
		}

		private static string FormatError(string f, ErrorType t, string s)
		{
			return $"{t.ToString().ToUpper()} {Path.GetFileName(f)} : {s}";
		}
	}
}
