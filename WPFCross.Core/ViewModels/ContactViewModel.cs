using DataAccess.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using Service.Core;
using Service.Dialogs;
using Service.Logger;
using System;
using System.Collections.ObjectModel;
using WPFCross.Core.ViewModels.Base;
using WPFCross.Extensions;

namespace WPFCross.Core.ViewModels
{
    public class ContactViewModel : InputOutputViewModelBase<Contact, bool>
    {
        private bool favourite;
        private string title, name;
        private GroupItemViewModel selectedGroup;
        private ObservableCollection<GroupItemViewModel> groups;

        private readonly IGroupsService groupsService;
        private readonly IContactsService contactsService;

        public ContactViewModel(IMvxNavigationService navigationService, ILoggerService loggingService, IDialogsService dialogsService,
            IGroupsService groupsService, IContactsService contactsService)
            : base(navigationService, loggingService, dialogsService)
        {
            this.groupsService = groupsService;
            this.contactsService = contactsService;

            SaveCommand = new MvxCommand(Save);
            CloseCommand = new MvxCommand(Close);
            EditGroupsCommand = new MvxCommand(EditGroups);
        }

        public IMvxCommand SaveCommand { get; }
        public IMvxCommand CloseCommand { get; }
        public IMvxCommand EditGroupsCommand { get; }

        public bool Favourite
        {
            get => favourite;
            set => SetProperty(ref favourite, value);
        }

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public ObservableCollection<GroupItemViewModel> Groups
        {
            get => groups;
            set => SetProperty(ref groups, value);
        }

        public GroupItemViewModel SelectedGroup
        {
            get => selectedGroup;
            set => SetProperty(ref selectedGroup, value);
        }

        private void Close()
        {
            CloseWithResult(false);
        }

        private void Save()
        {
            CloseWithResult(true);
        }

        private async void EditGroups()
        {
            await navigationService.NavigateWithCallback<GroupsViewModel, bool>((changed) =>
            {
                if (changed)
                {
                    
                }
            }).ConfigureAwait(false);
        }

        protected override void InitializeInput(Contact input)
        {
            if (input == null)
                return;

            Title = input.Title;
            Name = input.Title;
            Favourite = input.Favourite;
        }
    }
}
