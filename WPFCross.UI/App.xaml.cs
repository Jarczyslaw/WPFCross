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

namespace WPFCross.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : MvxApplication
    {
        private GlobalExceptionHandler globalExceptionHandler;

        protected override void RegisterSetup()
        {
            this.RegisterSetupType<CrossSetup>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
        }

        public override void ApplicationInitialized()
        {
            base.ApplicationInitialized();

            Mvx.IoCProvider.RegisterType<GlobalExceptionHandler>();
            globalExceptionHandler = Mvx.IoCProvider.Resolve<GlobalExceptionHandler>();
        }
    }
}
