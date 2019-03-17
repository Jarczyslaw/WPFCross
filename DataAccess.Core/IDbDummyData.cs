using DataAccess.Models;
using System.Collections.Generic;

namespace DataAccess.Core
{
    public interface IDbDummyData
    {
        List<Contact> CreateContacts(IEnumerable<Group> groups);
        List<Group> CreateGroups();
        Group CreateDefaultGroup();
    }
}
