using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;

namespace WPFCross.Startup.Views
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
