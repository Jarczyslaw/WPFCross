using Commons;
using DataAccess.Models;
using System.Collections.Generic;

namespace Service.Core
{
    public interface IGroupsService
    {
        bool ValidateGroupName(string groupName);
        ValueResult<IEnumerable<Group>> GetGroups();
        Result AddGroup(Group group);
        Result EditGroup(Group group);
        Result DeleteGroup(Group group);
        Result CanDelete(Group group);
    }
}
