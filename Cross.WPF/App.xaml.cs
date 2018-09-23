using Cross.App;
using Dialogs;
using Logging;
using MvvmCross;
using MvvmCross.Core;
using MvvmCross.IoC;
using MvvmCross.Platforms.Wpf.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Cross.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : MvxApplication
    {
        private GlobalExceptionHandler globalExceptionHandler;

        protected override void RegisterSetup()
        {
            this.RegisterSetupType<Setup>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
        }

        public override void ApplicationInitialized()
        {
            base.ApplicationInitialized();

            var logging = Mvx.IoCProvider.Resolve<ILoggingService>();
            var dialogs = Mvx.IoCProvider.Resolve<IDialogsService>();
            globalExceptionHandler = new GlobalExceptionHandler(logging, dialogs);
        }
    }
}
