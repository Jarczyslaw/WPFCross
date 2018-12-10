using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class ContactItem
    {
        public int Id { get; set; }
        public ContactItemType Type { get; set; }
        public string Value { get; set; }
    }
}
