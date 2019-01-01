using MvvmCross.Commands;
using MvvmCross.Navigation;
using Service.Dialogs;
using Service.Logger;
using System;
using WPFCross.Core.ViewModels.Base;

namespace WPFCross.Core.ViewModels
{
    public class ContactViewModel : ViewModelBase
    {
        private string title, name;

        public ContactViewModel(IMvxNavigationService navigationService, ILoggerService loggingService, IDialogsService dialogsService)
            : base(navigationService, loggingService, dialogsService)
        {
            SaveCommand = new MvxCommand(Save);
            CloseCommand = new MvxCommand(Close);
            EditGroupsCommand = new MvxCommand(EditGroups);
        }

        public IMvxCommand SaveCommand { get; }
        public IMvxCommand CloseCommand { get; }
        public IMvxCommand EditGroupsCommand { get; }

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

        private void Close()
        {
            throw new NotImplementedException();
        }

        private void Save()
        {
            throw new NotImplementedException();
        }

        private void EditGroups()
        {
            /*await navigationService.NavigateWithCallback<GroupsViewModel, bool>((changed) =>
            {
                if (changed)
                {
                    LoadAll();
                }
            }).ConfigureAwait(false);*/
        }
    }
}
