using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using Dialogs.Builders;

namespace Dialogs
{
    public class DialogsService : IDialogsService
    { 
        public void ShowInfo(string message, IntPtr owner = default(IntPtr))
        {
            var builder = new TaskDialogBuilder()
                .Initialize("Information", message, TaskDialogStandardIcon.Information)
                .SetOwner(owner);

            using (var dialog = builder.Build())
            {
                dialog.Show();
            }
        }

        public void ShowWarning(string message, IntPtr owner = default(IntPtr))
        {
            var builder = new TaskDialogBuilder()
                .Initialize("Warning", message, TaskDialogStandardIcon.Warning)
                .SetOwner(owner);

            using (var dialog = builder.Build())
            {
                dialog.Show();
            }
        }

        public void ShowError(string error, IntPtr owner = default(IntPtr))
        {
            var builder = new TaskDialogBuilder()
                .Initialize("Error", error, TaskDialogStandardIcon.Error)
                .SetOwner(owner);

            using (var dialog = builder.Build())
            {
                dialog.Show();
            }
        }

        public void ShowException(Exception exception, string message = null, IntPtr owner = default(IntPtr))
        {
            var text = string.Empty;
            if (!string.IsNullOrEmpty(message))
                text += message + Environment.NewLine;
            text += exception.Message;

            var builder = new TaskDialogBuilder()
                .Initialize("Exception", text, TaskDialogStandardIcon.Error, "An unexpected application exception occurred")
                .SetDetails("Show details", "Hide details", exception.StackTrace)
                .SetOwner(owner);

            using (var dialog = builder.Build())
            {
                dialog.Show();
            }
        }

        public bool ShowYesNoQuestion(string question, IntPtr owner = default(IntPtr))
        {
            var builder = new TaskDialogBuilder()
                .Initialize("Question", question, TaskDialogStandardIcon.Information, "Question")
                .SetButtons(TaskDialogStandardButtons.Yes, TaskDialogStandardButtons.No)
                .SetOwner(owner);

            var result = false;
            using (var dialog = builder.Build())
            {
                result = dialog.Show() == TaskDialogResult.Yes;
            }
            return result;
        }

        public string OpenFile(string title, string initialDirectory, List<FilterPair> filters)
        {
            var builder = new CommonOpenDialogBuilder().
                Initialize(title).
                SetAsFileDialog(false, initialDirectory).
                AddFilters(filters);

            string result = null;
            using (var dialog = builder.Build())
            {
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                    result = dialog.FileName;
            }  
            return result;
        }

        public List<string> OpenFiles(string title, string initialDirectory, List<FilterPair> filters)
        {
            var builder = new CommonOpenDialogBuilder().
                Initialize(title).
                SetAsFileDialog(true, initialDirectory).
                AddFilters(filters);

            List<string> result = null;
            using (var dialog = builder.Build())
            {
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                    result = dialog.FileNames.ToList();
            }
            return result;
        }

        public string SaveFile(string title, string initialDirectory, string defaultFileName, List<FilterPair> filters)
        {
            var dialog = new CommonSaveFileDialog()
            {
                Title = title,
                InitialDirectory = initialDirectory,
                DefaultFileName = defaultFileName
            };

            if (filters != null)
            {
                foreach (var pair in filters)
                    dialog.Filters.Add(new CommonFileDialogFilter(pair.DisplayName, pair.ExtensionsList));
            }
            
            string result = null;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                result = dialog.FileName;
            dialog.Dispose();
            return result;
        }

        public string OpenFolder(string title, string initialDirectory)
        {
            var builder = new CommonOpenDialogBuilder().
                Initialize(title).
                SetAsFolderDialog(initialDirectory);

            string result = null;
            using (var dialog = builder.Build())
            {
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                    result = dialog.FileName;
            }
            return result;
        }

        public void ShowProgressDialog(string caption, string text, string instruction)
        {
            var builder = new TaskDialogBuilder().
                Initialize(caption, text, TaskDialogStandardIcon.Information, instruction).
                SetButtons(TaskDialogStandardButtons.Cancel).
                SetProgressBar(0, 100, TaskDialogProgressBarState.Marquee);

            using (var dialog = builder.Build())
                dialog.Show();
        }
    }
}
