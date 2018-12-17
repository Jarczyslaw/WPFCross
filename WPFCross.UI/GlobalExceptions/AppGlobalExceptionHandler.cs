using Service.Dialogs;
using Service.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCross.UI.GlobalExceptions
{
    public class AppGlobalExceptionHandler : GlobalExceptionHandler
    {
        private ILoggerService loggingService;
        private IDialogsService dialogService;

        public AppGlobalExceptionHandler(ILoggerService loggingService, IDialogsService dialogService)
        {
            this.loggingService = loggingService;
            this.dialogService = dialogService;
        }

        public override bool HandleException(string source, Exception exception)
        {
            var message = "Fatal exception source: " + source + ". Exception message: ";
            loggingService.Fatal(exception, message);
            dialogService.ShowCriticalException(message, exception);
            return true;
        }
    }
}
