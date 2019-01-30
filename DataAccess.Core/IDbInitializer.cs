using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Core
{
    internal interface IDbInitializer
    {
        List<Contact> CreateContacts(IEnumerable<Group> groups, int initialContactId = 0);
        List<Group> CreateGroups(int initialGroupId = 0);
        Group CreateDefaultGroup(int groupId = 0);
    }
}
