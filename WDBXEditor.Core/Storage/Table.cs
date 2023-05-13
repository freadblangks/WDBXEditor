using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using static WDBXEditor.Core.Common.Constants;


namespace WDBXEditor.Core.Storage
{
	[Serializable]
	public class Table
	{
		[XmlAttribute]
		public string Name { get; set; }
		[XmlAttribute]
		public int Build { get; set; }
		[XmlElement("Field")]
		public List<Field> Fields { get; set; }
		[XmlIgnore]
		public Field Key { get; private set; }
		[XmlIgnore]
		public bool Changed { get; set; } = false;
		[XmlIgnore]
		public string BuildText { get; private set; }

		public void Load()
		{
			Key = Fields.FirstOrDefault(x => x.IsIndex);
			BuildText = BuildText(Build);
		}
	}
}
