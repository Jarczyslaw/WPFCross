using DataAccess.Core;
using DataAccess.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using Service.Dialogs;
using Service.Logger;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace WPFCross.Core.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private bool favourites;
        private GroupItemViewModel allGroups;
        private ContactItemViewModel selectedContact;
        private GroupItemViewModel selectedGroup;

        private readonly IMvxNavigationService navigationService;
        private readonly ILoggerService loggingService;
        private readonly IDialogsService dialogsService;
        private readonly IDbDataAccess dbDataAccess;

        public MainViewModel(IMvxNavigationService navigationService, ILoggerService loggingService,
            IDialogsService dialogsService, IDbDataAccess dbDataAccess)
        {
            this.navigationService = navigationService;
            this.loggingService = loggingService;
            this.dialogsService = dialogsService;
            this.dbDataAccess = dbDataAccess;

            AddNewCommand = new MvxCommand(AddNewContact);
            EditCommand = new MvxCommand(EditContact);
            DeleteCommand = new MvxCommand(DeleteContact);
            EditGroupsCommand = new MvxCommand(EditGroups);

            Contacts = new ObservableCollection<ContactItemViewModel>(
                dbDataAccess.GetContacts(null, false).Select(c => new ContactItemViewModel
                {
                    Title = c.Title
                }));

            InitializeGroups();
        }



        public IMvxCommand AddNewCommand { get; private set; }
        public IMvxCommand EditCommand { get; private set; }
        public IMvxCommand DeleteCommand { get; private set; }
        public IMvxCommand EditGroupsCommand { get; private set; }

        public ObservableCollection<ContactItemViewModel> Contacts { get; private set; }
        public ObservableCollection<GroupItemViewModel> Groups { get; private set; }

        public bool Favourites
        {
            get => favourites;
            set => SetProperty(ref favourites, value);
        }

        public ContactItemViewModel SelectedContact
        {
            get => selectedContact;
            set => SetProperty(ref selectedContact, value);
        }

        public GroupItemViewModel SelectedGroup
        {
            get => selectedGroup;
            set
            {
                SetProperty(ref selectedGroup, value);
            }
        }

        private void AddNewContact()
        {
            navigationService.Navigate<ContactViewModel>();
        }

        private void EditContact()
        {
            loggingService.Debug("TEST");
        }

        private void DeleteContact()
        {
            throw new Exception("asd");
        }

        private void EditGroups()
        {
            navigationService.Navigate<GroupsViewModel>();
        }

        private void InitializeGroups()
        {
            allGroups = new GroupItemViewModel { Name = "All" };
            Groups = new ObservableCollection<GroupItemViewModel> { allGroups };

            foreach (var group in dbDataAccess.GetGroups())
            {
                Groups.Add(new GroupItemViewModel
                {
                    Name = group.Name
                });
            }
            SelectedGroup = allGroups;
        }

        private void LoadContacts()
        {
            Contacts = new ObservableCollection<ContactItemViewModel>(
                dbDataAccess.GetContacts(null, false).Select(c => new ContactItemViewModel
                {
                    Title = c.Title
                }));
        }
    }
}
