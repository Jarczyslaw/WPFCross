using System.Linq;

namespace Commons
{
    public class ResultInfos : ResultItems<ResultInfo>
    {
        public ResultInfos()
        {
        }

        public ResultInfos(ResultInfos infos)
        {
            Items = infos.Items
                .Select(i => new ResultInfo(i))
                .ToList();
        }

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
