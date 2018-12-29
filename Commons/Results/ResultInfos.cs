using System;
using System.Collections.Generic;
using System.Text;

namespace Commons
{
    public class ResultInfos : ResultItems<ResultInfo>
    {
        public void Add(ResultInfo info)
        {
            Items.Add(info);
        }

        public void Add(string content)
        {
            Add(new ResultInfo
            {
                Content = content
            });
        }
    }
}
