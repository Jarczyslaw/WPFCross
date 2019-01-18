using DataAccess.Models;
using LiteDB;
using Service.DataMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Core
{
    public class DbDataAccess : IDbDataAccess
    {
        public string ContactsCollection { get; } = "Contacts";
        public string GroupsCollection { get; } = "Groups";

        private readonly IDataMapperService dataMapperService;
        private readonly IDbConnectionProvider connectionProvider;

        public DbDataAccess(IDataMapperService dataMapperService, IDbConnectionProvider connectionProvider)
        {
            this.dataMapperService = dataMapperService;
            this.connectionProvider = connectionProvider;

            BsonMapper.Global
                .Entity<Contact>().Id(c => c.Id);
            BsonMapper.Global
                .Entity<Group>().Id(g => g.Id);
        }

        private string ConnectionString => connectionProvider.DbConnection;

        public void AddContact(Contact contact)
        {
            using (var db = new LiteDatabase(ConnectionString))
            {
                var collection = GetContactsCollection(db);
                collection.Insert(contact);
            }
        }

        private void AddContacts(IEnumerable<Contact> contacts)
        {
            using (var db = new LiteDatabase(ConnectionString))
            {
                var collection = GetContactsCollection(db);
                collection.Insert(contacts);
            }
        }

        public void AddGroup(Group group)
        {
            using (var db = new LiteDatabase(ConnectionString))
            {
                var collection = GetGroupsCollection(db);
                collection.Insert(group);
            }
        }

        private void AddGroups(IEnumerable<Group> groups)
        {
            using (var db = new LiteDatabase(ConnectionString))
            {
                var collection = GetGroupsCollection(db);
                collection.Insert(groups);
            }
        }

        public void DeleteContact(int id)
        {
            using (var db = new LiteDatabase(ConnectionString))
            {
                var collection = GetContactsCollection(db);
                collection.Delete(c => c.Id == id);
            }
        }

        public void DeleteGroup(int id)
        {
            using (var db = new LiteDatabase(ConnectionString))
            {
                var collection = GetGroupsCollection(db);
                collection.Delete(g => g.Id == id);
            }
        }

        public void EditContact(Contact contact)
        {
            using (var db = new LiteDatabase(ConnectionString))
            {
                var collection = GetContactsCollection(db);
                var edited = collection.FindById(contact.Id);
                dataMapperService.Map(contact, edited);
                collection.Update(edited);
            }
        }

        public void EditGroup(Group group)
        {
            using (var db = new LiteDatabase(ConnectionString))
            {
                var collection = GetGroupsCollection(db);
                var edited = collection.FindById(group.Id);
                dataMapperService.Map(group, edited);
                collection.Update(edited);
            }
        }

        public IEnumerable<Contact> GetContacts(Group group, bool favourites)
        {
            using (var db = new LiteDatabase(ConnectionString))
            {
                var collection = GetContactsCollection(db);
                return collection.FindAll().Where(c => (group == null || c.Group.Id == group.Id)
                    && (!favourites || c.Favourite));
            }
        }

        public Group GetDefaultGroup()
        {
            using (var db = new LiteDatabase(ConnectionString))
            {
                var collection = GetGroupsCollection(db);
                return collection.FindAll().Single(g => g.Default);
            }
        }

        public IEnumerable<Group> GetGroups()
        {
            using (var db = new LiteDatabase(ConnectionString))
            {
                var collection = GetGroupsCollection(db);
                return collection.FindAll();
            }
        }

        public bool GroupExists(string groupName)
        {
            using (var db = new LiteDatabase(ConnectionString))
            {
                var collection = GetGroupsCollection(db);
                return collection.FindAll().Any(g => g.Name == groupName);
            }
        }

        public void Initialize()
        {
            using (var db = new LiteDatabase(ConnectionString))
            {
                db.DropCollection(GroupsCollection);
                var groupsCollection = GetGroupsCollection(db);
                AddGroups(DbDataInitializer.CreateGroups());

                db.DropCollection(ContactsCollection);
                var contactsCollection = GetContactsCollection(db);
                var groups = GetGroups();
                AddContacts(DbDataInitializer.CreateContacts(groups));
            }
        }

        private LiteCollection<Contact> GetContactsCollection(LiteDatabase database)
        {
            return database.GetCollection<Contact>(ContactsCollection);
        }

        private LiteCollection<Group> GetGroupsCollection(LiteDatabase database)
        {
            return database.GetCollection<Group>(GroupsCollection);
        }
    }
}
