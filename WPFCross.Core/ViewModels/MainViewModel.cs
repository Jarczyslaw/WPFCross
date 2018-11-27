using Service.Dialogs;
using Service.Logger;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WPFCross.Extensions;

namespace WPFCross.Core.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private bool showFavourites;
        public bool ShowFavourites
        {
            get => showFavourites;
            set => SetProperty(ref showFavourites, value);
        }

        private ContactViewModel selectedContact;
        public ContactViewModel SelectedContact
        {
            get => selectedContact;
            set => SetProperty(ref selectedContact, value);
        }

        public ObservableCollection<ContactViewModel> Contacts { get; private set; }

        public GroupSelectionViewModel GroupSelection { get; private set; }

        public IMvxCommand AddNewContactCommand { get; private set; }
        public IMvxCommand EditContactCommand { get; private set; }
        public IMvxCommand DeleteContactCommand { get; private set; }

        private readonly IMvxNavigationService navigationService;
        private readonly ILoggerService loggingService;
        private readonly IDialogsService dialogsService;
        
        public MainViewModel(IMvxNavigationService navigationService, ILoggerService loggingService, IDialogsService dialogsService)
        {
            this.navigationService = navigationService;
            this.loggingService = loggingService;
            this.dialogsService = dialogsService;

            AddNewContactCommand = new MvxCommand(AddNewContact);
            EditContactCommand = new MvxCommand(EditContact);
            DeleteContactCommand = new MvxCommand(DeleteContact);

            GroupSelection = Mvx.IoCProvider.RegisterTypeAndResolve<GroupSelectionViewModel>();
            GroupSelection.OnGroupSelected += GroupSelection_OnGroupSelected;

            Contacts = new ObservableCollection<ContactViewModel>
            {
                new ContactViewModel() { Title = "Jarek" },
                new ContactViewModel() { Title = "Tomek" },
                new ContactViewModel() { Title = "Romek" },
                new ContactViewModel() { Title = "Tymek" },
                new ContactViewModel() { Title = "Zenek" },
                new ContactViewModel() { Title = "Jarek" },
                new ContactViewModel() { Title = "Tomek" },
                new ContactViewModel() { Title = "Romek" },
                new ContactViewModel() { Title = "Tymek" },
                new ContactViewModel() { Title = "Zenek" },
                new ContactViewModel() { Title = "Jarek" },
                new ContactViewModel() { Title = "Tomek" },
                new ContactViewModel() { Title = "Romek" },
                new ContactViewModel() { Title = "Tymek" },
                new ContactViewModel() { Title = "Zenek" },
                new ContactViewModel() { Title = "Witek" }
            };
        }

        private void GroupSelection_OnGroupSelected(GroupItemViewModel groupItem)
        {
            dialogsService.ShowInfo(groupItem.Name);
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
    }
}
