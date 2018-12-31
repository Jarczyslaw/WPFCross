using DataAccess.Core;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Service.Dialogs;
using Service.Logger;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using WPFCross.Extensions;

namespace WPFCross.Core.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        private bool favourites;
        private GroupItemViewModel allGroups;
        private ContactItemViewModel selectedContact;
        private GroupItemViewModel selectedGroup;
        private ObservableCollection<ContactItemViewModel> contacts;
        private ObservableCollection<GroupItemViewModel> groups;

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

            LoadAll();
        }

        public IMvxCommand AddNewCommand { get; }
        public IMvxCommand EditCommand { get; }
        public IMvxCommand DeleteCommand { get; }
        public IMvxCommand EditGroupsCommand { get; }

        public ObservableCollection<ContactItemViewModel> Contacts
        {
            get => contacts;
            set => SetProperty(ref contacts, value);
        }

        public ObservableCollection<GroupItemViewModel> Groups
        {
            get => groups;
            set => SetProperty(ref groups, value);
        }

        public bool Favourites
        {
            get => favourites;
            set
            {
                SetProperty(ref favourites, value);
                LoadContacts();
            }
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
                LoadContacts();
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

        private async void EditGroups()
        {
            await navigationService.NavigateWithCallback<GroupsViewModel, bool>((changed) =>
            {
                if (changed)
                    LoadAll();
            }).ConfigureAwait(false);
        }

        private void LoadGroups()
        {
            allGroups = new GroupItemViewModel
            {
                Name = "All"
            };
            Groups = new ObservableCollection<GroupItemViewModel>
            {
                allGroups
            };

            foreach (var group in dbDataAccess.GetGroups())
            {
                Groups.Add(new GroupItemViewModel
                {
                    Name = group.Name,
                    Group = group
                });
            }
            SelectedGroup = allGroups;
        }

        private void LoadAll()
        {
            LoadContacts();
            LoadGroups();
        }

        private void LoadContacts()
        {
            Contacts = new ObservableCollection<ContactItemViewModel>(
                dbDataAccess.GetContacts(SelectedGroup?.Group, Favourites).Select(c => new ContactItemViewModel
                {
                    Title = c.Title
                }));
        }
    }
}
