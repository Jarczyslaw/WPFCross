using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Core
{
    public interface IContactsService
    {
        void DeleteContact(Contact contact);
    }
}
