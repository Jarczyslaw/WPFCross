using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;

namespace WPFCross.UI.Views
{
    [MvxWindowPresentation(Identifier = nameof(GroupsView), Modal = true)]
    public partial class GroupsView : MvxWindow
    {
        public GroupsView()
        {
            InitializeComponent();
        }
    }
}
