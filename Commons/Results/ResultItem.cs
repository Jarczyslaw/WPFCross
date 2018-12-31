using System;
using System.Collections.Generic;
using System.Text;

namespace Commons
{
    public abstract class ResultItem
    {
        public int Code { get; set; }
        public string Content { get; set; }

        protected ResultItem()
        {

        }

        protected ResultItem(ResultItem item)
        {
            Code = item.Code;
            Content = item.Content;
        }
    }
}
