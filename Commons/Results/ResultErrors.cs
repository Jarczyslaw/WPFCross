using System;
using System.Linq;

namespace Commons
{
    public class ResultErrors : ResultItems<ResultError>
    {
        public ResultErrors()
        {
        }

        public ResultErrors(ResultErrors errors)
        {
            Items = errors.Items
                .Select(i => new ResultError(i))
                .ToList();
        }

        public void Add(Exception exception)
        {
            Add(new ResultError
            {
                Content = exception.Message,
                Exception = exception
            });
        }

        public void Add(ResultError error)
        {
            Items.Add(error);
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
