﻿using DataAccess.Core;
using DataAccess.Core.DbLib;
using System.Data;
using System.Data.SQLite;

namespace DataAccess.SQLiteAccess
{
    public class DbFactory : IDbFactory
    {
        public readonly IDbConnectionProvider dbConnectionProvider;

        public DbFactory(IDbConnectionProvider dbConnectionProvider)
        {
            this.dbConnectionProvider = dbConnectionProvider;
        }

        public IDbConnection CreateConnection()
        {
            return new SQLiteConnection(dbConnectionProvider.ConnectionString);
        }
    }
}
