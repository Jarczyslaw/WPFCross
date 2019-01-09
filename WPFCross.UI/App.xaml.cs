using MvvmCross;
using MvvmCross.Core;
using MvvmCross.Platforms.Wpf.Views;
using System.Windows;
using WPFCross.UI.GlobalExceptions;
using WPFCross.UI.Services;

namespace WPFCross.UI
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
            ArgsService.Parse(e.Args);

            var mainWindow = new MainWindow();
            mainWindow.Show();
        }

        public override void ApplicationInitialized()
        {
            base.ApplicationInitialized();
            globalExceptionHandler = Mvx.IoCProvider.IoCConstruct<AppGlobalExceptionHandler>();
        }
    }
}
