using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;

namespace WPFCross.Startup.Views
{
    [MvxContentPresentation(WindowIdentifier = nameof(TestWindow), StackNavigation = false)]
    public partial class TestView : MvxWpfView
    {
        public TestView()
        {
            InitializeComponent();
        }
    }
}
