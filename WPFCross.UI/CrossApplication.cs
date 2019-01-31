using System.Threading.Tasks;
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

        public override Task Startup()
        {
            var argsService = Mvx.IoCProvider.Resolve<IArgsService>();
            var dbDataAccess = Mvx.IoCProvider.Resolve<IDbAccess>();

            if (argsService.DbInitialize)
            {
                dbDataAccess.Initialize();
            }
            else if (argsService.Clear)
            {
                dbDataAccess.Clear();
            }

            return base.Startup();
        }

        private void RegisterDependencies()
        {
            Mvx.IoCProvider.RegisterSingleton<IArgsService>(App.ArgsService);
            var appSettings = Mvx.IoCProvider.IoCConstruct<AppSettings>();
            Mvx.IoCProvider.RegisterSingleton<IAppSettings>(appSettings);
            Mvx.IoCProvider.RegisterSingleton<IDbConnectionProvider>(appSettings);
            Mvx.IoCProvider.RegisterSingleton<ILoggerService>(() => Mvx.IoCProvider.IoCConstruct<LoggerService>());
            Mvx.IoCProvider.RegisterSingleton<IDialogsService>(() => Mvx.IoCProvider.IoCConstruct<DialogsService>());
            Mvx.IoCProvider.RegisterSingleton<IDataMapperService>(() => Mvx.IoCProvider.IoCConstruct<DataMapperService>());
            if (App.ArgsService.Mock)
            {
                Mvx.IoCProvider.RegisterSingleton<IDbAccess>(() => Mvx.IoCProvider.IoCConstruct<DbAccessMock>());
            }
            else
            {
                Mvx.IoCProvider.RegisterSingleton<IDbAccess>(() => Mvx.IoCProvider.IoCConstruct<DbAccess>());
            }
            Mvx.IoCProvider.RegisterSingleton<IContactsService>(() => Mvx.IoCProvider.IoCConstruct<ContactsService>());
            Mvx.IoCProvider.RegisterSingleton<IGroupsService>(() => Mvx.IoCProvider.IoCConstruct<GroupsService>());
        }
    }
}
