using MvvmCross;
using MvvmCross.Core;
using MvvmCross.Platforms.Wpf.Views;
using System.Windows;
using WPFCross.Startup.GlobalExceptions;
using WPFCross.Startup.Services;

namespace WPFCross.Startup
{
    public partial class App : MvxApplication
    {
        private AppGlobalExceptionHandler globalExceptionHandler;

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

        public override void ApplicationInitialized()
        {
            base.ApplicationInitialized();
            globalExceptionHandler = Mvx.IoCProvider.IoCConstruct<AppGlobalExceptionHandler>();
        }
    }
}
