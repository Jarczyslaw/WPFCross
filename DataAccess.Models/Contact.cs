using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public bool Favourite { get; set; }
        public Group Group { get; set; }
        public List<ContactItem> Items { get; set; }
    }
}
