using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DataAccess.Core
{
    internal class DbInitializer : IDbInitializer
    {
        private Group FindGroup(IEnumerable<Group> groups, string groupName)
        {
            var group = groups.SingleOrDefault(g => g.Name == groupName);
            return group == null ? groups.Single(g => g.Default) : group;
        }

        public List<Contact> CreateContacts(IEnumerable<Group> groups, int initialContactId = 0)
        {
            int contactItemsId = 0;
            return new List<Contact>
            {
                new Contact
                {
                    Id = ++initialContactId,
                    Title = "Mum",
                    Name = "Mother",
                    Favourite = true,
                    Group = FindGroup(groups, "Family"),
                    Items = new List<ContactEntry>
                {
                    new ContactEntry
                    {
                        Id = ++initialContactId,
                        Type = ContactEntryType.Phone,
                        Value = "111 111 111"
                    }
                }
                },
                new Contact
                {
                    Id = ++initialContactId,
                    Title = "Dad",
                    Name = "Father",
                    Favourite = true,
                    Group = FindGroup(groups, "Family"),
                    Items = new List<ContactEntry>
                {
                    new ContactEntry
                    {
                        Id = ++contactItemsId,
                        Type = ContactEntryType.Phone,
                        Value = "222 222 222"
                    }
                }
                },
                new Contact
                {
                    Id = ++initialContactId,
                    Title = "Mark",
                    Name = "Mark Zandberg",
                    Favourite = true,
                    Group = FindGroup(groups, "Friends"),
                    Items = new List<ContactEntry>
                {
                    new ContactEntry
                    {
                        Id = ++contactItemsId,
                        Type = ContactEntryType.Phone,
                        Value = "333 333 333"
                    },
                    new ContactEntry
                    {
                        Id = ++contactItemsId,
                        Type = ContactEntryType.Email,
                        Value = "mark.zandberg@mail.com"
                    }
                }
                },
                new Contact
                {
                    Id = ++initialContactId,
                    Title = "Janek",
                    Name = "Jan Kowalski",
                    Favourite = false,
                    Group = FindGroup(groups, "General"),
                    Items = new List<ContactEntry>
                {
                    new ContactEntry
                    {
                        Id = ++contactItemsId,
                        Type = ContactEntryType.Phone,
                        Value = "444 444 444"
                    },
                    new ContactEntry
                    {
                        Id = ++contactItemsId,
                        Type = ContactEntryType.Email,
                        Value = "jan.kowalski@mail.com"
                    },
                    new ContactEntry
                    {
                        Id = ++contactItemsId,
                        Type = ContactEntryType.Website,
                        Value = "www.kowalski.com"
                    }
                }
                },
                new Contact
                {
                    Id = ++initialContactId,
                    Title = "Jurek",
                    Name = "Jerzy Nowak",
                    Favourite = false,
                    Group = FindGroup(groups, "General"),
                    Items = new List<ContactEntry>
                {
                    new ContactEntry
                    {
                        Id = ++contactItemsId,
                        Type = ContactEntryType.Email,
                        Value = "jerzy.nowak@mail.com"
                    },
                }
                }
            };
        }

        public List<Group> CreateGroups(int initialGroupId = 0)
        {
            return new List<Group>
            {
                CreateDefaultGroup(),
                new Group
                {
                    Id = ++initialGroupId,
                    Default = false,
                    Name = "Family"
                },
                new Group
                {
                    Id = ++initialGroupId,
                    Default = false,
                    Name = "Friends"
                },
                new Group
                {
                    Id = ++initialGroupId,
                    Default = false,
                    Name = "Work"
                }
            };
        }

        public Group CreateDefaultGroup(int groupId = 0)
        {
            return new Group
            {
                Id = groupId,
                Default = true,
                Name = "General"
            };
        }
    }
}
