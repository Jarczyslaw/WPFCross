using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DataAccess.Models;

namespace DataAccess.Core
{
    public class DbDataAccessMock : IDbDataAccess
    {
        private List<Contact> contacts = new List<Contact>();
        private List<Group> groups = new List<Group>();

        private int contactsId;
        private int contactItemsId;
        private int groupsId;

        public DbDataAccessMock()
        {
            InitializeGroups();
            InitializeContacts();
        }

        public void AddContact(Contact contact)
        {
            contact.Id = contacts.Max(c => c.Id) + 1;
            contacts.Add(contact);
        }

        public void AddGroup(Group group)
        {
            group.Id = groups.Max(c => c.Id) + 1;
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
            edited.Favourite = contact.Favourite;
            edited.Group = contact.Group;
            edited.Items = contact.Items;
            edited.Name = contact.Name;
            edited.Title = contact.Title;
        }

        public void EditGroup(Group group)
        {
            var edited = groups.Single(g => g.Id == group.Id);
            edited.Default = group.Default;
            edited.Name = group.Name;
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

        private void InitializeGroups()
        {
            groups.Add(new Group
            {
                Id = ++groupsId,
                Default = true,
                Name = "General"
            });
            groups.Add(new Group
            {
                Id = ++groupsId,
                Default = false,
                Name = "Family"
            });
            groups.Add(new Group
            {
                Id = ++groupsId,
                Default = false,
                Name = "Friends"
            });
            groups.Add(new Group
            {
                Id = ++groupsId,
                Default = false,
                Name = "Work"
            });
        }

        private void InitializeContacts()
        {
            contacts.Add(new Contact
            {
                Id = ++contactsId,
                Title = "Mum",
                Name = "Mother",
                Favourite = true,
                Group = groups.Single(g => g.Name == "Family"),
                Items = new List<ContactItem>
                {
                    new ContactItem
                    {
                        Id = ++contactItemsId,
                        Type = ContactItemType.Phone,
                        Value = "111 111 111"
                    }
                }
            });
            contacts.Add(new Contact
            {
                Id = ++contactsId,
                Title = "Dad",
                Name = "Father",
                Favourite = true,
                Group = groups.Single(g => g.Name == "Family"),
                Items = new List<ContactItem>
                {
                    new ContactItem
                    {
                        Id = ++contactItemsId,
                        Type = ContactItemType.Phone,
                        Value = "222 222 222"
                    }
                }
            });
            contacts.Add(new Contact
            {
                Id = ++contactsId,
                Title = "Mark",
                Name = "Mark Zandberg",
                Favourite = true,
                Group = groups.Single(g => g.Name == "Friends"),
                Items = new List<ContactItem>
                {
                    new ContactItem
                    {
                        Id = ++contactItemsId,
                        Type = ContactItemType.Phone,
                        Value = "333 333 333"
                    },
                    new ContactItem
                    {
                        Id = ++contactItemsId,
                        Type = ContactItemType.Email,
                        Value = "mark.zandberg@mail.com"
                    }
                }
            });
            contacts.Add(new Contact
            {
                Id = ++contactsId,
                Title = "Janek",
                Name = "Jan Kowalski",
                Favourite = false,
                Group = groups.Single(g => g.Name == "General"),
                Items = new List<ContactItem>
                {
                    new ContactItem
                    {
                        Id = ++contactItemsId,
                        Type = ContactItemType.Phone,
                        Value = "444 444 444"
                    },
                    new ContactItem
                    {
                        Id = ++contactItemsId,
                        Type = ContactItemType.Email,
                        Value = "jan.kowalski@mail.com"
                    },
                    new ContactItem
                    {
                        Id = ++contactItemsId,
                        Type = ContactItemType.Website,
                        Value = "www.kowalski.com"
                    }
                }
            });
            contacts.Add(new Contact
            {
                Id = ++contactsId,
                Title = "Jurek",
                Name = "Jerzy Nowak",
                Favourite = false,
                Group = groups.Single(g => g.Name == "General"),
                Items = new List<ContactItem>
                {
                    new ContactItem
                    {
                        Id = ++contactItemsId,
                        Type = ContactItemType.Email,
                        Value = "jerzy.nowak@mail.com"
                    },
                }
            });
        }
    }
}
