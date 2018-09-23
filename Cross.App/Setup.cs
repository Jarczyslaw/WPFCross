using Core.ViewModels;
using MvvmCross.Platforms.Wpf.Core;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Cross.App
{
    public class Setup : MvxWpfSetup
    {
        protected override IMvxApplication CreateApp()
        {
            return new Application();
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
