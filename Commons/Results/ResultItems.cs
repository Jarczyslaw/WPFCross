using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commons
{
    public abstract class ResultItems<T>
        where T : ResultItem
    {
        public List<T> Items { get; protected set; } = new List<T>();

        public bool Any => Items.Count > 0;

        public T First => Items.FirstOrDefault();

        public T Last => Items.LastOrDefault();

        public void Clear()
        {
            Items.Clear();
        }
    }
}
