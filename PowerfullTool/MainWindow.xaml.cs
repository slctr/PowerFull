using PowerfullTool.ViewModels;
using System.Windows;

namespace PowerfullTool
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow(ApplicationViewModel applicationViewModel)
		{
			this.DataContext = applicationViewModel;
			this.InitializeComponent();
		}
	}
}
