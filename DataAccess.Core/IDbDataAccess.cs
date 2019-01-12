using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Core
{
    public interface IDbDataAccess
    {
        void Initialize();
        void AddContact(Contact contact);
        void AddGroup(Group group);
        void DeleteContact(int id);
        void DeleteGroup(int id);
        void EditContact(Contact contact);
        void EditGroup(Group group);
        IEnumerable<Contact> GetContacts(Group group, bool favourites);
        IEnumerable<Group> GetGroups();
        bool GroupExists(string groupName);
        Group GetDefaultGroup();
    }
}
