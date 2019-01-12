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
        private readonly LiteDbAccess liteDbAccess;

        public DbDataAccess(IDataMapperService dataMapperService)
        {
            this.dataMapperService = dataMapperService;
            liteDbAccess = new LiteDbAccess();
        }

        public void AddContact(Contact contact)
        {
            throw new NotImplementedException();
        }

        public void AddGroup(Group group)
        {
            throw new NotImplementedException();
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
    }
}
