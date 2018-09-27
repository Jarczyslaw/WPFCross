using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialogs
{
    public class FilterPair
    {
        public string DisplayName { get; set; }
        public string ExtensionsList { get; set; }

        public FilterPair(string displayName, string extensionsList)
        {
            DisplayName = displayName;
            ExtensionsList = extensionsList;
        }
    }
}
