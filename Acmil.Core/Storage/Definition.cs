using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static Acmil.Core.Common.Constants;

namespace Acmil.Core.Storage
{
	[Serializable]
	public class Definition
	{
		[XmlElement("Table")]
		public HashSet<Table> Tables { get; set; } = new HashSet<Table>();

		[XmlIgnore]
		public int Build { get; set; }

		[XmlIgnore]
		private bool _loading = false;

		public bool LoadDefinition(string definitionText)
		{
			bool result = false;
			if (!_loading)
			{
				try
				{
					using (var reader = new StringReader(definitionText))
					{
						var deserializer = new XmlSerializer(typeof(Definition));
						var definition = (Definition)deserializer.Deserialize(reader);

						// If this runs into issues, use the commented out code below.
						var newTables = definition.Tables.Where(newTable => !Tables.Any(existingTable => newTable.Build == existingTable.Build && newTable.Name == existingTable.Name)).ToList();
						newTables.ForEach(t => t.Load());
						Tables.UnionWith(newTables.Where(t => t.Key is not null));

						result = true;
					}
				}
				catch (Exception ex)
				{
					// TODO: Add logging here.
				}
			}
			else
			{
				result = true;
			}

			return result;
		}

		//public bool LoadDefinition(string path)
		//{
		//	if (_loading) return true;

		//	try
		//	{
		//		var deser = new XmlSerializer(typeof(Definition));
		//		using (var fs = new FileStream(path, FileMode.Open))
		//		{
		//			Definition def = (Definition)deser.Deserialize(fs);
		//			var newtables = def.Tables.Where(x => Tables.Count(y => x.Build == y.Build && x.Name == y.Name) == 0).ToList();
		//			newtables.ForEach(x => x.Load());
		//			Tables.UnionWith(newtables.Where(x => x.Key != null));
		//			return true;
		//		}
		//	}
		//	catch
		//	{
		//		return false;
		//	}
		//}

		//public bool SaveDefinitions()
		//{
		//	try
		//	{
		//		_loading = true;

		//		List<IGrouping<int, Table>> builds = Tables.OrderBy(x => x.Name).GroupBy(x => x.Build).ToList();
		//		Tables.Clear();
		//		foreach (var build in builds)
		//		{
		//			var _def = new Definition
		//			{
		//				Build = build.Key,
		//				Tables = new HashSet<Table>(build)
		//			};

		//			var serializer = new XmlSerializer(typeof(Definition));
		//			using (var fs = new FileStream(Path.Combine(DEFINITION_DIR, ValidFilename(BuildText(build.Key))), FileMode.Create))
		//			{
		//				serializer.Serialize(fs, _def);
		//			}

		//		}

		//		_loading = false;
		//		return true;
		//	}
		//	catch (Exception)
		//	{
		//		_loading = false;
		//		return false;
		//	}
		//}

		//private string ValidFilename(string b)
		//{
		//	return string.Join("_", b.Split(Path.GetInvalidFileNameChars(), StringSplitOptions.RemoveEmptyEntries)).TrimEnd('.') + ".xml";
		//}
	}
}
