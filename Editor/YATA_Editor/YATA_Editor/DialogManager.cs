using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using System.Drawing;


namespace YATA_Editor
{
	enum DOCUMENT_TYPE
	{
		DOCUMENT_TYPE_SCRIPT,
		DOCUMENT_TYPE_IMAGE
	}


	class DialogManager
	{
		public string ScriptName { get; set; } = "";
		public string ScriptDirectory { get; set; } = "";
		public string Filename { get; set; }

		private bool Dialog()
		{
			SaveFileDialog saveDialog = new SaveFileDialog()
			{
				FileName = "file.bmp",
				Filter = "Image Files (*.bmp) | *.bmp|All files(*.*) | *.*"
			};
			Nullable<bool> result = saveDialog.ShowDialog();
			Filename = saveDialog.FileName;
			return (bool)result;
		}

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

		public void SaveScript(string text, string path = null)
		{
			string saveTo = Filename;
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
			if (Dialog())
				SaveScript(text);
		}
		public void OpenSaveDialog(Bitmap image)
		{
			if (Dialog())
				image.Save(Filename);
		}
	}
}
