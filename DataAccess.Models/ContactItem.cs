using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class ContactItem
    {
        public int ContactItemId { get; set; }
        public ContactItemType ItemType { get; set; }
        public string Value { get; set; }
    }
}
