﻿using Dapper;
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
                    contact.Id = AddContact(db.Connection, contact);
                    InsertContactEntries(db.Connection, contact);
                    transactionScope.Complete();
                }
            }
        }

        private int AddContact(IDbConnection connection, Contact contact)
        {
            const string sql = @"INSERT INTO Contacts (Title, Name, Favourite, GroupId) 
                                VALUES(@Title, @Name, @Favourite, @GroupId);
                                SELECT seq FROM sqlite_sequence WHERE Name=""Contacts""";
            return Convert.ToInt32(connection.ExecuteScalar(sql, new Entities.Contact(contact)));
        }

        public void AddDummyData()
        {
            using (var transactionScope = new TransactionScope())
            {
                using (var db = GetDbContext())
                {
                    Clear(db.Connection);
                    var groups = dbDummyData.CreateGroups();
                    foreach (var group in groups)
                    {
                        AddGroup(db.Connection, group);
                    }

                    groups = GetGroups(db.Connection).ToList();
                    foreach (var contact in dbDummyData.CreateContacts(groups))
                    {
                        contact.Id = AddContact(db.Connection, contact);
                        InsertContactEntries(db.Connection, contact);
                    }
                    transactionScope.Complete();
                }
            }
        }

        public void AddGroup(Group group)
        {
            using (var db = GetDbContext())
            {
                AddGroup(db.Connection, group);
            }
        }

        private int AddGroup(IDbConnection connection, Group group)
        {
            const string sql = @"INSERT INTO Groups (`Default`, Name) 
                                VALUES (@Default, @Name);
                                SELECT seq FROM sqlite_sequence WHERE Name=""Groups""";
            return Convert.ToInt32(connection.ExecuteScalar(sql, new Entities.Group(group)));
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

            AddGroup(connection, dbDummyData.CreateDefaultGroup());
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
            using (var transactionScope = new TransactionScope())
            {
                using (var db = GetDbContext())
                {
                    UpdateContact(db.Connection, contact);
                    DeleteContactEntries(db.Connection, contact.Id);
                    InsertContactEntries(db.Connection, contact);
                    transactionScope.Complete();
                }
            }
        }

        private void UpdateContact(IDbConnection connection, Contact contact)
        {
            const string sql = @"UPDATE Contacts 
                                SET Title = @title, Name = @Name, Favourite = @Favourite, GroupId = @GroupId
                                WHERE Id = @Id";
            connection.Execute(sql, new Entities.Contact(contact));
        }

        private void DeleteContactEntries(IDbConnection connection, int contactId)
        {
            connection.Execute("DELETE FROM ContactEntrys WHERE ContactId = @ContactId", new { ContactId = contactId });
        }

        private void InsertContactEntries(IDbConnection connection, Contact contact)
        {
            const string sql = @"INSERT INTO ContactEntrys (Type, Value, ContactId)
                                VALUES (@Type, @Value, @ContactId)";
            connection.Execute(sql, contact.Items.Select(i =>
            {
                return new Entities.ContactEntry(i)
                {
                    ContactId = contact.Id
                };
            }).ToArray());
        }

        public void EditGroup(Group group)
        {
            const string sql = "UPDATE Groups SET Name = @Name WHERE Id = @Id";
            using (var db = GetDbContext())
            {
                db.Connection.Execute(sql, new Entities.Group(group));
            }
        }

        public IEnumerable<Contact> GetContacts(Group group, bool favourites)
        {
            string sql = @"
                SELECT
                    *
                FROM Contacts c
                JOIN Groups g ON c.GroupId = g.Id
                LEFT JOIN ContactEntrys ce ON ce.ContactId = c.Id 
                WHERE 1 = 1
                {0}
                ORDER BY Id";

            var where = string.Empty;
            if (group != null)
            {
                where += $" AND c.GroupId = {group.Id}";
            }

            if (favourites)
            {
                where += " AND c.Favourite = 1";
            }

            sql = string.Format(sql, where);

            using (var db = GetDbContext())
            {
                var lookup = new Dictionary<int, Contact>();
                var queryResult = db.Connection.Query<Entities.Contact, Entities.Group, Entities.ContactEntry, Entities.Contact>(sql,
                    (contact, contactEntry, g) => GetContactsLookup(lookup, contact, contactEntry, g));

                var contacts = lookup.Values;
                foreach (var contact in contacts)
                {
                    contact.Items.OrderBy(i => i.Id);
                }
                return contacts;
            }
        }

        private Entities.Contact GetContactsLookup(Dictionary<int, Contact> lookup, Entities.Contact contact, Entities.Group group, Entities.ContactEntry contactEntry)
        {
            if (!lookup.TryGetValue(contact.Id, out Contact tempContact))
            {
                tempContact = contact.ToModel();
                tempContact.Group = group.ToModel();
                tempContact.Items = new List<ContactEntry>();
                lookup.Add(contact.Id, tempContact);
            }

            if (contactEntry != null)
            {
                tempContact.Items.Add(contactEntry.ToModel());
            }

            return contact;
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
            return connection.Query<Entities.Group>(sql)
                .Select(g => g.ToModel()).ToList();
        }

        public bool GroupExists(string groupName)
        {
            const string sql = "SELECT count(*) FROM Groups WHERE Name = @Name";
            using (var db = GetDbContext())
            {
                return db.Connection.ExecuteScalar<int>(sql, new { Name = groupName }) > 0;
            }
        }

        private bool DefaultGroupExists(IDbConnection connection)
        {
            const string sql = "SELECT count(*) FROM Groups WHERE `Default` = 1";
            return Convert.ToInt32(connection.ExecuteScalar(sql)) > 0;
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
                    if (!DefaultGroupExists(db.Connection))
                    {
                        AddGroup(db.Connection, dbDummyData.CreateDefaultGroup());
                    }
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
