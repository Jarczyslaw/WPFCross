﻿using System.Threading.Tasks;
using DataAccess.Core;
using MvvmCross;
using MvvmCross.ViewModels;
using Service.Core;
using Service.DataMapper;
using Service.Dialogs;
using Service.Logger;
using WPFCross.Core.ViewModels;
using WPFCross.Extensions;
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
            Mvx.IoCProvider.RegisterAndConstruct<IConnectionStringProvider, ConnectionStringProvider>();
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
