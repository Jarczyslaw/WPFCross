using System;

namespace Commons
{
    public class Result
    {
        public bool IsSuccess => !Errors.Any;

        public ResultErrors Errors { get; }

        public ResultInfos Infos { get; }

        public Result()
        {
            Errors = new ResultErrors();
            Infos = new ResultInfos();
        }

        public Result(Result result)
        {
            Errors = new ResultErrors(result.Errors);
            Infos = new ResultInfos(result.Infos);
        }

        public virtual void Clear()
        {
            Errors.Clear();
            Infos.Clear();
        }
    }
}
