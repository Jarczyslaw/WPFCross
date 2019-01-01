using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace WPFCross.Core.ViewModels
{
    public class ContactViewModel : MvxViewModel
    {
        private string title, name;

        public ContactViewModel()
        {
            SaveCommand = new MvxCommand(Save);
            CancelCommand = new MvxCommand(Cancel);
        }

        public IMvxCommand SaveCommand { get; }
        public IMvxCommand CancelCommand { get; }

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

        private void Cancel()
        {
            throw new NotImplementedException();
        }

        private void Save()
        {
            throw new NotImplementedException();
        }
    }
}
