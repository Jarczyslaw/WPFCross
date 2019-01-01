using DataAccess.Core;
using DataAccess.Models;
using System;

namespace Service.Core
{
    public class ContactsService : IContactsService
    {
        private readonly IDbDataAccess dataAccess;

        public ContactsService(IDbDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        public void DeleteContact(Contact contact)
        {
            dataAccess.DeleteContact(contact.Id);
        }
    }
}
