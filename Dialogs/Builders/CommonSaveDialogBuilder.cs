using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialogs.Builders
{
    public class CommonSaveDialogBuilder : CommonDialogBuilderBase<CommonSaveFileDialog>
    {
        public CommonSaveDialogBuilder Initialize(string title, string initialDirectory)
        {
            var dialog = new CommonSaveFileDialog()
            {
                Title = title,
                InitialDirectory = initialDirectory,
                IsExpandedMode = false,
                EnsureValidNames = true,
                AlwaysAppendDefaultExtension = true,
            };
            return this;
        }

        public CommonSaveDialogBuilder SetDefaults(string defaultFileName, string defaultExtension)
        {
            dialog.DefaultFileName = defaultFileName;
            dialog.DefaultExtension = defaultExtension;
            return this;
        }

        public new CommonSaveDialogBuilder AddFilter(DialogFilterPair filter)
        {
            base.AddFilter(filter);
            return this;
        }

        public new CommonSaveDialogBuilder AddFilters(List<DialogFilterPair> filters)
        {
            base.AddFilters(filters);
            return this;
        }
    }
}
