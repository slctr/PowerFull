using PowerfullTool.DI;
using PowerfullTool.ViewModels;
using System.Windows;

namespace PowerfullTool
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			IocKernel.Initialize(new IocConfiguration());
			ApplicationViewModel applicationViewModel = IocKernel.Get<ApplicationViewModel>();

			MainWindow window = new MainWindow(applicationViewModel);
			window.Show();
		}
	}
}
