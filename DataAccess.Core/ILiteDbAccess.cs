using DataAccess.Models;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Core
{
    public interface ILiteDbAccess
    {
        void DropContacts(string connectionString);
        void DropGroups(string connectionString);
        void InvokeContactsAction(string connectionString, Action<LiteDatabase, LiteCollection<Contact>> action);
        void InvokeGroupsAction(string connectionString, Action<LiteDatabase, LiteCollection<Group>> action);
        void InvokeDbAction(string connectionString, Action<LiteDatabase> action);
    }
}
