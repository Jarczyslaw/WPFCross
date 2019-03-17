using Commons;
using DataAccess.Models;
using Service.DataMapper;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Core
{
    public class DbAccessMock : IDbAccess
    {
        private readonly IDataMapperService dataMapperService;
        private readonly IDbDummyData dbInitializer;

        private List<Contact> contacts;
        private List<Group> groups;

        public DbAccessMock(IDataMapperService dataMapperService)
        {
            this.dataMapperService = dataMapperService;
            dbInitializer = new DbDummyData();
        }

        public void AddContact(Contact contact)
        {
            contact.Id = contacts.SafeMax(c => c.Id) + 1;
            contacts.Add(contact);
        }

        public void AddGroup(Group group)
        {
            group.Id = groups.SafeMax(c => c.Id) + 1;
            groups.Add(group);
        }

        public void DeleteContact(int id)
        {
            contacts.Remove(contacts.Single(c => c.Id == id));
        }

        public void DeleteGroup(int id)
        {
            groups.Remove(groups.Single(c => c.Id == id));
        }

        public void EditContact(Contact contact)
        {
            var edited = contacts.Single(c => c.Id == contact.Id);
            dataMapperService.Map(contact, edited);
        }

        public void EditGroup(Group group)
        {
            var edited = groups.Single(g => g.Id == group.Id);
            dataMapperService.Map(group, edited);
        }

        public IEnumerable<Contact> GetContacts(Group group, bool favourites)
        {
            return contacts.Where(c => (group == null || c.Group.Id == group.Id) && (!favourites || c.Favourite))
                .OrderBy(c => c.Title)
                .ToList();
        }

        public IEnumerable<Group> GetGroups()
        {
            return groups.OrderBy(g => g.Id).ToList();
        }

        public Group GetDefaultGroup()
        {
            return groups.Single(g => g.Default);
        }

        public bool GroupExists(string groupName)
        {
            return groups.Any(g => g.Name == groupName);
        }

        public void Initialize()
        {
            contacts = new List<Contact>();
            groups = new List<Group>();
        }

        public void AddDummyData()
        {
            Clear();
            dbInitializer.CreateGroups().ForEach(AddGroup);
            contacts.AddRange(dbInitializer.CreateContacts(groups));
        }

        public void Clear()
        {
            contacts.Clear();
            groups.Clear();
            groups.Add(dbInitializer.CreateDefaultGroup());
        }
    }
}
