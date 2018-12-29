using DataAccess.Core;
using MvvmCross;
using MvvmCross.ViewModels;
using Service.Core;
using Service.Dialogs;
using Service.Logger;
using WPFCross.Core.ViewModels;
using WPFCross.UI.Services;

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
            Mvx.IoCProvider.RegisterSingleton<IContactsService>(() => new ContactsService(Mvx.IoCProvider.Resolve<IDbDataAccess>()));
            Mvx.IoCProvider.RegisterSingleton<IGroupsService>(() => new GroupsService(Mvx.IoCProvider.Resolve<IDbDataAccess>()));
        }
    }
}
