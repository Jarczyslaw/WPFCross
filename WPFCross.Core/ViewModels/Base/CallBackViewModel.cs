using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Service.Dialogs;
using Service.Logger;
using System;
using System.Collections.Generic;
using System.Text;

namespace WPFCross.Core.ViewModels.Base
{
    public abstract class CallBackViewModel<T> : MvxViewModel<Action<T>>
    {
        protected Action<T> Callback { get; private set; }

        protected readonly IMvxNavigationService navigationService;
        protected readonly ILoggerService loggingService;
        protected readonly IDialogsService dialogsService;

        public CallBackViewModel(IMvxNavigationService navigationService, ILoggerService loggingService, IDialogsService dialogsService)
        {
            this.navigationService = navigationService;
            this.loggingService = loggingService;
            this.dialogsService = dialogsService;
        }

        public override void Prepare(Action<T> parameter)
        {
            Callback = parameter;
        }
    }
}
