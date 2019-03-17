using DataAccess.Models;

namespace WPFCross.Core.ViewModels
{
    public class GroupItemViewModel
    {
        public string Name { get; set; }
        public Group Group { get; set; }

        public GroupItemViewModel()
        {
        }

        public GroupItemViewModel(Group group)
        {
            Group = group;
            Name = group.Name;
        }
    }
}
