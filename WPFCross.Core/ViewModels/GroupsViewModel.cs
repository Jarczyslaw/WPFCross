using DataAccess.Core;
using MvvmCross.Commands;
using Service.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace WPFCross.Core.ViewModels
{
    public class GroupsViewModel : ViewModelBase
    {
        private string groupName;
        private GroupItemViewModel selectedGroup;

        private readonly IDbDataAccess dbDataAccess;
        private readonly IDialogsService dialogsService;

        public GroupsViewModel(IDialogsService dialogsService, IDbDataAccess dbDataAccess)
        {
            this.dbDataAccess = dbDataAccess;
            this.dialogsService = dialogsService;

            AddNewCommand = new MvxCommand(AddNew);
            EditCommand = new MvxCommand(Edit);
            DeleteCommand = new MvxCommand(Delete);

            Groups = new ObservableCollection<GroupItemViewModel>(dbDataAccess.GetGroups().Select(g => new GroupItemViewModel
            {
                Name = g.Name
            }));

            SelectedGroup = Groups.FirstOrDefault();   
        }

        public IMvxCommand AddNewCommand { get; private set; }
        public IMvxCommand EditCommand { get; private set; }
        public IMvxCommand DeleteCommand { get; private set; }

        public ObservableCollection<GroupItemViewModel> Groups { get; private set; }

        public GroupItemViewModel SelectedGroup
        {
            get => selectedGroup;
            set
            {
                SetProperty(ref selectedGroup, value);
                GroupName = selectedGroup.Name;
            }
        }

        public string GroupName
        {
            get => groupName;
            set => SetProperty(ref groupName, value);
        }

        private void AddNew()
        {
            throw new NotImplementedException();
        }

        private void Edit()
        {
            throw new NotImplementedException();
        }

        private void Delete()
        {
            throw new NotImplementedException();
        }
    }
}
