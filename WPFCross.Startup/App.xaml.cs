using MvvmCross.Core;
using MvvmCross.Platforms.Wpf.Views;
using System.Windows;
using WPFCross.Startup.Services;

namespace WPFCross.Startup
{
    public partial class App : MvxApplication
    {
        public static ArgsService ArgsService { get; private set; }

        protected override void RegisterSetup()
        {
            this.RegisterSetupType<CrossSetup>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            ArgsService = new ArgsService();
            if (!ArgsService.Parse(e.Args))
            {
                MessageBox.Show("Error while parsing app arguments!");
            }

            ShowWindow();
        }

        private void ShowWindow()
        {
            Window startupWindow = null;
            if (!ArgsService.Test)
            {
                startupWindow = new MainWindow();
            }
            else
            {
                startupWindow = new TestWindow();
            }
            startupWindow.Show();
        }
    }
}
