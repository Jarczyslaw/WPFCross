using Core.ViewModels;
using Dialogs;
using Logging;
using MvvmCross;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cross.App
{
    public class Application : MvxApplication
    {
        public override void Initialize()
        {
            RegisterDependencies();
            RegisterAppStart<MainViewModel>();
        }

        private void RegisterDependencies()
        {
            Mvx.IoCProvider.RegisterSingleton<ILoggingService>(() => new LoggingService());
            Mvx.IoCProvider.RegisterType<IDialogsService, DialogsService>();
        }
    }
}
