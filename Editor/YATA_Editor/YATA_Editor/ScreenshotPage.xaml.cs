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

		public ScreenshotPage()
		{
			InitializeComponent();
			this.Width = (int)SystemParameters.VirtualScreenWidth;
			this.Height = (int)SystemParameters.VirtualScreenHeight;
			screen = new Screenshot((int)this.Width, (int)this.Height);
			ScreenshotImage.Source = new BitmapImage(new Uri(screen._filename));
		}
	}
}
