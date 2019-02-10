using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;

namespace WPFCross.Startup.Views
{
    [MvxWindowPresentation(Identifier = nameof(ContactView), Modal = true)]
    public partial class ContactView : MvxWindow
    {
        public ContactView()
        {
            InitializeComponent();
        }
    }
}
