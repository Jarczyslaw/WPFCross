using Dialogs;
using Logging;
using MvvmCross.Commands;
using System;
using System.Collections.Generic;

namespace Core.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public string Test { get; set; } = "Test Test Test";

        private IMvxCommand testCommand;
        public IMvxCommand TestCommand
        {
            get
            {
                if (testCommand == null)
                    testCommand = new MvxCommand(() =>
                    {
                        //loggingService.Debug("Test command Test command Test command Test command Test command Test command");
                        throw new Exception("Exception message");
                    });
                return testCommand;
            }
        }

        private IMvxCommand dialogsCommand;
        public IMvxCommand DialogsCommand
        {
            get
            {
                if (dialogsCommand == null)
                    dialogsCommand = new MvxCommand(() =>
                    {
                        dialogsService.ShowInfo("asd");
                    });
                return dialogsCommand;
            }
        }

        private ILoggingService loggingService;
        private IDialogsService dialogsService;

        public MainViewModel(ILoggingService loggingService, IDialogsService dialogsService)
        {
            this.loggingService = loggingService;
            this.dialogsService = dialogsService;
        }
    }
}
