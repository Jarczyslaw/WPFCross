using DataAccess.Models;
using LiteDB;
using System;

namespace DataAccess.Core
{
    public class LiteDbAccess : ILiteDbAccess
    {
        public string ContactsCollection { get; } = "Contacts";
        public string GroupsCollection { get; } = "Groups";

        public LiteDbAccess()
        {
            BsonMapper.Global.Entity<Contact>()
                .DbRef(x => x.Group, GroupsCollection);
        }

        public void DropContacts(string connectionString)
        {
            InvokeDbAction(connectionString, (db) => db.DropCollection(ContactsCollection));
        }

        public void DropGroups(string connectionString)
        {
            InvokeDbAction(connectionString, (db) => db.DropCollection(GroupsCollection));
        }

        public void InvokeContactsAction(string connectionString, Action<LiteDatabase, LiteCollection<Contact>> action)
        {
            InvokeCollectionAction(connectionString, ContactsCollection, action);
        }

        public void InvokeGroupsAction(string connectionString, Action<LiteDatabase, LiteCollection<Group>> action)
        {
            InvokeCollectionAction(connectionString, GroupsCollection, action);
        }

        public void InvokeDbAction(string connectionString, Action<LiteDatabase> action)
        {
            using (var db = new LiteDatabase(connectionString))
            {
                action(db);
            }
        }

        private void InvokeCollectionAction<T>(string connectionString, string collectionName, Action<LiteDatabase, LiteCollection<T>> action)
        {
            using (var db = new LiteDatabase(connectionString))
            {
                var collection = db.GetCollection<T>(collectionName);
                action(db, collection);
            }
        }
    }
}
