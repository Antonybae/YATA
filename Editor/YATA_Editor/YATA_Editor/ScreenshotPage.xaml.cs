using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace YATA_Editor
{
	/// <summary>
	/// Interaction logic for ScreenshotPage.xaml
	/// </summary>
	public partial class ScreenshotPage : Window
	{
		Screenshot screen;
		System.Drawing.Point start;
		System.Drawing.Point end;

		bool IsLMBPressed = false;

		public ScreenshotPage()
		{
			InitializeComponent();

			this.Width = (int)SystemParameters.VirtualScreenWidth;
			this.Height = (int)SystemParameters.VirtualScreenHeight;
			screen = new Screenshot((int)this.Width, (int)this.Height);
			Uri uri = new Uri(screen._filename, UriKind.RelativeOrAbsolute);
			ScreenshotImage.Source = new BitmapImage(uri);
		}

		public string GetImage()
		{
			if (screen._filename == null)
				return "";
			else
				return screen._filename;
		}

		private void LMBDown(object sender, MouseButtonEventArgs e)
		{
			IsLMBPressed = true;
			start = new System.Drawing.Point((int)e.GetPosition(this).X, (int)e.GetPosition(this).Y);
		}
		private void LMBUp(object sender, MouseButtonEventArgs e)
		{
			IsLMBPressed = false;
			end = new System.Drawing.Point((int)e.GetPosition(this).X, (int)e.GetPosition(this).Y);

			screen.Cut(new System.Drawing.Rectangle(start.X, start.Y, end.X - start.X, end.Y - start.Y));

			ScreenshotImage.Source = null;
			screen.Clean();
			Close();
		}

		private void ImageMouseMove(object sender, MouseEventArgs e)
		{
			if(IsLMBPressed)
			{

			}
		}
	}
}
