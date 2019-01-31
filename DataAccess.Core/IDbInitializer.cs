using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Core
{
    internal interface IDbInitializer
    {
        List<Contact> CreateContacts(IEnumerable<Group> groups);
        List<Group> CreateGroups();
        Group CreateDefaultGroup();
    }
}
