using WPFCross.Core.ViewModels;
using Service.Dialogs;
using Service.Logger;
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
using WPFCross.Extensions;

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
            Mvx.IoCProvider.RegisterSingleton<ILoggerService>(() => new LoggerService());
            Mvx.IoCProvider.RegisterSingleton<IDialogsService>(() => new DialogsService());
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
