using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialogs.Builders
{
    public class TaskDialogBuilder
    {
        private TaskDialog dialog;

        public TaskDialogBuilder Initialize(string caption, string text, TaskDialogStandardIcon icon, string instructionText = null)
        {
            dialog = new TaskDialog
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
            dialog.OwnerWindowHandle = handle;
            return this;
        }

        public TaskDialogBuilder AddDetails(string collapsedLabel, string expandedLabel, string text, bool expanded = false)
        {
            dialog.DetailsExpanded = expanded;
            dialog.DetailsCollapsedLabel = collapsedLabel;
            dialog.DetailsExpandedLabel = expandedLabel;
            dialog.DetailsExpandedText = text;
            return this;
        }

        public TaskDialogBuilder AddFooter(string text, TaskDialogStandardIcon icon)
        {
            dialog.FooterIcon = icon;
            dialog.FooterText = text;
            return this;
        }

        public TaskDialogBuilder AddFooterTextbox(string text, bool checkedState)
        {
            dialog.FooterCheckBoxText = text;
            dialog.FooterCheckBoxChecked = checkedState;
            return this;
        }

        public TaskDialogBuilder SetButtons(params TaskDialogStandardButtons[] buttons)
        {
            var buttonsSum = TaskDialogStandardButtons.None;
            foreach (var button in buttons)
                buttonsSum |= button;
            dialog.StandardButtons = buttonsSum;
            return this;
        }

        public TaskDialogBuilder SetDefaultButton(TaskDialogDefaultButton defaultButton)
        {
            dialog.DefaultButton = defaultButton;
            return this;
        }

        public TaskDialogBuilder AddCustomButton(string name, string text, EventHandler handler, bool setAsDefault = false)
        {
            var customButton = new TaskDialogButton(name, text);
            customButton.Click += handler;
            customButton.Default = setAsDefault;
            dialog.Controls.Add(customButton);
            return this;
        }

        public TaskDialogBuilder AddCommandLink(string name, string text, string instruction, EventHandler handler, bool setAsDefault = false)
        {
            var commandLink = new TaskDialogCommandLink(name, text, instruction)
            {
                Default = setAsDefault
            };
            commandLink.Click += handler;
            dialog.Controls.Add(commandLink);
            return this;
        }

        public TaskDialogBuilder AddProgressbar(int minValue, int maxValue, TaskDialogProgressBarState state)
        {
            var progressBar = new TaskDialogProgressBar(minValue, maxValue, minValue)
            {
                State = state
            };
            dialog.ProgressBar = progressBar;
            return this;
        }

        public TaskDialog Build()
        {
            return dialog;
        }
    }
}
