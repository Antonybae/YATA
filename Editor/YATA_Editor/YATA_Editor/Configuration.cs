using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace YATA_Editor
{
	[XmlRoot("Configuration")]
	public struct Configuration
	{
		[XmlElement("SaveFolder")]
		public string SaveFolder { get; set; }
		[XmlElement("ScriptDictionary")]
		public List<ScriptDictionary> scriptDictionaryList;
	}
	public struct ScriptDictionary
	{
		[XmlElement("Name")]
		public string Name { get; set; }
		[XmlElement("Value")]
		public string Value { get; set; }
		[XmlElement("Icon")]
		public string Icon { get; set; }
	}
}
