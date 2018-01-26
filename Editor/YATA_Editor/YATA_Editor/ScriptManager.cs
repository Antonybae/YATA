using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace YATA_Editor
{
	class ScriptManager
	{
		public OperationsManager operManager { get; }
		private DialogManager dialogs;
		private string generatedScriptText;

		public ScriptManager()
		{
			dialogs = new DialogManager();
			operManager = new OperationsManager();
		}

		public List<string[]> Open()
		{
			List<string[]> arguments = new List<string[]>();
			string[] data = dialogs.OpenFileDialog();

			foreach(string dataString in data)
			{
				var temp = dataString.Split(' ');
				string[] newData = { temp[0], CheckImages(temp[1],dialogs.ScriptName)};
				arguments.Add(newData);
			}
			return arguments;
		}

		private string CheckImages(string imagePath, string scriptPath)
		{
			if (!File.Exists(imagePath))
			{
				return dialogs.ScriptDirectory + "\\" + imagePath;
			}
			else
				return imagePath;
		}

		public void Save(bool saveAs = false)
		{
			if (dialogs.ScriptDirectory == "")
			{
				if (saveAs)
					dialogs.OpenSaveDialog(generatedScriptText);
			}
			else
				dialogs.SaveScript(generatedScriptText);
		}

		public void GenerateScriptText(List<string[]> OperaionsList)
		{
			foreach(string[] operation in OperaionsList)
			{
				generatedScriptText += operation[0] + " " + operation[1] + "\r\n";
			}
		}
	}
}
