using WPFCross.Core.ViewModels;
using Dialogs;
using Logging;
using MvvmCross;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFCross.UI.Services;
using DataAccess.Core;
using DataAccess.Core.Collections;

namespace WPFCross.UI
{
    public class CrossApplication : MvxApplication
    {
        public override void Initialize()
        {
            RegisterDependencies();
            RegisterAppStart<MainViewModel>();
        }

        private void RegisterDependencies()
        {
            Mvx.IoCProvider.RegisterSingleton<IAppSettings>(() => new AppSettings());
            Mvx.IoCProvider.RegisterSingleton<ILoggingService>(() => new LoggingService());
            Mvx.IoCProvider.RegisterType<IDialogsService, DialogsService>();
            RegisterDbDependencies();
        }

        private void RegisterDbDependencies()
        {
            var appSettings = Mvx.IoCProvider.Resolve<IAppSettings>();
            Mvx.IoCProvider.RegisterType<IDatabaseSource>(() => new DatabaseSource(appSettings.LiteDbConnectionString.ConnectionString));
            Mvx.IoCProvider.RegisterType<IContacts, Contacts>();
        }
    }
}
