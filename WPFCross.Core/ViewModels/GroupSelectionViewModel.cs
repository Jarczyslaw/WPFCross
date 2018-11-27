using MvvmCross.Commands;
using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace WPFCross.Core.ViewModels
{
    public class GroupSelectionViewModel : ViewModelBase
    {
        private event Action<GroupItemViewModel> onGroupSelected;
        public event Action<GroupItemViewModel> OnGroupSelected
        {
            add { onGroupSelected += value; }
            remove { onGroupSelected -= value; }
        }

        private GroupItemViewModel selectedGroup;
        public GroupItemViewModel SelectedGroup
        {
            get => selectedGroup;
            set
            {
                SetProperty(ref selectedGroup, value);
                onGroupSelected?.Invoke(selectedGroup);
            }
        }

        public ObservableCollection<GroupItemViewModel> Groups { get; private set; }

        public IMvxCommand EditGroupsCommand { get; private set; }

        private IMvxNavigationService navigationService;

        public GroupSelectionViewModel(IMvxNavigationService navigationService)
        {
            this.navigationService = navigationService;

            EditGroupsCommand = new MvxCommand(EditGroups);

            Groups = new ObservableCollection<GroupItemViewModel>
            {
                new GroupItemViewModel { Name = "Group 1" },
                new GroupItemViewModel { Name = "Group 2" },
                new GroupItemViewModel { Name = "Group 3" }
            };
        }

        private void EditGroups()
        {
            navigationService.Navigate<GroupsViewModel>();
        }
    }
}
