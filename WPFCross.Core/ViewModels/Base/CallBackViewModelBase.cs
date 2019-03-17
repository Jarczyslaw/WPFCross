using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Service.Dialogs;
using Service.Logger;
using System;

namespace WPFCross.Core.ViewModels.Base
{
    public abstract class CallBackViewModelBase<T> : MvxViewModel<Action<T>>
    {
        protected Action<T> Callback { get; private set; }

        protected readonly IMvxNavigationService navigationService;
        protected readonly ILoggerService loggingService;
        protected readonly IDialogsService dialogsService;

        protected CallBackViewModelBase(IMvxNavigationService navigationService, ILoggerService loggingService, IDialogsService dialogsService)
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
