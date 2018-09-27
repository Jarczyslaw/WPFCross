using System;
using System.Collections.Generic;
using System.Text;

namespace Dialogs
{
    public interface IDialogsService
    {
        void ShowInfo(string message, IntPtr owner = default(IntPtr));
        void ShowWarning(string message, IntPtr owner = default(IntPtr));
        void ShowError(string error, IntPtr owner = default(IntPtr));
        void ShowException(Exception exception, string message = null, IntPtr owner = default(IntPtr));
        bool ShowYesNoQuestion(string question, IntPtr owner = default(IntPtr));
        string OpenFile(string title, string initialDirectory, List<FilterPair> filters);
        List<string> OpenFiles(string title, string initialDirectory, List<FilterPair> filters);
        string SaveFile(string title, string initialDirectory, string defaultFileName, List<FilterPair> filters);
        string OpenFolder(string title, string initialDirectory);
        void ShowProgressDialog(string caption, string text, string instruction);
    }
}
