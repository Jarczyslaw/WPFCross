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
        public void ShowInfo(string message, IntPtr? owner = null)
        {
            var builder = new TaskDialogBuilder()
                .Initialize("Information", message, TaskDialogStandardIcon.Information)
                .SetOwner(GetOwnerHandle(owner));

            using (var dialog = builder.Build())
            {
                dialog.Show();
            }
        }

        public void ShowWarning(string message, IntPtr? owner = null)
        {
            var builder = new TaskDialogBuilder()
                .Initialize("Warning", message, TaskDialogStandardIcon.Warning)
                .SetOwner(GetOwnerHandle(owner));

            using (var dialog = builder.Build())
            {
                dialog.Show();
            }
        }

        public void ShowError(string error, IntPtr? owner = null)
        {
            var builder = new TaskDialogBuilder()
                .Initialize("Error", error, TaskDialogStandardIcon.Error)
                .SetOwner(GetOwnerHandle(owner));

            using (var dialog = builder.Build())
            {
                dialog.Show();
            }
        }

        public void ShowException(Exception exception, string message = null, IntPtr? owner = null)
        {
            var text = string.Empty;
            if (!string.IsNullOrEmpty(message))
                text += message + Environment.NewLine;
            text += exception.Message;

            var builder = new TaskDialogBuilder()
                .Initialize("Exception", text, TaskDialogStandardIcon.Error, "An unexpected application exception occurred")
                .AddDetails("Show details", "Hide details", exception.StackTrace)
                .SetOwner(GetOwnerHandle(owner));

            using (var dialog = builder.Build())
            {
                dialog.Show();
            }
        }

        public bool ShowYesNoQuestion(string question, IntPtr? owner = null)
        {
            var builder = new TaskDialogBuilder()
                .Initialize("Question", question, TaskDialogStandardIcon.Information, "Question")
                .SetButtons(TaskDialogStandardButtons.Yes, TaskDialogStandardButtons.No)
                .SetOwner(GetOwnerHandle(owner));

            var result = false;
            using (var dialog = builder.Build())
            {
                result = dialog.Show() == TaskDialogResult.Yes;
            }
            return result;
        }

        public void ShowProgressDialog(string caption, string text, string instruction)
        {
            var builder = new TaskDialogBuilder()
                .Initialize(caption, text, TaskDialogStandardIcon.Information, instruction)
                .SetButtons(TaskDialogStandardButtons.Cancel)
                .AddProgressbar(0, 100, TaskDialogProgressBarState.Marquee);

            using (var dialog = builder.Build())
                dialog.Show();
        }

        public string OpenFile(string title, string initialDirectory, List<DialogFilterPair> filters)
        {
            var builder = new CommonOpenDialogBuilder()
                .Initialize(title, initialDirectory)
                .SetAsFileDialog(false)
                .AddFilters(filters);

            string result = null;
            using (var dialog = builder.Build())
            {
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                    result = dialog.FileName;
            }  
            return result;
        }

        public List<string> OpenFiles(string title, string initialDirectory, List<DialogFilterPair> filters)
        {
            var builder = new CommonOpenDialogBuilder()
                .Initialize(title, initialDirectory)
                .SetAsFileDialog(true)
                .AddFilters(filters);

            List<string> result = null;
            using (var dialog = builder.Build())
            {
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                    result = dialog.FileNames.ToList();
            }
            return result;
        }

        public string OpenFolder(string title, string initialDirectory)
        {
            var builder = new CommonOpenDialogBuilder()
                .Initialize(title, initialDirectory)
                .SetAsFolderDialog();

            string result = null;
            using (var dialog = builder.Build())
            {
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                    result = dialog.FileName;
            }
            return result;
        }

        public string SaveFile(string title, string initialDirectory, string defaultFileName, DialogFilterPair filter)
        {
            var builder = new CommonSaveDialogBuilder()
                .Initialize(title, initialDirectory)
                .SetDefaults(defaultFileName, filter.ExtensionsList)
                .AddFilter(filter);

            string result = null;
            using (var dialog = builder.Build())
            {
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                    result = dialog.FileName;
            }
            return result;
        }

        private IntPtr GetOwnerHandle(IntPtr? owner)
        {
            if (owner == null)
                return NativeMethods.GetActiveWindow();
            else if (owner.Value == IntPtr.Zero)
                return IntPtr.Zero;
            else
                return owner.Value;
        }
    }
}
