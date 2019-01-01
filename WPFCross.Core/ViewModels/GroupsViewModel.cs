﻿using DataAccess.Core;
using DataAccess.Models;
using MvvmCross.Commands;
using Service.Core;
using Service.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace WPFCross.Core.ViewModels
{
    public class GroupsViewModel : CallBackViewModel<bool>
    {
        private bool dataChanged;
        private string groupName;
        private GroupItemViewModel selectedGroup;
        private ObservableCollection<GroupItemViewModel> groups;

        private readonly IGroupsService groupsService;
        private readonly IDialogsService dialogsService;

        public GroupsViewModel(IDialogsService dialogsService, IGroupsService groupsService)
        {
            this.groupsService = groupsService;
            this.dialogsService = dialogsService;

            AddNewCommand = new MvxCommand(AddNew);
            EditCommand = new MvxCommand(Edit);
            DeleteCommand = new MvxCommand(Delete);

            LoadGroups();
        }

        public IMvxCommand AddNewCommand { get; }
        public IMvxCommand EditCommand { get; }
        public IMvxCommand DeleteCommand { get; }

        public ObservableCollection<GroupItemViewModel> Groups
        {
            get => groups;
            set => SetProperty(ref groups, value);
        }

        public string GroupName
        {
            get => groupName;
            set => SetProperty(ref groupName, value);
        }

        public GroupItemViewModel SelectedGroup
        {
            get => selectedGroup;
            set
            {
                SetProperty(ref selectedGroup, value);
                GroupName = selectedGroup?.Name;
            }
        }

        public void LoadGroups(Group selected = null)
        {
            try
            {
                var result = groupsService.GetGroups();
                Groups = new ObservableCollection<GroupItemViewModel>(result.Value.Select(g => new GroupItemViewModel
                {
                    Name = g.Name,
                    Group = g
                }));

                if (selected == null)
                    SelectedGroup = Groups.FirstOrDefault();
                else
                    SelectedGroup = Groups.FirstOrDefault(g => g.Group.Id == selected.Id);
            }
            catch (Exception exc)
            {
                dialogsService.ShowException("Exception during loading groups", exc);
            }
        }

        private void AddNew()
        {
            var newGroup = new Group
            {
                Name = GroupName
            };

            try
            {
                var result = groupsService.AddGroup(newGroup);
                if (!result.IsSuccess)
                {
                    dialogsService.ShowError(result.Errors.First().Content);
                    return;
                }

                dataChanged = true;
                LoadGroups(newGroup);
            }
            catch (Exception exc)
            {
                dialogsService.ShowException("Exception during adding new group", exc);
            }
        }

        private void Edit()
        {
            try
            {
                var edited = new Group(SelectedGroup.Group)
                {
                    Name = GroupName
                };

                var result = groupsService.EditGroup(edited);
                if (!result.IsSuccess)
                {
                    dialogsService.ShowError(result.Errors.First().Content);
                    return;
                }

                dataChanged = true;
                LoadGroups(edited);
            }
            catch (Exception exc)
            {
                dialogsService.ShowException("Exception during editing group", exc);
            }
        }

        private void Delete()
        {
            var canDelete = groupsService.CanDelete(SelectedGroup.Group);
            if (!canDelete.IsSuccess)
            {
                dialogsService.ShowError(canDelete.Errors.First().Content);
                return;
            }

            var dialogResult = dialogsService.ShowYesNoQuestion($"Do you really want to remove {SelectedGroup.Name}?");
            if (!dialogResult)
                return;

            try
            {
                var result = groupsService.DeleteGroup(SelectedGroup.Group);
                if (!result.IsSuccess)
                {
                    dialogsService.ShowError(result.Errors.First().Content);
                    return;
                }

                if (result.Infos.Any())
                    dialogsService.ShowInfo(result.Infos.First().Content);

                dataChanged = true;
                LoadGroups();
            }
            catch (Exception exc)
            {
                dialogsService.ShowException("Exception during deleting group", exc);
            }
        }

        public override void ViewDestroy(bool viewFinishing = true)
        {
            Callback?.Invoke(dataChanged);
            base.ViewDestroy(viewFinishing);
        }
    }
}
