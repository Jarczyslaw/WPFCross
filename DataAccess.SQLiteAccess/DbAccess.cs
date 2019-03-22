using Dapper;
using DataAccess.Core;
using DataAccess.Core.DbLib;
using DataAccess.Models;
using Service.DataMapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Transactions;

namespace DataAccess.SQLiteAccess
{
    public class DbAccess : DbAccessBase, IDbAccess
    {
        private readonly IDbFactory dbFactory;
        private readonly IDbDummyData dbDummyData;

        public DbAccess(IDataMapperService dataMapperService, IDbConnectionProvider dbConnectionProvider)
            : base(dataMapperService, dbConnectionProvider)
        {
            dbFactory = new DbFactory(dbConnectionProvider);
            dbDummyData = new DbDummyData();
        }

        public void AddContact(Contact contact)
        {
            using (var transactionScope = new TransactionScope())
            {
                using (var db = GetDbContext())
                {
                    AddContacts(new List<Contact> { contact }, db.Connection);
                    transactionScope.Complete();
                }
            }
        }

        private void AddContacts(IEnumerable<Contact> contacts, IDbConnection connection)
        {
            const string sql = @"INSERT INTO Contacts (Title, Name, Favourite, GroupId) 
                                VALUES(@Title, @Name, @Favourite, @GroupId);";
            connection.Execute(sql, contacts.Select(c => new Entities.Contact
            {
                Favourite = c.Favourite ? 1 : 0,
                GroupId = c.Group.Id,
                Name = c.Name,
                Title = c.Title
            }).ToArray());
        }

        public void AddDummyData()
        {
            using (var transactionScope = new TransactionScope())
            {
                using (var db = GetDbContext())
                {
                    Clear(db.Connection);
                    AddGroups(dbDummyData.CreateGroups(), db.Connection);
                    var groups = GetGroups(db.Connection);
                    AddContacts(dbDummyData.CreateContacts(groups), db.Connection);
                    transactionScope.Complete();
                }
            }
        }

        public void AddGroup(Group group)
        {
            using (var db = GetDbContext())
            {
                AddGroups(new List<Group> { group }, db.Connection);
            }
        }

        private void AddGroups(IEnumerable<Group> groups, IDbConnection connection)
        {
            const string sql = "INSERT INTO Groups (`Default`, Name) VALUES (@Default, @Name);";
            connection.Execute(sql, groups.Select(g => new { g.Default, g.Name })
                    .ToArray());
        }

        public void Clear()
        {
            using (var transactionScope = new TransactionScope())
            {
                using (var db = GetDbContext())
                {
                    Clear(db.Connection);
                    transactionScope.Complete();
                }
            }
        }

        private void Clear(IDbConnection connection)
        {
            connection.Execute(GetClearQuery("ContactEntrys"));
            connection.Execute(GetClearQuery("Contacts"));
            connection.Execute(GetClearQuery("Groups"));

            AddGroups(new List<Group> { dbDummyData.CreateDefaultGroup() }, connection);
        }

        private string GetClearQuery(string tableName)
        {
            return $@"DELETE FROM {tableName}; 
                    DELETE FROM sqlite_sequence WHERE name = '{tableName}';";
        }

        public void DeleteContact(int id)
        {
            using (var transactionScope = new TransactionScope())
            {
                using (var db = GetDbContext())
                {
                    db.Connection.Execute("DELETE FROM ContactEntrys WHERE ContactId = @ContactId", new { ContactId = id });
                    db.Connection.Execute("DELETE FROM Contacts WHERE Id = @Id", new { Id = id });
                    transactionScope.Complete();
                }
            }
        }

        public void DeleteGroup(int id)
        {
            using (var transactionScope = new TransactionScope())
            {
                using (var db = GetDbContext())
                {
                    db.Connection.Execute("DELETE FROM Contacts WHERE GroupId = @GroupId", new { GroupId = id });
                    db.Connection.Execute("DELETE FROM Groups WHERE Id = @Id", new { Id = id });
                    transactionScope.Complete();
                }
            }
        }

        public void EditContact(Contact contact)
        {
            throw new NotImplementedException();
        }

        public void EditGroup(Group group)
        {
            const string sql = "UPDATE Groups SET Name = @Name";
            using (var db = GetDbContext())
            {
                db.Connection.Execute(sql, new { group.Name });
            }
        }

        public IEnumerable<Contact> GetContacts(Group group, bool favourites)
        {
            string sql = @"
                SELECT
                    *
                FROM Contacts
                WHERE 1 = 1";

            if (group != null)
            {
                sql += $" AND GroupId = {group.Id}";
            }

            if (favourites)
            {
                sql += " AND Favourite = 1";
            }

            using (var db = GetDbContext())
            {
                return db.Connection.Query<Entities.Contact>(sql).Select(c => new Contact
                {
                    Favourite = c.Favourite == 1,
                    Id = c.Id,
                    Name = c.Name,
                    Title = c.Title
                }).ToList();
            }
        }

        public Group GetDefaultGroup()
        {
            return GetGroups().Single(g => g.Default);
        }

        public IEnumerable<Group> GetGroups()
        {
            using (var db = GetDbContext())
            {
                return GetGroups(db.Connection);
            }
        }

        private IEnumerable<Group> GetGroups(IDbConnection connection)
        {
            const string sql = "SELECT * FROM Groups";
            return connection.Query<Entities.Group>(sql).Select(g => new Group
            {
                Default = g.Default == 1,
                Id = g.Id,
                Name = g.Name
            }).ToList();
        }

        public bool GroupExists(string groupName)
        {
            const string sql = "SELECT count(*) FROM Groups WHERE Name = @Name";
            using (var db = GetDbContext())
            {
                return db.Connection.ExecuteScalar<int>(sql, new { Name = groupName }) > 0;
            }
        }

        public void Initialize()
        {
            if (!File.Exists(dbConnectionProvider.DbFilePath))
            {
                SQLiteConnection.CreateFile(dbConnectionProvider.DbFilePath);
            }

            using (var transactionScope = new TransactionScope())
            {
                using (var db = GetDbContext())
                {
                    InitializeContacts(db.Connection);
                    InitializeContactEntries(db.Connection);
                    InitializeGroups(db.Connection);
                    transactionScope.Complete();
                }
            }
        }

        private DbContext GetDbContext()
        {
            return new DbContext(dbFactory);
        }

        private void InitializeContacts(IDbConnection connection)
        {
            const string sql = @"CREATE TABLE IF NOT EXISTS Contacts (
                                Id    INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	                            Title TEXT,
	                            Name  TEXT,
	                            Favourite INTEGER NOT NULL DEFAULT 0,
	                            GroupId   INTEGER);";
            connection.Execute(sql);
        }

        private void InitializeContactEntries(IDbConnection connection)
        {
            const string sql = @"CREATE TABLE IF NOT EXISTS ContactEntrys (
                                Id    INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	                            Type  INTEGER NOT NULL DEFAULT 0,
	                            Value TEXT,
                                ContactId	INTEGER);";
            connection.Execute(sql);
        }

        private void InitializeGroups(IDbConnection connection)
        {
            const string sql = @"CREATE TABLE IF NOT EXISTS Groups (
                                Id    INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	                            `Default`   INTEGER NOT NULL DEFAULT 0,
	                            Name  TEXT);";
            connection.Execute(sql);
        }

        public int GetContactsCount()
        {
            return GetContacts(null, false).Count();
        }
    }
}
