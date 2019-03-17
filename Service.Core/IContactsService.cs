using Commons;
using DataAccess.Models;
using System.Collections.Generic;

namespace Service.Core
{
    public interface IContactsService
    {
        bool ValidateContactName(string name);
        bool ValidateContactTitle(string title);
        Result ValidateContact(Contact contact);
        ValueResult<IEnumerable<Contact>> GetContacts(Group group, bool favourites);
        Result AddContact(Contact contact);
        Result EditContact(Contact contact);
        void DeleteContact(Contact contact);
    }
}
