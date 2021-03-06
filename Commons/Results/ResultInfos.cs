﻿using System.Collections.Generic;
using System.Linq;

namespace Commons
{
    public class ResultInfos : List<ResultInfo>
    {
        public ResultInfos()
        {
        }

        public ResultInfos(ResultInfos infos)
        {
            AddRange(infos);
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
