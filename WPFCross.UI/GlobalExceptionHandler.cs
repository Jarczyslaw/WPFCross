using Dialogs;
using Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFCross.UI
{
    public class GlobalExceptionHandler
    {
        private ILoggingService loggingService;
        private IDialogsService dialogService;

        public GlobalExceptionHandler(ILoggingService loggingService, IDialogsService dialogService)
        {
            this.loggingService = loggingService;
            this.dialogService = dialogService;

            AppDomain.CurrentDomain.UnhandledException += 
                (s, e) => HandleException("AppDomain.CurrentDomain.UnhandledException", (Exception)e.ExceptionObject);
            Application.Current.DispatcherUnhandledException += 
                (s, e) =>
                {
                    HandleException("Application.Current.DispatcherUnhandledException", e.Exception);
                    e.Handled = true;
                };
            TaskScheduler.UnobservedTaskException += 
                (s, e) => HandleException("TaskScheduler.UnobservedTaskException", e.Exception);
        }

        private void HandleException(string source, Exception exception)
        {
            var message = "Fatal exception source: " + source;
            loggingService.Fatal(exception, message);
            dialogService.ShowException(exception, message);
        }
    }
}
