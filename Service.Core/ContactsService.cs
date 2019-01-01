using Commons;
using DataAccess.Core;
using DataAccess.Models;
using System;
using System.Collections.Generic;

namespace Service.Core
{
    public class ContactsService : IContactsService
    {
        private readonly IDbDataAccess dataAccess;

        public ContactsService(IDbDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        public ValueResult<IEnumerable<Contact>> GetContacts(Group group, bool favourites)
        {
            return new ValueResult<IEnumerable<Contact>>(dataAccess.GetContacts(group, favourites));
        }

        public bool ValidateContactName(string name)
        {
            return !string.IsNullOrEmpty(name) && name.Length >= 3;
        }

        public bool ValidateContactTitle(string title)
        {
            return !string.IsNullOrEmpty(title) && title.Length >= 3;
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
                result.Errors.Add("Contact has to be in group");
                return result;
            }

            return result;
        }

        public Result AddContact(Contact contact)
        {
            var result = new Result();

            var validationResult = ValidateContact(contact);
            if (!validationResult.IsSuccess)
                return validationResult;

            dataAccess.AddContact(contact);

            return result;
        }

        public void DeleteContact(Contact contact)
        {
            dataAccess.DeleteContact(contact.Id);
        }
    }
}
