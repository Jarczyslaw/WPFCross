using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Core.Collections
{
    public class Contacts : Repository<Contact>, IContacts
    {
        public Contacts(IDatabaseSource databaseSource) : base(databaseSource)
        {
            CollectionName = "contacts";
        }
    }
}
