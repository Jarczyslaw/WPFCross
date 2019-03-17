using Dapper;
using DataAccess.Core;
using DataAccess.Core.DbLib;
using DataAccess.Models;
using Service.DataMapper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace DataAccess.SQLiteAccess
{
    public class DbAccess : DbAccessBase, IDbAccess
    {
        private readonly IDbFactory dbFactory;

        public DbAccess(IDataMapperService dataMapperService, IDbConnectionProvider dbConnectionProvider)
            : base(dataMapperService, dbConnectionProvider)
        {
            dbFactory = new DbFactory(dbConnectionProvider);
        }

        public void AddContact(Contact contact)
        {
            throw new NotImplementedException();
        }

        public void AddDummyData()
        {
            throw new NotImplementedException();
        }

        public void AddGroup(Group group)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            using (var db = CreateDbContext())
            {
                //db.Connection.Execute("DROP TABLE IF EXISTS Contacts");
            }
        }

        public void DeleteContact(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteGroup(int id)
        {
            throw new NotImplementedException();
        }

        public void EditContact(Contact contact)
        {
            throw new NotImplementedException();
        }

        public void EditGroup(Group group)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Contact> GetContacts(Group group, bool favourites)
        {
            throw new NotImplementedException();
        }

        public Group GetDefaultGroup()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Group> GetGroups()
        {
            throw new NotImplementedException();
        }

        public bool GroupExists(string groupName)
        {
            throw new NotImplementedException();
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        private DbContext CreateDbContext()
        {
            return new DbContext(dbFactory);
        }

        private void InitializeDatabase()
        {
            if (!File.Exists(dbConnectionProvider.DbFilePath))
            {
                SQLiteConnection.CreateFile(dbConnectionProvider.DbFilePath);

                using (var db = CreateDbContext())
                {
                    var sql = @"CREATE TABLE Contacts (
                                Id    INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	                            Title TEXT,
	                            Name  TEXT,
	                            Favourite INTEGER DEFAULT 0
                            );";
                    db.Connection.Execute(sql);
                }
            }
        }
    }
}
