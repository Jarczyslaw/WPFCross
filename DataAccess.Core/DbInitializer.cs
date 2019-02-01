using DataAccess.Models;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Core
{
    public class DbInitializer : IDbInitializer
    {
        private string familyGroup = "Family";
        private string workGroup = "Work";
        private string friendsGroup = "Friends";
        private string generalGroup = "General";

        private Group FindGroup(IEnumerable<Group> groups, string groupName)
        {
            var group = groups.SingleOrDefault(g => g.Name == groupName);
            return group == null ? groups.Single(g => g.Default) : group;
        }

        public List<Contact> CreateContacts(IEnumerable<Group> groups)
        {
            return new List<Contact>
            {
                new Contact
                {
                    Title = "Mum",
                    Name = "Mother",
                    Favourite = true,
                    Group = FindGroup(groups, familyGroup),
                    Items = new List<ContactEntry>
                    {
                        new ContactEntry
                        {
                            Type = ContactEntryType.Phone,
                            Value = "111 111 111"
                        }
                    }
                },
                new Contact
                {
                    Title = "Dad",
                    Name = "Father",
                    Favourite = true,
                    Group = FindGroup(groups, familyGroup),
                    Items = new List<ContactEntry>
                    {
                        new ContactEntry
                        {
                            Id = 0,
                            Type = ContactEntryType.Phone,
                            Value = "222 222 222"
                        }
                    }
                },
                new Contact
                {
                    Title = "Mark",
                    Name = "Mark Zandberg",
                    Favourite = true,
                    Group = FindGroup(groups, friendsGroup),
                    Items = new List<ContactEntry>
                    {
                        new ContactEntry
                        {
                            Id = 0,
                            Type = ContactEntryType.Phone,
                            Value = "333 333 333"
                        },
                        new ContactEntry
                        {
                            Id = 1,
                            Type = ContactEntryType.Email,
                            Value = "mark.zandberg@mail.com"
                        }
                    }
                },
                new Contact
                {
                    Title = "Janek",
                    Name = "Jan Kowalski",
                    Favourite = false,
                    Group = FindGroup(groups, generalGroup),
                    Items = new List<ContactEntry>
                    {
                        new ContactEntry
                        {
                            Id = 0,
                            Type = ContactEntryType.Phone,
                            Value = "444 444 444"
                        },
                        new ContactEntry
                        {
                            Id = 1,
                            Type = ContactEntryType.Email,
                            Value = "jan.kowalski@mail.com"
                        },
                        new ContactEntry
                        {
                            Id = 2,
                            Type = ContactEntryType.Website,
                            Value = "www.kowalski.com"
                        }
                    }
                },
                new Contact
                {
                    Title = "Jurek",
                    Name = "Jerzy Nowak",
                    Favourite = false,
                    Group = FindGroup(groups, generalGroup),
                    Items = new List<ContactEntry>
                    {
                        new ContactEntry
                        {
                            Id = 0,
                            Type = ContactEntryType.Email,
                            Value = "jerzy.nowak@mail.com"
                        },
                    }
                }
            };
        }

        public List<Group> CreateGroups()
        {
            return new List<Group>
            {
                new Group
                {
                    Default = false,
                    Name = familyGroup
                },
                new Group
                {
                    Default = false,
                    Name = friendsGroup
                },
                new Group
                {
                    Default = false,
                    Name = workGroup
                }
            };
        }

        public Group CreateDefaultGroup()
        {
            return new Group
            {
                Default = true,
                Name = generalGroup
            };
        }
    }
}
