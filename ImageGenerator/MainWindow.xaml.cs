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

using ImageGenerator.Services;

namespace ImageGenerator
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private async void btnGenerateImage_Click(object sender, RoutedEventArgs e)
		{
			var service = new OpenAIService();
			var question = txtPrompt.Text;
			var generation = await service.GenerateImage(question);

			var bmp = new BitmapImage();
			bmp.BeginInit();
			bmp.UriSource = new Uri(generation);
			bmp.EndInit();

			imgResult.Source = bmp;
		}
	}
}
