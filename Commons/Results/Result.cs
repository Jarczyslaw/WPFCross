﻿using System.Linq;

namespace Commons
{
    public class Result
    {
        public bool IsSuccess => !Errors.Any();

        public ResultErrors Errors { get; }

        public ResultInfos Infos { get; }

        public Result()
            : this (new ResultErrors(), new ResultInfos())
        {
        }

        public Result(Result result)
            : this (new ResultErrors(result.Errors), new ResultInfos(result.Infos))
        {
        }

        public Result(ResultErrors errors, ResultInfos infos)
        {
            Errors = errors;
            Infos = infos;
        }

        public virtual void Clear()
        {
            Errors.Clear();
            Infos.Clear();
        }
    }
}