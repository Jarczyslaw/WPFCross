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

        public void DeleteContact(Contact contact)
        {
            dataAccess.DeleteContact(contact.Id);
        }
    }
}
