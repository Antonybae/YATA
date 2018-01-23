using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YATA_Editor
{
	class OperationsManager
	{
		public Configuration configuration { get; set; }

		public OperationsManager()
		{
			GetConfiguration();
		}

		private void GetConfiguration()
		{
			XMLParser parser = new XMLParser();
			configuration = parser.Parse("Configuration.xml");
		}
	}
}
