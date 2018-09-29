using Microsoft.WindowsAPICodePack.Dialogs;
using System.Collections.Generic;

namespace Dialogs.Builders
{
    public class CommonOpenDialogBuilder : CommonDialogBuilderBase<CommonOpenFileDialog>
    {
        public CommonOpenDialogBuilder Initialize(string title, string initialDirectory)
        {
            dialog = new CommonOpenFileDialog
            {
                Title = title,
                InitialDirectory = initialDirectory
            };
            return this;
        }

        public CommonOpenDialogBuilder SetAsFileDialog(bool multiselect)
        {
            dialog.IsFolderPicker = false;
            dialog.EnsureFileExists = false;
            dialog.Multiselect = multiselect;
            return this;
        }

        public CommonOpenDialogBuilder SetAsFolderDialog()
        {
            dialog.IsFolderPicker = true;
            dialog.EnsurePathExists = true;
            dialog.Multiselect = false;
            return this;
        }
    }
}
