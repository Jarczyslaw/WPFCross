using System;
using System.Collections.Generic;
using System.Linq;

namespace Commons
{
    public class ResultErrors : List<ResultError>
    {
        public ResultErrors()
        {
        }

        public ResultErrors(ResultErrors errors)
        {
            AddRange(errors.Select(i => new ResultError(i)));
        }

        public void Add(Exception exception)
        {
            Add(new ResultError
            {
                Content = exception.Message,
                Exception = exception
            });
        }

        public void Add(string content)
        {
            Add(new ResultError
            {
                Content = content
            });
        }
    }
}
