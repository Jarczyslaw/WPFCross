using LiteDB;
using System;

namespace DataAccess.Models
{
    public class Group
    {
        public string Name { get; set; }
        public bool Editable { get; set; }
    }
}
