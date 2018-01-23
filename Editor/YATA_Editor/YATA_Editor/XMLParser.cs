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
	class XMLParser
	{
		public Configuration Parse(string filename)
		{
			XmlSerializer serializer = new
			XmlSerializer(typeof(Configuration));

			using (FileStream fs = new FileStream(filename, FileMode.Open))
			{
				XmlReader reader = XmlReader.Create(fs);
				Configuration config = (Configuration)serializer.Deserialize(reader);
				fs.Close();
				return config;
			}
		}
	}
}
