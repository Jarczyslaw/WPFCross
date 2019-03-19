using DataAccess.Core;
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
        private readonly IDbAccess dbAccess;

        public TestViewModel(IMvxNavigationService navigationService, ILoggerService loggingService, IDialogsService dialogsService, IDbAccess dbAccess)
            : base(navigationService, loggingService, dialogsService)
        {
            this.dbAccess = dbAccess;

            Test1Command = new MvxCommand(Test1);
            Test2Command = new MvxCommand(Test2);
            Test3Command = new MvxCommand(Test3);
        }

        public IMvxCommand Test1Command { get; }
        public IMvxCommand Test2Command { get; }
        public IMvxCommand Test3Command { get; }

        private void Test1()
        {
            try
            {
                dbAccess.Initialize();
                dialogsService.ShowInfo("Done: Initialize");
            }
            catch (Exception exc)
            {
                dialogsService.ShowException("Exception", exc);
            }
        }

        private void Test2()
        {
            try
            {
                dbAccess.Clear();
                dialogsService.ShowInfo("Done: Clear");
            }
            catch (Exception exc)
            {
                dialogsService.ShowException("Exception", exc);
            }
        }

        private void Test3()
        {
            try
            {
                var contactsCount = dbAccess.GetContactsCount();
                dialogsService.ShowInfo(contactsCount.ToString());
            }
            catch (Exception exc)
            {
                dialogsService.ShowException("Exception", exc);
            }
        }
    }
}
