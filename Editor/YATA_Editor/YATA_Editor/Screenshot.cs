using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows;
using System.IO;

namespace YATA_Editor
{
	class Screenshot : IDisposable
	{
		public Bitmap _image { get; set; }
		int _width;
		int _height;
		public string _filename { get; set; }
		private string previousFilename { get; set; }

		public Screenshot(int width, int height)
		{
			_width = width;
			_height = height;
			Update();
		}

		public void Update()
		{
			Graphics graph = null;
			_image = new Bitmap(_width, _height);
			
			graph = Graphics.FromImage(_image);
			graph.CopyFromScreen(0, 0, 0, 0, _image.Size);

			Save();
		}

		public void Clean()
		{
			//.Delete(previousFilename);
		}

		public void Save()
		{
			var dialog = new DialogManager();
			dialog.OpenSaveDialog(_image);
			_filename = dialog.Filename;
		}

		public void Cut(Rectangle rectangle)
		{
			using (Bitmap temp = new Bitmap(rectangle.Width, rectangle.Height))
			{
				Graphics g = Graphics.FromImage(temp);

				g.DrawImage(_image, 0, 0, rectangle, GraphicsUnit.Pixel);

				var dialog = new DialogManager(); //FIXME
				dialog.OpenSaveDialog(temp);
				previousFilename = _filename;
				_filename = dialog.Filename;
			}
		}

		public void Dispose()
		{
			_image.Dispose();
		}
	}
}
