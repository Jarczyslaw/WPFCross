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
            Mvx.IoCProvider.RegisterSingleton<ILoggingService>(() => new LoggingService());
            Mvx.IoCProvider.RegisterType<IDialogsService, DialogsService>();
        }
    }
}
