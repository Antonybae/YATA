using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows;

namespace YATA_Editor
{
	class Screenshot
	{
		public Bitmap image { get; set; }
		int _width;
		int _height;
		public string _filename { get; set; }

		public Screenshot(int width, int height)
		{
			_width = width;
			_height = height;
			Update();
		}

		public void Update()
		{
			Graphics graph = null;
			image = new Bitmap(_width, _height);
			graph = Graphics.FromImage(image);
			graph.CopyFromScreen(0, 0, 0, 0, image.Size);
			Save(@"C:\\Users\\Anton\\Desktop\\Projects\\YATA\\Editor\\YATA_Editor\\YATA_Editor\\bin\\Debug\\temp.bmp");
		}

		public void Save(string filename)
		{
			_filename = filename;
			image.Save(_filename);
		}

		public void Cut(Rectangle rectangle)
		{
			// An empty bitmap which will hold the cropped image
			Bitmap temp = new Bitmap(_width, _height);

			Graphics g = Graphics.FromImage(temp);

			// Draw the given area (section) of the source image
			// at location 0,0 on the empty bitmap (bmp)
			g.DrawImage(image, 0, 0, rectangle, GraphicsUnit.Pixel);
			image = temp;
		}
	}
}
