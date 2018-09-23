using System;
using System.Collections.Generic;
using System.Text;

namespace Dialogs
{
    public interface IDialogsService
    {
        void ShowInfo(string message);
        void ShowWarning(string message);
        void ShowError(string error);
        void ShowException(Exception exception);
        void ShowException(string message, Exception exception);
        bool? ShowYesNoQuestion(string question);
    }
}
