using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Dialogs
{
    public class DialogsService : IDialogsService
    { 
        public void ShowInfo(string message)
        {
            var builder = new TaskDialogBuilder().
                Initialize("Information", message, TaskDialogStandardIcon.Information);
            using (var dialog = builder.Build())
            {
                dialog.Show();
            }
        }

        public void ShowWarning(string message)
        {
            var builder = new TaskDialogBuilder().
                Initialize("Warning", message, TaskDialogStandardIcon.Warning);
            using (var dialog = builder.Build())
            {
                dialog.Show();
            }
        }

        public void ShowError(string error)
        {
            var builder = new TaskDialogBuilder().
                Initialize("Error", error, TaskDialogStandardIcon.Error);
            using (var dialog = builder.Build())
            {
                dialog.Show();
            }
        }

        public void ShowException(Exception exception, string message = null)
        {
            var text = string.Empty;
            if (!string.IsNullOrEmpty(message))
                text += message + Environment.NewLine;
            text += exception.Message;

            var builder = new TaskDialogBuilder().
                Initialize("Exception", text, TaskDialogStandardIcon.Error, "An unexpected application exception occurred").
                SetDetails("Show details", "Hide details", exception.StackTrace);
            using (var dialog = builder.Build())
            {
                dialog.Opened += (s, e) => dialog.DetailsExpandedText = exception.StackTrace;
                dialog.Show();
            }
        }

        public string OpenFile(string title, string filter)
        {
            var dialog = new CommonOpenFileDialog();
            dialog.AddToMostRecentlyUsedList = true;
            dialog.AllowNonFileSystemItems = false;
            dialog.EnsureFileExists = true;
            dialog.Multiselect = false;

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                return dialog.FileName;
            return null;
        }

        public IEnumerable<string> OpenFiles()
        {
            var dialog = new CommonOpenFileDialog();
            dialog.Multiselect = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                return dialog.FileNames;
            return null;
        }

        public void SaveFile()
        {

        }

        public string OpenFolder()
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                return dialog.FileName;
            return null;
        }
    }
}
