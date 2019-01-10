using DataAccess.Core;
using MvvmCross;
using MvvmCross.ViewModels;
using Service.Core;
using Service.DataMapper;
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
            Mvx.IoCProvider.RegisterSingleton<IArgsService>(App.ArgsService);
            Mvx.IoCProvider.RegisterSingleton<IAppSettings>(() => Mvx.IoCProvider.IoCConstruct<AppSettings>());
            Mvx.IoCProvider.RegisterSingleton<ILoggerService>(() => Mvx.IoCProvider.IoCConstruct<LoggerService>());
            Mvx.IoCProvider.RegisterSingleton<IDialogsService>(() => Mvx.IoCProvider.IoCConstruct<DialogsService>());
            Mvx.IoCProvider.RegisterSingleton<IDataMapperService>(() => Mvx.IoCProvider.IoCConstruct<DataMapperService>());
            if (App.ArgsService.Mock)
            {
                Mvx.IoCProvider.RegisterSingleton<IDbDataAccess>(() => Mvx.IoCProvider.IoCConstruct<DbDataAccessMock>());
            }
            else
            {
                Mvx.IoCProvider.RegisterSingleton<IDbDataAccess>(() => Mvx.IoCProvider.IoCConstruct<DbDataAccess>());
            }
            Mvx.IoCProvider.RegisterSingleton<IContactsService>(() => Mvx.IoCProvider.IoCConstruct<ContactsService>());
            Mvx.IoCProvider.RegisterSingleton<IGroupsService>(() => Mvx.IoCProvider.IoCConstruct<GroupsService>());
        }
    }
}
