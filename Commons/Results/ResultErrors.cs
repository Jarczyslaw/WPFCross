using System;
using System.Collections.Generic;
using System.Text;

namespace Commons
{
    public class ResultErrors : ResultItems<ResultError>
    {
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
