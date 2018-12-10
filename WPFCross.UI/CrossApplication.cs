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
using WPFCross.Extensions;
using WPFCross.UI.GlobalExceptions;

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
            Mvx.IoCProvider.RegisterSingleton<IDbDataAccess>(() => new DbDataAccessMock());
            RegisterGlobalHandler();
        }

        private void RegisterGlobalHandler()
        {
            var dialogs = Mvx.IoCProvider.Resolve<IDialogsService>();
            var logger = Mvx.IoCProvider.Resolve<ILoggerService>();
            Mvx.IoCProvider.RegisterSingleton<IGlobalExceptionHandler>(() => new GlobalExceptionHandler(logger, dialogs));
        }
    }
}
