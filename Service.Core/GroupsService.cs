using Commons;
using DataAccess.Core;
using DataAccess.Models;
using System.Linq;

namespace Service.Core
{
    public class GroupsService : IGroupsService
    {
        private readonly IDbDataAccess dataAccess;

        public GroupsService(IDbDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        public Result AddGroup(Group group)
        {
            var result = new Result();

            if (!ValidateGroupName(group.Name))
            {
                result.Errors.Add("Invalid group name");
                return result;
            }

            var groups = dataAccess.GetGroups();
            if (groups.Any(g => g.Name == group.Name))
            {
                result.Errors.Add("Group with given name currently exists");
                return result;
            }

            dataAccess.AddGroup(group);
            return result;
        }

        public Result EditGroup(Group group)
        {
            var result = new Result();

            if (!ValidateGroupName(group.Name))
            {
                result.Errors.Add("Invalid group name");
                return result;
            }

            dataAccess.EditGroup(group);
            return result;
        }

        public Result DeleteGroup(Group group)
        {
            var result = new Result();

            if (group.Default)
            {
                result.Errors.Add("Can not delete default group");
                return result;
            }

            var contactsInGroup = dataAccess.GetContacts(group, false);
            if (contactsInGroup.Any())
            {
                var defaultGroup = dataAccess.GetDefaultGroup();
                foreach (var contact in contactsInGroup)
                {
                    contact.Group = defaultGroup;
                    dataAccess.EditContact(contact);
                }
                result.Infos.Add($"{contactsInGroup.Count()} contacts moved to {defaultGroup.Name} group");
            }

            dataAccess.DeleteGroup(group.Id);

            return result;
        }

        private bool ValidateGroupName(string groupName)
        {
            return !string.IsNullOrEmpty(groupName) && groupName.Length >= 3;
        }
    }
}
