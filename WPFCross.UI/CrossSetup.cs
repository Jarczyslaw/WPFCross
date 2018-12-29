using MvvmCross.Platforms.Wpf.Core;
using MvvmCross.ViewModels;
using System.Collections.Generic;
using System.Reflection;
using WPFCross.Core.ViewModels;

namespace WPFCross.UI
{
    public class CrossSetup : MvxWpfSetup
    {
        protected override IMvxApplication CreateApp()
        {
            return new CrossApplication();
        }

        public override IEnumerable<Assembly> GetViewModelAssemblies()
        {
            return new List<Assembly>
            {
                typeof(ViewModelBase).Assembly
            };
        }
    }
}
