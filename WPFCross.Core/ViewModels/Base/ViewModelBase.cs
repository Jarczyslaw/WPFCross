using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Service.Dialogs;
using Service.Logger;
using System;
using System.Collections.Generic;
using System.Text;

namespace WPFCross.Core.ViewModels.Base
{
    public abstract class ViewModelBase : MvxViewModel
    {
        protected readonly IMvxNavigationService navigationService;
        protected readonly ILoggerService loggingService;
        protected readonly IDialogsService dialogsService;

        protected ViewModelBase(IMvxNavigationService navigationService, ILoggerService loggingService, IDialogsService dialogsService)
        {
            this.navigationService = navigationService;
            this.loggingService = loggingService;
            this.dialogsService = dialogsService;
        }
    }
}
