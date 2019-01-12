using DataAccess.Models;
using LiteDB;
using System;

namespace DataAccess.Core
{
    public class LiteDbAccess
    {
        private readonly string contactsCollection = "Contacts";
        private readonly string groupsCollection = "Groups";

        public void Contacts(string dbFile, Action<LiteCollection<Contact>> action)
        {
            InvokeCollectionAction(dbFile, contactsCollection, action);
        }

        public void Groups(string dbFile, Action<LiteCollection<Group>> action)
        {
            InvokeCollectionAction(dbFile, groupsCollection, action);
        }

        private void InvokeCollectionAction<T>(string dbFile, string collectionName, Action<LiteCollection<T>> action)
        {
            using (var db = new LiteDatabase(dbFile))
            {
                var collection = db.GetCollection<T>(collectionName);
                action(collection);
            }
        }
    }
}
