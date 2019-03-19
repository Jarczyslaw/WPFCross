using DataAccess.Models;
using System.Collections.Generic;

namespace DataAccess.Core
{
    public interface IDbAccess
    {
        void Initialize();
        void Clear();
        void AddDummyData();
        void AddContact(Contact contact);
        void AddGroup(Group group);
        void DeleteContact(int id);
        void DeleteGroup(int id);
        void EditContact(Contact contact);
        void EditGroup(Group group);
        IEnumerable<Contact> GetContacts(Group group, bool favourites);
        int GetContactsCount();
        IEnumerable<Group> GetGroups();
        bool GroupExists(string groupName);
        Group GetDefaultGroup();
    }
}
