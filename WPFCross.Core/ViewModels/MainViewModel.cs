using DataAccess.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using Service.Core;
using Service.Dialogs;
using Service.Logger;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using WPFCross.Core.ViewModels.Base;
using WPFCross.Extensions;

namespace WPFCross.Core.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private bool favourites;
        private GroupItemViewModel allGroups;
        private ContactItemViewModel selectedContact;
        private GroupItemViewModel selectedGroup;
        private ObservableCollection<ContactItemViewModel> contacts;
        private ObservableCollection<GroupItemViewModel> groups;

        private readonly IGroupsService groupsService;
        private readonly IContactsService contactsService;

        public MainViewModel(IMvxNavigationService navigationService, ILoggerService loggingService, IDialogsService dialogsService,
            IContactsService contactsService, IGroupsService groupsService)
            : base(navigationService, loggingService, dialogsService)
        {
            this.contactsService = contactsService;
            this.groupsService = groupsService;

            AddNewCommand = new MvxCommand(AddNewContact);
            EditCommand = new MvxCommand(EditContact, () => EditionEnabled);
            DeleteCommand = new MvxCommand(DeleteContact, () => EditionEnabled);
            EditGroupsCommand = new MvxCommand(EditGroups);

            LoadAll();
        }

        public IMvxCommand AddNewCommand { get; }
        public IMvxCommand EditCommand { get; }
        public IMvxCommand DeleteCommand { get; }
        public IMvxCommand EditGroupsCommand { get; }

        public bool EditionEnabled
        {
            get => SelectedContact != null;
        }

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
            set
            {
                SetProperty(ref selectedContact, value);
                EditCommand.RaiseCanExecuteChanged();
                DeleteCommand.RaiseCanExecuteChanged();
            }
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

        private async void AddNewContact()
        {
            await OpenContactViewModel(null)
                .ConfigureAwait(false);
        }

        private async void EditContact()
        {
            if (SelectedContact == null)
                return;

            await OpenContactViewModel(SelectedContact.Contact)
                .ConfigureAwait(false);
        }

        private async Task OpenContactViewModel(Contact contact)
        {
            var refresh = await navigationService.Navigate<ContactViewModel, Contact, bool>(contact)
                .ConfigureAwait(false);

            if (refresh)
            {
                LoadAll();
            }
        }

        private void DeleteContact()
        {
            if (SelectedContact == null)
            {
                return;
            }

            var dialogResult = dialogsService.ShowYesNoQuestion($"Do you really want to remove {SelectedContact.Title}?");
            if (!dialogResult)
            {
                return;
            }

            try
            {

            }
            catch (Exception exc)
            {
                dialogsService.ShowException("Exception during deleting contact", exc);
            }
        }

        private async void EditGroups()
        {
            await navigationService.NavigateWithCallback<GroupsViewModel, bool>((refresh) =>
            {
                if (refresh)
                {
                    LoadAll();
                }
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

            var result = groupsService.GetGroups();
            foreach (var group in result.Value)
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
            try
            {
                LoadContacts();
                LoadGroups();
            }
            catch (Exception exc)
            {
                dialogsService.ShowException("Exception during loading data", exc);
            }
        }

        private void LoadContacts()
        {
            var result = contactsService.GetContacts(SelectedGroup?.Group, Favourites);
            Contacts = new ObservableCollection<ContactItemViewModel>(result.Value
                .Select(c => new ContactItemViewModel(c)));
        }
    }
}
