using System;
using System.Collections.Generic;
using System.Text;
using LiteDB;

namespace DataAccess.Core
{
    public class DatabaseSource : IDatabaseSource
    {
        public string DatabasePath { get; private set; }

        public DatabaseSource(string databasePath)
        {
            DatabasePath = databasePath;
        }

        public LiteDatabase OpenDatabase()
        {
            return new LiteDatabase(DatabasePath);
        }
    }
}
