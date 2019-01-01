﻿using Commons;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Core
{
    public interface IGroupsService
    {
        ValueResult<IEnumerable<Group>> GetGroups();
        Result AddGroup(Group group);
        Result EditGroup(Group group);
        Result DeleteGroup(Group group);
        Result CanDelete(Group group);
    }
}
