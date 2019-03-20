using Dapper;
using DataAccess.Core;
using DataAccess.Core.DbLib;
using DataAccess.Models;
using Service.DataMapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Dynamic;
using System.IO;
using System.Linq;

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
            AddContacts(new List<Contact> { contact });
        }

        private void AddContacts(IEnumerable<Contact> contacts)
        {
            const string sql = @"INSERT INTO Contacts (Title, Name, Favourite, GroupId) 
                                VALUES(@Title, @Name, @Favourite, @GroupId);";
            using (var db = CreateDbContext())
            {
                db.Connection.Execute(sql, contacts.Select(c => new Entities.Contact
                {
                    Favourite = c.Favourite ? 1 : 0,
                    GroupId = c.Group.Id,
                    Name = c.Name,
                    Title = c.Title
                }).ToArray());
            }
        }

        public void AddDummyData()
        {
            Clear();
            AddGroups(dbDummyData.CreateGroups());
            var groups = GetGroups();
            AddContacts(dbDummyData.CreateContacts(groups));
        }

        public void AddGroup(Group group)
        {
            AddGroups(new List<Group> { group });
        }

        private void AddGroups(IEnumerable<Group> groups)
        {
            const string sql = "INSERT INTO Groups (`Default`, Name) VALUES (@Default, @Name);";
            using (var db = CreateDbContext())
            {
                db.Connection.Execute(sql, groups.Select(g => new { g.Default, g.Name })
                    .ToArray());
            }
        }

        public void Clear()
        {
            using (var db = CreateDbContext())
            {
                db.Connection.Execute(GetClearQuery("ContactEntrys"));
                db.Connection.Execute(GetClearQuery("Contacts"));
                db.Connection.Execute(GetClearQuery("Groups"));

                AddGroup(dbDummyData.CreateDefaultGroup());
            }
        }

        private string GetClearQuery(string tableName)
        {
            return $@"DELETE FROM {tableName}; 
                    DELETE FROM sqlite_sequence WHERE name = '{tableName}';
                    VACUUM;";
        }

        public void DeleteContact(int id)
        {
            using (var db = CreateDbContext())
            {
                db.Connection.Execute("DELETE FROM ContactEntrys WHERE ContactId = @ContactId", new { ContactId = id });
                db.Connection.Execute("DELETE FROM Contacts WHERE Id = @Id", new { Id = id });
            }
        }

        public void DeleteGroup(int id)
        {
            using (var db = CreateDbContext())
            {
                db.Connection.Execute("DELETE FROM Contacts WHERE GroupId = @GroupId", new { GroupId = id });
                db.Connection.Execute("DELETE FROM Groups WHERE Id = @Id", new { Id = id });
            }
        }

        public void EditContact(Contact contact)
        {
            throw new NotImplementedException();
        }

        public void EditGroup(Group group)
        {
            const string sql = "UPDATE Groups SET Name = @Name";
            using (var db = CreateDbContext())
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

            var parameters = new ExpandoObject();
            if (group != null)
            {
                sql += $" AND GroupId = {group.Id}";
            }

            if (favourites)
            {
                sql += " AND Favourite = 1";
            }

            using (var db = CreateDbContext())
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
            const string sql = "SELECT * FROM Groups";
            using (var db = CreateDbContext())
            {
                return db.Connection.Query<Entities.Group>(sql).Select(g => new Group
                {
                    Default = g.Default == 1,
                    Id = g.Id,
                    Name = g.Name
                }).ToList();
            }
        }

        public bool GroupExists(string groupName)
        {
            const string sql = "SELECT count(*) FROM Groups WHERE Name = @Name";
            using (var db = CreateDbContext())
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

            using (var db = CreateDbContext())
            {
                InitializeContacts(db.Connection);
                InitializeContactEntries(db.Connection);
                InitializeGroups(db.Connection);
            }
        }

        private DbContext CreateDbContext()
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
