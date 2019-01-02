using DataAccess.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using Service.Core;
using Service.Dialogs;
using Service.Logger;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using WPFCross.Core.ViewModels.Base;
using WPFCross.Extensions;

namespace WPFCross.Core.ViewModels
{
    public class ContactViewModel : InputOutputViewModelBase<Contact, bool>
    {
        private bool addNew;
        private bool favourite;
        private string title, name;
        private GroupItemViewModel selectedGroup;
        private ObservableCollection<GroupItemViewModel> groups;
        private ObservableCollection<ContactEntryViewModel> contactEntries;

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
            AddEntryCommand = new MvxCommand(AddEntry);
            RemoveEntryCommand = new MvxCommand(RemoveEntry);

            LoadGroups();
        }

        public IMvxCommand SaveCommand { get; }
        public IMvxCommand CloseCommand { get; }
        public IMvxCommand EditGroupsCommand { get; }
        public IMvxCommand AddEntryCommand { get; }
        public IMvxCommand RemoveEntryCommand { get; }

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

        public ObservableCollection<ContactEntryViewModel> ContactEntries
        {
            get => contactEntries;
            set => SetProperty(ref contactEntries, value);
        }

        private void Close()
        {
            CloseWithResult(false);
        }

        private void Save()
        {
            var contact = new Contact
            {
                Favourite = Favourite,
                Group = SelectedGroup.Group,
                Name = Name,
                Title = Title
            };
            AddOrSave(contact);
        }

        private void AddOrSave(Contact contact)
        {
            try
            {
                var result = contactsService.AddContact(contact);
                if (!result.IsSuccess)
                {
                    dialogsService.ShowError(result.Errors.First().Content);
                    return;
                }

                CloseWithResult(true);
            }
            catch (Exception exc)
            {
                dialogsService.ShowException("Exception during adding contact", exc);
            }
        }

        private async void EditGroups()
        {
            await navigationService.NavigateWithCallback<GroupsViewModel, bool>((changed) =>
            {
                if (changed)
                {
                    LoadGroups(SelectedGroup.Group.Id);
                }
            }).ConfigureAwait(false);
        }

        private void LoadGroups(int? id = null)
        {
            try
            {
                var result = groupsService.GetGroups();
                Groups = new ObservableCollection<GroupItemViewModel>(result.Value.Select(g => new GroupItemViewModel(g)));
                if (id == null)
                    SelectedGroup = Groups.FirstOrDefault();
                else
                    SelectedGroup = Groups.SingleOrDefault(g => g.Group.Id == id);
            }
            catch (Exception exc)
            {
                dialogsService.ShowException("Exception during loading groups", exc);
            }
        }

        public override void Prepare(Contact parameter)
        {
            addNew = parameter == null;
            if (addNew)
                return;

            LoadContact(parameter);
        }

        private void LoadContact(Contact contact)
        {
            Title = contact.Title;
            Name = contact.Title;
            Favourite = contact.Favourite;
            SelectedGroup = Groups.FirstOrDefault(g => g.Group.Id == contact.Group.Id);
            LoadEntries(contact);
        }

        private void RemoveEntry()
        {
            throw new NotImplementedException();
        }

        private void AddEntry()
        {
            throw new NotImplementedException();
        }

        private void LoadEntries(Contact contact)
        {
            ContactEntries = new ObservableCollection<ContactEntryViewModel>(contact.Items
                .Select(c => new ContactEntryViewModel(c)));
        }
    }
}
