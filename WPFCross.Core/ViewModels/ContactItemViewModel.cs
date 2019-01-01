using DataAccess.Models;

namespace WPFCross.Core.ViewModels
{
    public class ContactItemViewModel
    {
        public string Initials => string.IsNullOrEmpty(Title) ? "C" : Title[0].ToString();

        public string Title => Contact.Title;

        public Contact Contact { get; set; }

        public ContactItemViewModel(Contact contact)
        {
            Contact = contact;
        }
    }
}
