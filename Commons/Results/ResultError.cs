using System;

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
