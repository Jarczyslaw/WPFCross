using System;
using System.Collections.Generic;
using System.Text;

namespace Commons
{
    public class ResultError : ResultItem
    {
        public Exception Exception { get; set; }
        public bool IsException => Exception != null;

        public ResultError()
        {
        }

        public ResultError(ResultError resultError)
            : base(resultError)
        {
            Exception = resultError.Exception;
        }
    }
}
