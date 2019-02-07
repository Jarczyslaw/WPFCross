using DataAccess.Core.DbLib;
using System.Data;
using System.Data.SQLite;

namespace DataAccess.SQLiteAccess
{
    public class DbFactory : IDbFactory
    {
        public string ConnectionString { get; }

        public DbFactory(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            return new SQLiteConnection($"Data Source={ConnectionString};Version=3;Pooling=True;");
        }
    }
}
