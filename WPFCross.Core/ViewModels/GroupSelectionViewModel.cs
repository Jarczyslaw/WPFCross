using DataAccess.Core;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;

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

        private readonly IMvxNavigationService navigationService;
        private readonly IDbDataAccess dbDataAccess;

        public GroupSelectionViewModel(IMvxNavigationService navigationService, IDbDataAccess dbDataAccess)
        {
            this.navigationService = navigationService;
            this.dbDataAccess = dbDataAccess;

            EditGroupsCommand = new MvxCommand(EditGroups);

            Groups = new ObservableCollection<GroupItemViewModel>(
                dbDataAccess.GetGroups().Select(g => new GroupItemViewModel
                {
                    Name = g.Name
                }));
        }

        private void EditGroups()
        {
            navigationService.Navigate<GroupsViewModel>();
        }
    }
}
