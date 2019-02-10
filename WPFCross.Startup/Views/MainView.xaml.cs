using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using System.Windows;
using System.Windows.Input;
using WPFCross.Core.ViewModels;

namespace WPFCross.Startup
{
    [MvxContentPresentation(WindowIdentifier = nameof(MainWindow), StackNavigation = false)]
    public partial class MainView : MvxWpfView
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is MainViewModel mainViewModel
                && ((FrameworkElement)e.OriginalSource).DataContext is ContactItemViewModel contactViewModel)
            {
                mainViewModel.EditCommand.Execute();
            }
        }
    }
}
