using DataAccess.Core;
using WPFCross.UI.Services;

namespace WPFCross.UI
{
    public class ConnectionStringProvider : IConnectionStringProvider
    {
        public string ConnectionString { get; }

        public ConnectionStringProvider(IAppSettings appSettings, IArgsService argsService)
        {
            if (argsService.DbAccessType == DbAccessType.LiteDb)
            {
                ConnectionString = appSettings.LiteDbConnection;
            }
            else if (argsService.DbAccessType == DbAccessType.SQLite)
            {
                ConnectionString = appSettings.SQLiteConnection;
            }
        }
    }
}
