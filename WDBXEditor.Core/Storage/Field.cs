using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml.Serialization;

namespace WDBXEditor.Core.Storage
{
	[Serializable]
	public class Field
	{
		[XmlAttribute]
		public string Name { get; set; }
		[XmlAttribute]
		public string Type { get; set; }
		[XmlAttribute, DefaultValue(1)]
		public int ArraySize { get; set; } = 1;
		[XmlAttribute, DefaultValue(false)]
		public bool IsIndex { get; set; } = false;
		[XmlAttribute, DefaultValue(false)]
		public bool AutoGenerate { get; set; } = false;
		[XmlAttribute, DefaultValue("")]
		public string DefaultValue { get; set; } = "";
		[XmlAttribute, DefaultValue("")]
		public string ColumnNames { get; set; } = "";
		[XmlIgnore]
		public string InternalName { get; set; }
	}
}
