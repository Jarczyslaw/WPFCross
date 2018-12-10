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
    public class GlobalExceptionObserver
    {
        private readonly IGlobalExceptionHandler globalExceptionHandler;

        public GlobalExceptionObserver(IGlobalExceptionHandler globalExceptionHandler)
        {
            this.globalExceptionHandler = globalExceptionHandler;

            AppDomain.CurrentDomain.UnhandledException += 
                (s, e) => globalExceptionHandler.HandleException("AppDomain.CurrentDomain.UnhandledException", (Exception)e.ExceptionObject);
            Application.Current.DispatcherUnhandledException += 
                (s, e) =>
                {
                    globalExceptionHandler.HandleException("Application.Current.DispatcherUnhandledException", e.Exception);
                    e.Handled = true;
                };
            TaskScheduler.UnobservedTaskException += 
                (s, e) => globalExceptionHandler.HandleException("TaskScheduler.UnobservedTaskException", e.Exception);
        }
    }
}
