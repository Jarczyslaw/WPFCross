using System.Threading.Tasks;
using DataAccess.Core;
using MvvmCross;
using MvvmCross.ViewModels;
using Service.Core;
using Service.DataMapper;
using Service.Dialogs;
using Service.Logger;
using WPFCross.Core.ViewModels;
using WPFCross.Extensions;
using WPFCross.Startup.GlobalExceptions;
using WPFCross.Startup.Services;

namespace WPFCross.Startup
{
    public class CrossApplication : MvxApplication
    {
        public override void Initialize()
        {
            RegisterDependencies();
            InitializeViewModels();
        }

        public override Task Startup()
        {
            Mvx.IoCProvider.IoCConstruct<AppGlobalExceptionHandler>();
            InitializeDatabase();
            return base.Startup();
        }

        private void InitializeViewModels()
        {
            if (!App.ArgsService.Test)
            {
                RegisterAppStart<MainViewModel>();
            }
            else
            {
                RegisterAppStart<TestViewModel>();
            }
        }

        private void InitializeDatabase()
        {
            var dbDataAccess = Mvx.IoCProvider.Resolve<IDbAccess>();
            dbDataAccess.Initialize();

            if (App.ArgsService.DummyData)
            {
                dbDataAccess.AddDummyData();
            }
            else if (App.ArgsService.Clear)
            {
                dbDataAccess.Clear();
            }
        }

        private void RegisterDependencies()
        {
            Mvx.IoCProvider.RegisterSingleton<IArgsService>(App.ArgsService);
            Mvx.IoCProvider.RegisterAndConstruct<IAppSettings, AppSettings>();
            Mvx.IoCProvider.RegisterAndConstruct<ILoggerService, LoggerService>();
            Mvx.IoCProvider.RegisterAndConstruct<IDialogsService, DialogsService>();
            Mvx.IoCProvider.RegisterAndConstruct<IDataMapperService, DataMapperService>();
            Mvx.IoCProvider.RegisterAndConstruct<IContactsService, ContactsService>();
            Mvx.IoCProvider.RegisterAndConstruct<IGroupsService, GroupsService>();
            RegisterDbAccess();
        }

        private void RegisterDbAccess()
        {
            Mvx.IoCProvider.RegisterAndConstruct<IDbConnectionProvider, DbConnectionProvider>();
            switch (App.ArgsService.DbAccessType)
            {
                case DbAccessType.Mock:
                    Mvx.IoCProvider.RegisterAndConstruct<IDbAccess, DbAccessMock>();
                    break;
                case DbAccessType.LiteDb:
                    Mvx.IoCProvider.RegisterAndConstruct<IDbAccess, DataAccess.LiteDbAccess.DbAccess>();
                    break;
                case DbAccessType.SQLite:
                    Mvx.IoCProvider.RegisterAndConstruct<IDbAccess, DataAccess.SQLiteAccess.DbAccess>();
                    break;
            }
        }
    }
}
