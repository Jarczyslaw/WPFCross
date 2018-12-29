using DataAccess.Core;
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


    }
}
