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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YATA_Editor
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private ScriptManager scriptManager;
		List<string[]> array;

		public MainWindow()
		{
			InitializeComponent();
			scriptManager = new ScriptManager();

			OperationsManager opManager = new OperationsManager();
			List<ScriptDictionary> list = opManager.configuration.scriptDictionaryList;
			operationsListBox.ItemsSource = list;
		}

		private void RemoveOperation_Click(object sender, RoutedEventArgs e)
		{
			if (array != null)
			{
				array.RemoveAt(array.Count-1);
				UpdateScriptGrid();
			}
		}

		private void UpdateScriptGrid()
		{
			scriptGrid.ItemsSource = null;
			scriptGrid.ItemsSource = array;
		}

		private void AddOperation_Click(object sender, RoutedEventArgs e)
		{
			if(array != null)
			{
				ScreenshotPage screenshotPage = new ScreenshotPage();
				screenshotPage.ShowDialog();

				string[] temp = { ((ScriptDictionary)operationsListBox.SelectedItem).Value, screenshotPage.GetImage() };
				array.Add(temp);
				UpdateScriptGrid();
			}
		}

		private void NewFile_Click(object sender, RoutedEventArgs e)
		{
			array = new List<string[]>();
			scriptGrid.ItemsSource = array;
		}

		private void OpenFile_Click(object sender, RoutedEventArgs e)
		{
			array = scriptManager.Open();
			UpdateScriptGrid();
		}

		private void SaveFile_Click(object sender, RoutedEventArgs e)
		{
			scriptManager.GenerateScriptText(array);
			scriptManager.Save();
		}

		private void SaveFileAs_Click(object sender, RoutedEventArgs e)
		{
			scriptManager.GenerateScriptText(array);
			scriptManager.Save(true);
		}
	}
}
