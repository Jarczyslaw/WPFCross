using MvvmCross;
using MvvmCross.Core;
using MvvmCross.IoC;
using MvvmCross.Platforms.Wpf.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using WPFCross.Extensions;
using WPFCross.UI.GlobalExceptions;

namespace WPFCross.UI
{
    public partial class App : MvxApplication
    {
        private AppGlobalExceptionHandler globalExceptionHandler;

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
            globalExceptionHandler = Mvx.IoCProvider.RegisterTypeAndResolve<AppGlobalExceptionHandler>();
        }
    }
}
