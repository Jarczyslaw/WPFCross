using DataAccess.Models;
using MvvmCross.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace WPFCross.Core.ViewModels
{
    public class ContactEntryViewModel : MvxViewModel
    {
        private ContactEntry contactEntry;

        public ContactEntryViewModel() : this(new ContactEntry())
        {
        }

        public ContactEntryViewModel(ContactEntry contactEntry)
        {
            ContactEntry = contactEntry;
        }

        public ContactEntry ContactEntry
        {
            get => contactEntry;
            set => SetProperty(ref contactEntry, value);
        }

        public ContactEntryType ContactEntryType
        {
            get => ContactEntry.Type;
            set
            {
                ContactEntry.Type = value;
                RaisePropertyChanged(nameof(ContactEntryType));
            }
        }

        public string Value
        {
            get => ContactEntry.Value;
            set
            {
                ContactEntry.Value = value;
                RaisePropertyChanged(nameof(Value));
            }
        }
    }
}
