using DataAccess.Models;
using MvvmCross.ViewModels;

namespace WPFCross.Core.ViewModels
{
    public class ContactEntryViewModel : MvxViewModel
    {
        private int contactEntryId;
        private string contactEntryValue;
        private ContactEntryType contactEntryType;

        public ContactEntryViewModel(ContactEntry contactEntry)
        {
            ContactEntryId = contactEntry.Id;
            ContactEntryValue = contactEntry.Value;
            ContactEntryType = contactEntry.Type;
        }

        public int ContactEntryId
        {
            get => contactEntryId;
            set => SetProperty(ref contactEntryId, value);
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
