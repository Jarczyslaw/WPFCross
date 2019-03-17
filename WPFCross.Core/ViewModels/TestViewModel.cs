using MvvmCross.Commands;
using MvvmCross.Navigation;
using Service.Dialogs;
using Service.Logger;
using System;
using WPFCross.Core.ViewModels.Base;

namespace WPFCross.Core.ViewModels
{
    public class TestViewModel : ViewModelBase
    {
        public TestViewModel(IMvxNavigationService navigationService, ILoggerService loggingService, IDialogsService dialogsService)
            : base(navigationService, loggingService, dialogsService)
        {
            Test1Command = new MvxCommand(Test1);
            Test2Command = new MvxCommand(Test2);
            Test3Command = new MvxCommand(Test3);
        }

        public IMvxCommand Test1Command { get; }
        public IMvxCommand Test2Command { get; }
        public IMvxCommand Test3Command { get; }

        private void Test1()
        {
            throw new NotImplementedException();
        }

        private void Test2()
        {
            throw new NotImplementedException();
        }

        private void Test3()
        {
            throw new NotImplementedException();
        }
    }
}
