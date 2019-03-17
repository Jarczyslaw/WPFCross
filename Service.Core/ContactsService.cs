using Commons;
using DataAccess.Core;
using DataAccess.Models;
using System.Collections.Generic;
using System.Linq;

namespace Service.Core
{
    public class ContactsService : IContactsService
    {
        private readonly IDbAccess dataAccess;
        private readonly int minLength = 3;

        public ContactsService(IDbAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        public ValueResult<IEnumerable<Contact>> GetContacts(Group group, bool favourites)
        {
            return new ValueResult<IEnumerable<Contact>>(dataAccess.GetContacts(group, favourites));
        }

        private bool CheckStringValue(string value)
        {
            return !string.IsNullOrEmpty(value) && value.Length >= minLength;
        }

        public bool ValidateContactName(string name)
        {
            return CheckStringValue(name);
        }

        public bool ValidateContactTitle(string title)
        {
            return CheckStringValue(title);
        }

        public bool ValidateContactEntry(ContactEntry entry)
        {
            return CheckStringValue(entry.Value);
        }

        public Result ValidateContact(Contact contact)
        {
            var result = new Result();

            if (!ValidateContactName(contact.Name))
            {
                result.Errors.Add("Invalid contact's name");
                return result;
            }

            if (!ValidateContactTitle(contact.Title))
            {
                result.Errors.Add("Invalid contact's title");
                return result;
            }

            if (contact.Group == null)
            {
                result.Errors.Add("Contact has to be in a group");
                return result;
            }

            if (contact.Items == null || contact.Items.Count == 0 || contact.Items.Any(e => !ValidateContactEntry(e)))
            {
                result.Errors.Add("Can not add contact with no entries or empty entries");
                return result;
            }

            return result;
        }

        public Result AddContact(Contact contact)
        {
            var result = new Result();

            var validationResult = ValidateContact(contact);
            if (!validationResult.IsSuccess)
            {
                return validationResult;
            }

            dataAccess.AddContact(contact);

            return result;
        }

        public Result EditContact(Contact contact)
        {
            var result = new Result();

            var validationResult = ValidateContact(contact);
            if (!validationResult.IsSuccess)
            {
                return validationResult;
            }

            dataAccess.EditContact(contact);

            return result;
        }

        public void DeleteContact(Contact contact)
        {
            dataAccess.DeleteContact(contact.Id);
        }
    }
}
