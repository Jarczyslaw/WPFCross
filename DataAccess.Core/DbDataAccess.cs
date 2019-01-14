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
        }

        private string ConnectionString => connectionProvider.DbConnection;

        public void AddContact(Contact contact)
        {
            using (var db = new LiteDatabase(ConnectionString))
            {
                var collection = db.GetCollection<Contact>(ContactsCollection);
                collection.Insert(contact);
            }
        }

        private void AddContacts(IEnumerable<Contact> contacts)
        {
            using (var db = new LiteDatabase(ConnectionString))
            {
                var collection = db.GetCollection<Contact>(ContactsCollection);
                collection.InsertBulk(contacts);
            }
        }

        public void AddGroup(Group group)
        {
            using (var db = new LiteDatabase(ConnectionString))
            {
                var collection = db.GetCollection<Group>(GroupsCollection);
                collection.Insert(group);
            }
        }

        private void AddGroups(IEnumerable<Group> groups)
        {
            using (var db = new LiteDatabase(ConnectionString))
            {
                var collection = db.GetCollection<Group>(GroupsCollection);
                collection.InsertBulk(groups);
            }
        }

        public void DeleteContact(int id)
        {
            using (var db = new LiteDatabase(ConnectionString))
            {
                var collection = db.GetCollection<Contact>(ContactsCollection);
                collection.Delete(c => c.Id == id);
            }
        }

        public void DeleteGroup(int id)
        {
            using (var db = new LiteDatabase(ConnectionString))
            {
                var collection = db.GetCollection<Group>(GroupsCollection);
                collection.Delete(g => g.Id == id);
            }
        }

        public void EditContact(Contact contact)
        {
            using (var db = new LiteDatabase(ConnectionString))
            {
                var collection = db.GetCollection<Contact>(ContactsCollection);
                var edited = collection.FindById(contact.Id);
                dataMapperService.Map(contact, edited);
                collection.Update(edited);
            }
        }

        public void EditGroup(Group group)
        {
            using (var db = new LiteDatabase(ConnectionString))
            {
                var collection = db.GetCollection<Group>(GroupsCollection);
                var edited = collection.FindById(group.Id);
                dataMapperService.Map(group, edited);
                collection.Update(edited);
            }
        }

        public IEnumerable<Contact> GetContacts(Group group, bool favourites)
        {
            using (var db = new LiteDatabase(ConnectionString))
            {
                var collection = db.GetCollection<Contact>(GroupsCollection);
                return collection.Find(c => (group == null || c.Group.Id == group.Id)
                    && (!favourites || c.Favourite));
            }
        }

        public Group GetDefaultGroup()
        {
            using (var db = new LiteDatabase(ConnectionString))
            {
                var collection = db.GetCollection<Group>(GroupsCollection);
                return collection.FindAll().Single(g => g.Default);
            }
        }

        public IEnumerable<Group> GetGroups()
        {
            using (var db = new LiteDatabase(ConnectionString))
            {
                var collection = db.GetCollection<Group>(GroupsCollection);
                return collection.FindAll();
            }
        }

        public bool GroupExists(string groupName)
        {
            using (var db = new LiteDatabase(ConnectionString))
            {
                var collection = db.GetCollection<Group>(GroupsCollection);
                return collection.Find(g => g.Name == groupName).Any();
            }
        }

        public void Initialize()
        {
            using (var db = new LiteDatabase(ConnectionString))
            {
                db.DropCollection(GroupsCollection);
                var groupsCollection = db.GetCollection<Group>(GroupsCollection);
                AddGroups(DbDataInitializer.CreateGroups());

                db.DropCollection(GroupsCollection);
                var contactsCollection = db.GetCollection<Contact>(ContactsCollection);
                var groups = GetGroups();
                AddContacts(DbDataInitializer.CreateContacts(groups));
            }
        }
    }
}
