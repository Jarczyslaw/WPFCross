using WPFCross.Core.ViewModels;
using MvvmCross.Platforms.Wpf.Core;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

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
            var assemblies = new List<Assembly>
            {
                typeof(ViewModelBase).Assembly
            };
            return assemblies;
        }
    }
}
