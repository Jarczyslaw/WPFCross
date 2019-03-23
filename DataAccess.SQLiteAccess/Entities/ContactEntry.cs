﻿using DataAccess.Models;

namespace DataAccess.SQLiteAccess.Entities
{
    internal class ContactEntry
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public string Value { get; set; }
        public int ContactId { get; set; }

        public ContactEntryType MappedType => (ContactEntryType)Type;

        public Models.ContactEntry ToModel()
        {
            return new Models.ContactEntry
            {
                Id = Id,
                Type = MappedType,
                Value = Value
            };
        }
    }
}
