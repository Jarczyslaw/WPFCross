using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logging
{
    public class LoggingService : ILoggingService
    {
        private Logger logger;

        private readonly string emptyMessage = "empty";

        public LoggingService()
        {
            logger = LogManager.GetCurrentClassLogger();
        }

        public void Debug(string message, params object[] args)
        {
            logger.Debug(message, args);
        }

        public void Error(Exception exception)
        {
            logger.Error(exception, emptyMessage);
        }

        public void Error(Exception exception, string message)
        {
            logger.Error(exception, message);
        }

        public void Error(string message, params object[] args)
        {
            logger.Error(message, args);
        }

        public void Fatal(Exception exception)
        {
            logger.Fatal(exception, emptyMessage);
        }

        public void Fatal(Exception exception, string message)
        {
            logger.Fatal(exception, message);
        }

        public void Fatal(string message, params object[] args)
        {
            logger.Fatal(message, args);
        }

        public void Info(string message, params object[] args)
        {
            logger.Info(message, args);
        }

        public void Trace(string message, params object[] args)
        {
            logger.Trace(message, args);
        }

        public void Warn(string message, params object[] args)
        {
            logger.Warn(message, args);
        }
    }
}
