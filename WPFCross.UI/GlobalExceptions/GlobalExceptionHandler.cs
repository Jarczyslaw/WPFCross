using Service.Dialogs;
using Service.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFCross.UI.GlobalExceptions
{
    public abstract class GlobalExceptionHandler
    {
        public abstract bool HandleException(string source, Exception exception);

        public GlobalExceptionHandler()
        {
            AppDomain.CurrentDomain.UnhandledException += 
                (s, e) => HandleException("AppDomain.CurrentDomain.UnhandledException", (Exception)e.ExceptionObject);
            Application.Current.DispatcherUnhandledException += 
                (s, e) =>
                {
                    e.Handled = HandleException("Application.Current.DispatcherUnhandledException", e.Exception);
                };
            TaskScheduler.UnobservedTaskException += 
                (s, e) => HandleException("TaskScheduler.UnobservedTaskException", e.Exception);
        }
    }
}
