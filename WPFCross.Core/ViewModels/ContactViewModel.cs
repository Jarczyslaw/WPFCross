using MvvmCross;
using System;
using System.Collections.Generic;
using System.Text;
using WPFCross.Extensions;

namespace WPFCross.Core.ViewModels
{
    public class ContactViewModel : ViewModelBase
    {
        public string Initials
        {
            get { return string.IsNullOrEmpty(Title) ? string.Empty : Title[0].ToString(); }
        }

        public string Title { get; set; }

        public GroupSelectionViewModel GroupSelection { get; private set; }

        public ContactViewModel()
        {
            GroupSelection = Mvx.IoCProvider.RegisterTypeAndResolve<GroupSelectionViewModel>();
        }
    }
}
