using Commons;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Core
{
    public interface IContactsService
    {
        ValueResult<IEnumerable<Contact>> GetContacts(Group group, bool favourites);
        void DeleteContact(Contact contact);
    }
}
