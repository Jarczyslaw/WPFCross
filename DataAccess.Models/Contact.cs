﻿using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class Contact
    {
        public int ContactId { get; set; }
        public Group Group { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public List<ContactItem> Items { get; set; }
    }
}
