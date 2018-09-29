using System;
using System.Collections.Generic;
using System.Text;

namespace Dialogs
{
    public interface IDialogsService
    {
        void ShowInfo(string message, bool showAsModal = true);
        void ShowWarning(string message, bool showAsModal = true);
        void ShowError(string error, bool showAsModal = true);
        void ShowException(Exception exception, string message = null, bool showAsModal = true);
        bool ShowYesNoQuestion(string question, bool showAsModal = true);
        void ShowProgressDialog(string caption, string text, string instruction);
        string OpenFile(string title, string initialDirectory, List<DialogFilterPair> filters);
        List<string> OpenFiles(string title, string initialDirectory, List<DialogFilterPair> filters);
        string OpenFolder(string title, string initialDirectory);
        string SaveFile(string title, string initialDirectory, string defaultFileName, DialogFilterPair filter);
    }
}
