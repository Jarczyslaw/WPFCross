using DataAccess.Models;
using Service.DataMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Core
{
    public class DbDataAccess : IDbDataAccess
    {
        private readonly IDataMapperService dataMapperService;
        private readonly IDbConnectionProvider connectionProvider;

        private readonly ILiteDbAccess liteDbAccess;

        public DbDataAccess(IDataMapperService dataMapperService, IDbConnectionProvider connectionProvider)
        {
            this.dataMapperService = dataMapperService;
            this.connectionProvider = connectionProvider;

            liteDbAccess = new LiteDbAccess();
        }

        public void AddContact(Contact contact)
        {
            liteDbAccess.InvokeContactsAction(connectionProvider.DbConnection, (_, collection) => collection.Insert(contact));
        }

        private void AddContacts(IEnumerable<Contact> contacts)
        {
            liteDbAccess.InvokeContactsAction(connectionProvider.DbConnection, (_, collection) =>
            {
                foreach (var contact in contacts)
                {
                    collection.Insert(contact);
                }
            });
        }

        public void AddGroup(Group group)
        {
            liteDbAccess.InvokeGroupsAction(connectionProvider.DbConnection, (_, collection) => collection.Insert(group));
        }

        private void AddGroups(IEnumerable<Group> groups)
        {
            liteDbAccess.InvokeGroupsAction(connectionProvider.DbConnection, (_, collection) =>
            {
                foreach (var group in groups)
                {
                    collection.Insert(group);
                }
            });
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
            liteDbAccess.DropGroups(connectionProvider.DbConnection);
            var groups = DbDataInitializer.CreateGroups();
            AddGroups(groups);

            liteDbAccess.DropContacts(connectionProvider.DbConnection);
            var contacts = DbDataInitializer.CreateContacts(groups);
            AddContacts(contacts);
        }
    }
}
