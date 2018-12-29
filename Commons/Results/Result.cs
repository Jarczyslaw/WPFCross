using System;

namespace Commons
{
    public class Result
    {
        public bool IsSuccess => !Errors.Any;

        public ResultErrors Errors { get; } = new ResultErrors();

        public ResultInfos Infos { get; } = new ResultInfos();

        public virtual void Clear()
        {
            Errors.Clear();
            Infos.Clear();
        }
    }
}
