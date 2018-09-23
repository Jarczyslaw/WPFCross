using System;
using System.Collections.Generic;
using System.Text;

namespace Logging
{
    public interface ILoggingService
    {
        void Debug(string message, params object[] args);
        void Error(Exception exception);
        void Error(Exception exception, string message);
        void Error(string message, params object[] args);
        void Fatal(Exception exception);
        void Fatal(Exception exception, string message);
        void Fatal(string message, params object[] args);
        void Info(string message, params object[] args);
        void Trace(string fomessagermat, params object[] args);
        void Warn(string message, params object[] args);
    }
}
