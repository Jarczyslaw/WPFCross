using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;

namespace WPFCross.UI.Views
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
