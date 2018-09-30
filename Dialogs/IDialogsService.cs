using System;
using System.Collections.Generic;
using System.Text;

namespace Dialogs
{
    public interface IDialogsService
    {
        void ShowInfo(string message, IntPtr? owner = null);
        void ShowWarning(string message, IntPtr? owner = null);
        void ShowError(string error, IntPtr? owner = null);
        void ShowException(Exception exception, string message = null, IntPtr? owner = null);
        bool ShowYesNoQuestion(string question, IntPtr? owner = null);
        void ShowProgressDialog(string caption, string text, string instruction);
        string OpenFile(string title, string initialDirectory, List<DialogFilterPair> filters);
        List<string> OpenFiles(string title, string initialDirectory, List<DialogFilterPair> filters);
        string OpenFolder(string title, string initialDirectory);
        string SaveFile(string title, string initialDirectory, string defaultFileName, DialogFilterPair filter);
    }
}
