using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialogs.Builders
{
    public class CommonOpenDialogBuilder
    {
        private CommonOpenFileDialog dialog;

        public CommonOpenDialogBuilder Initialize(string title)
        {
            dialog = new CommonOpenFileDialog
            {
                Title = title
            };
            return this;
        }

        public CommonOpenDialogBuilder SetAsFileDialog(bool multiselect, string initialDirectory)
        {
            dialog.IsFolderPicker = false;
            dialog.EnsureFileExists = false;
            dialog.Multiselect = multiselect;
            dialog.InitialDirectory = initialDirectory;
            return this;
        }

        public CommonOpenDialogBuilder SetAsFolderDialog(string initialDirectory)
        {
            dialog.IsFolderPicker = true;
            dialog.EnsurePathExists = true;
            dialog.Multiselect = false;
            dialog.InitialDirectory = initialDirectory;
            return this;
        }

        public CommonOpenDialogBuilder AddFilters(List<FilterPair> filters)
        {
            if (filters != null)
            {
                foreach (var pair in filters)
                    AddFilter(pair.DisplayName, pair.ExtensionsList);
            }
            return this;
        }

        public CommonOpenDialogBuilder AddFilter(string displayName, string extensionList)
        {
            var filter = new CommonFileDialogFilter(displayName, extensionList);
            dialog.Filters.Add(filter);
            return this;
        }

        public CommonOpenFileDialog Build()
        {
            return dialog;
        }
    }
}
