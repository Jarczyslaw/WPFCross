using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class Group
    {
        public int Id { get; set; }
        public bool Default { get; set; }
        public string Name { get; set; }

        public Group()
        {
        }

        public Group(Group group)
        {
            Id = group.Id;
            Default = group.Default;
            Name = group.Name;
        }
    }
}
