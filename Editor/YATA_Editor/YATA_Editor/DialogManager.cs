using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using Microsoft.Win32;

namespace YATA_Editor
{
	class DialogManager
	{
		public string ScriptName { get; set; } = "";
		public string ScriptDirectory { get; set; } = "";

		public string[] OpenFileDialog()
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.FileName = "Script";
			openFileDialog.DefaultExt = ".txt";
			openFileDialog.Filter = "Scripts (.txt)|*.txt";

			string[] data = null;
			if (openFileDialog.ShowDialog() == true || ScriptName != "")
				ScriptName = openFileDialog.FileName;
			ScriptDirectory = Path.GetDirectoryName(ScriptName);

			data = File.ReadAllLines(ScriptName);
			return data;
		}

		public void Save(string text, string path = null)
		{
			string saveTo = ScriptDirectory;
			if (path != null)
			{
				saveTo = path;
			}
			using (StreamWriter writer = new StreamWriter(saveTo))
			{
				writer.Write(text);
				writer.Close();
			}
		}

		public void OpenSaveDialog(string text)
		{
			SaveFileDialog saveDialog = new SaveFileDialog
			{
				FileName = "Script.txt",
				Filter = "Script File | *.txt"
			};
			Nullable<bool> result = saveDialog.ShowDialog();

			if (result == true)
			{
				Save(text, saveDialog.FileName);
			}
		}
	}
}
