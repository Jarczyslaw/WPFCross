using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commons
{
    public class ResultItems<T>
        where T : ResultItem
    {
        public List<T> Items { get; } = new List<T>();

        public bool Any => Items.Count > 0;

        public T First => Items.FirstOrDefault();

        public T Last => Items.LastOrDefault();

        public void Clear()
        {
            Items.Clear();
        }
    }
}
