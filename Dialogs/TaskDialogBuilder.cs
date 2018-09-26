using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialogs
{
    public class TaskDialogBuilder
    {
        private TaskDialog taskDialog;

        public TaskDialogBuilder Initialize(string caption, string text, TaskDialogStandardIcon icon, string instructionText = null)
        {
            taskDialog = new TaskDialog
            {
                Caption = caption,
                Text = text,
                Icon = icon,
                InstructionText = instructionText,
                Cancelable = false
            };
            return this;
        }

        public TaskDialogBuilder SetOwner(IntPtr handle)
        {
            taskDialog.OwnerWindowHandle = handle;
            return this;
        }

        public TaskDialogBuilder SetDetails(string collapsedLabel, string expandedLabel, string text, bool expanded = false)
        {
            taskDialog.DetailsExpanded = expanded;
            taskDialog.DetailsCollapsedLabel = collapsedLabel;
            taskDialog.DetailsExpandedLabel = expandedLabel;
            taskDialog.DetailsExpandedText = text;
            return this;
        }

        public TaskDialogBuilder SetFooter(string text, TaskDialogStandardIcon icon)
        {
            taskDialog.FooterIcon = icon;
            taskDialog.FooterText = text;
            return this;
        }

        public TaskDialogBuilder SetButtons(params TaskDialogStandardButtons[] buttons)
        {
            var buttonsSum = TaskDialogStandardButtons.None;
            foreach (var button in buttons)
                buttonsSum |= button;
            taskDialog.StandardButtons = buttonsSum;
            return this;
        }

        public TaskDialogBuilder SetDefaultButton(TaskDialogDefaultButton defaultButton)
        {
            taskDialog.DefaultButton = defaultButton;
            return this;
        }

        public TaskDialogBuilder AddCustomButton(string name, string text, EventHandler handler, bool setAsDefault = false)
        {
            var customButton = new TaskDialogButton(name, text);
            customButton.Click += handler;
            customButton.Default = setAsDefault;
            taskDialog.Controls.Add(customButton);
            return this;
        }

        public TaskDialogBuilder AddCommandLink(string name, string text, string instruction, EventHandler handler, bool setAsDefault = false)
        {
            var commandLink = new TaskDialogCommandLink(name, text, instruction)
            {
                Default = setAsDefault
            };
            commandLink.Click += handler;
            taskDialog.Controls.Add(commandLink);
            return this;
        }

        public TaskDialogBuilder SetProgressBar(int minValue, int maxValue, TaskDialogProgressBarState state)
        {
            var progressBar = new TaskDialogProgressBar(minValue, maxValue, minValue)
            {
                State = state
            };
            taskDialog.ProgressBar = progressBar;
            return this;
        }

        public TaskDialog Build()
        {
            return taskDialog;
        }
    }
}
