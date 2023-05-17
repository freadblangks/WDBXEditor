using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Data;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Collections;
using Acmil.Core.Reader;
using Acmil.Common.Utility;
using Acmil.Common.Utility.Interfaces;
using Acmil.Common.Utility.Logging.Interfaces;

namespace Acmil.Core.Storage
{
	public class Database
	{
		public static Definition Definitions { get; set; } = new Definition();
		public static List<DBEntry> Entries { get; set; } = new List<DBEntry>();
		public static int BuildNumber { get; set; }

		private static readonly ILogger _logger;
		private static readonly IUtilityHelper _utilityHelper;

		// We should refactor this to be passed in with DI.
		static Database()
		{
			_utilityHelper = new UtilityHelper();
			_logger = _utilityHelper.GetLogger();
		}

		#region Load

		internal enum ErrorType
		{
			Warning,
			Error
		}

		private static string FormatError(string f, ErrorType t, string s)
		{
			return $"{t.ToString().ToUpper()} {Path.GetFileName(f)} : {s}";
		}

		public static async Task<List<string>> LoadFiles(IEnumerable<string> filenames)
		{
			var errors = new ConcurrentBag<string>();
			var files = new ConcurrentQueue<string>(filenames.Distinct().OrderBy(x => x).ThenByDescending(x => Path.GetExtension(x)));
			string firstFile = files.First();

			var batchBlock = new BatchBlock<string>(100, new GroupingDataflowBlockOptions { BoundedCapacity = 100 });
			var actionBlock = new ActionBlock<string[]>(t =>
			{
				for (int i = 0; i < t.Length; i++)
				{
					files.TryDequeue(out string file);
					try
					{
						var reader = new DBReader(_utilityHelper);
						DBEntry entry = reader.Read(file);
						if (entry != null)
						{
							var current = Entries.FirstOrDefault(x => x.FileName == entry.FileName && x.Build == entry.Build);
							if (current != null)
							{
								Entries.Remove(current);
							}

							Entries.Add(entry);
							//if (file != firstFile)
							//    entry.Detach();

							if (!string.IsNullOrWhiteSpace(reader.ErrorMessage))
							{
								errors.Add(FormatError(file, ErrorType.Warning, reader.ErrorMessage));
							}
						}
					}
					catch (ConstraintException)
					{
						errors.Add(FormatError(file, ErrorType.Error, "Id column contains duplicates."));
					}
					catch (Exception ex)
					{
						errors.Add(FormatError(file, ErrorType.Error, ex.Message));
					}
				}

				ForceGC();
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

		public static async Task<List<string>> LoadFiles(ConcurrentDictionary<string, MemoryStream> streams)
		{
			var errors = new List<string>();
			var files = new Queue<KeyValuePair<string, MemoryStream>>(streams);

			var batchBlock = new BatchBlock<KeyValuePair<string, MemoryStream>>(75, new GroupingDataflowBlockOptions { BoundedCapacity = 100 });
			var actionBlock = new ActionBlock<KeyValuePair<string, MemoryStream>[]>(t =>
			{
				for (int i = 0; i < t.Length; i++)
				{
					var s = files.Dequeue();
					try
					{
						var reader = new DBReader(_utilityHelper);
						DBEntry entry = reader.Read(s.Value, s.Key);
						if (entry != null)
						{
							var current = Entries.FirstOrDefault(x => x.FileName == entry.FileName && x.Build == entry.Build);
							if (current != null)
								Entries.Remove(current);

							Entries.Add(entry);

							if (!string.IsNullOrWhiteSpace(reader.ErrorMessage))
								errors.Add(FormatError(s.Key, ErrorType.Warning, reader.ErrorMessage));
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
						ForceGC();
					}
				}

				ForceGC();
			});
			batchBlock.LinkTo(actionBlock, new DataflowLinkOptions { PropagateCompletion = true });

			foreach (KeyValuePair<string, MemoryStream> stream in streams)
			{
				// Wait synchronously for the block to accept.
				await batchBlock.SendAsync(stream);
			}

			batchBlock.Complete();
			await actionBlock.Completion;

			ForceGC();

			return errors;
		}

		#endregion

		#region Save

		public static async Task<List<string>> SaveFiles(string path)
		{
			var errors = new List<string>();
			var files = new Queue<DBEntry>(Entries);

			var batchBlock = new BatchBlock<int>(100, new GroupingDataflowBlockOptions { BoundedCapacity = 100 });
			var actionBlock = new ActionBlock<int[]>(t =>
			{
				for (int i = 0; i < t.Length; i++)
				{
					DBEntry file = files.Dequeue();
					try
					{
						new DBReader(_utilityHelper).Write(file, Path.Combine(path, file.FileName));
					}
					catch (Exception ex)
					{
						errors.Add($"{file} : {ex.Message}");
					}
				}

				ForceGC();
			});
			batchBlock.LinkTo(actionBlock, new DataflowLinkOptions { PropagateCompletion = true });

			foreach (int i in Enumerable.Range(0, Entries.Count))
			{
				// Wait synchronously for the block to accept.
				await batchBlock.SendAsync(i);
			}

			batchBlock.Complete();
			await actionBlock.Completion;

			return errors;
		}

		#endregion

		#region Definitions

		public static async Task LoadDefinitions()
		{
			// If this doesn't work, look at https://stackoverflow.com/questions/2041000/loop-through-all-the-resources-in-a-resx-file
			// or https://stackoverflow.com/questions/3032592/resources-at-class-library-project
			// to see if that helps.
			await Task.Factory.StartNew(() =>
			{
				var definitionResourceSet = Resources.Definitions.ResourceManager.GetResourceSet(System.Globalization.CultureInfo.InvariantCulture, false, false);
				foreach (DictionaryEntry definitionResource in definitionResourceSet)
				{
					Definitions.LoadDefinition((string)definitionResource.Value);
				}
				//string[] Assembly.GetExecutingAssembly().GetManifestResourceNames();
				//var resourceManager = new ResourceManager(typeof(Resources.Definitions));
				//var resourceSet = resourceManager.GetResourceSet(System.Globalization.CultureInfo.InvariantCulture, false, false);

				//Definitions.LoadDefinition(filePath);
				//foreach (string filePath in Directory.GetFiles(DEFINITION_DIR, "*.xml"))
				//{
				//	Definitions.LoadDefinition(filePath);
				//}

			});
		}

		#endregion

		public static void ForceGC()
		{
			GC.Collect();
			GC.WaitForFullGCComplete();

#if DEBUG
			Debug.WriteLine((GC.GetTotalMemory(false) / 1024 / 1024).ToString() + "mb");
#endif
		}
	}
}
