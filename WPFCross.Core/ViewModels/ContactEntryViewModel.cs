using DataAccess.Models;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace WPFCross.Core.ViewModels
{
    public class ContactEntryViewModel : MvxViewModel
    {
        private string contactEntryValue;
        private ContactEntryType contactEntryType;

        public ContactEntryViewModel() : this(new ContactEntry())
        {
        }

        public ContactEntryViewModel(ContactEntry contactEntry)
        {
            ContactEntryValue = contactEntry.Value;
            ContactEntryType = contactEntry.Type;
        }

        public ContactEntryType ContactEntryType
        {
            get => contactEntryType;
            set => SetProperty(ref contactEntryType, value);
        }

        public string ContactEntryValue
        {
            get => contactEntryValue;
            set => SetProperty(ref contactEntryValue, value);
        }

        public ContactEntry GetContact()
        {
            return new ContactEntry
            {
                Type = ContactEntryType,
                Value = ContactEntryValue
            };
        }
    }
}
