using Service.Dialogs;
using Service.Logger;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WPFCross.Extensions;
using DataAccess.Core;
using System.Linq;

namespace WPFCross.Core.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private bool favourites;
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
            EditGroupsCommand = new MvxCommand(EditGroup);

            Contacts = new ObservableCollection<ContactItemViewModel>(
                dbDataAccess.GetContacts(null, false).Select(c => new ContactItemViewModel
                {
                    Title = c.Title
                }));
            Groups = new ObservableCollection<GroupItemViewModel>(dbDataAccess.GetGroups().Select(g => new GroupItemViewModel
                {
                    Name = g.Name
                }));
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
            set => SetProperty(ref selectedGroup, value);
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

        private void EditGroup()
        {
            throw new NotImplementedException();
        }
    }
}
