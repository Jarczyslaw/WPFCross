using DataAccess.Core;
using System.IO;
using System.Windows;
using WPFCross.Startup.Services;

namespace WPFCross.Startup
{
    public class DbConnectionProvider : IDbConnectionProvider
    {
        public string ConnectionString { get; private set; }
        public string DbFilePath { get; private set; }

        public DbConnectionProvider(IAppSettings appSettings, IArgsService argsService)
        {
            LoadDbSettings(appSettings, argsService);
        }

        private void LoadDbSettings(IAppSettings appSettings, IArgsService argsService)
        {
            if (argsService.DbAccessType == DbAccessType.LiteDb)
            {
                DbFilePath = GetDbFilePath(appSettings.LiteDbFileName);
                ConnectionString = string.Format(appSettings.LiteDbConnection, DbFilePath);
            }
            else if (argsService.DbAccessType == DbAccessType.SQLite)
            {
                DbFilePath = GetDbFilePath(appSettings.SQLiteFileName);
                ConnectionString = string.Format(appSettings.SQLiteConnection, DbFilePath);
            }
        }

        private string GetDbFilePath(string fileName)
        {
            return Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, fileName);
        }
    }
}
