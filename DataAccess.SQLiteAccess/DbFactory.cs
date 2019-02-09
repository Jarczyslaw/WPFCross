using DataAccess.Core;
using DataAccess.Core.DbLib;
using System.Data;
using System.Data.SQLite;

namespace DataAccess.SQLiteAccess
{
    public class DbFactory : IDbFactory
    {
        public readonly IConnectionStringProvider connectionStringProvider;

        public DbFactory(IConnectionStringProvider connectionStringProvider)
        {
            this.connectionStringProvider = connectionStringProvider;
        }

        public IDbConnection CreateConnection()
        {
            return new SQLiteConnection(connectionStringProvider.ConnectionString);
        }
    }
}
