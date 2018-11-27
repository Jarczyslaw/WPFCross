using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
