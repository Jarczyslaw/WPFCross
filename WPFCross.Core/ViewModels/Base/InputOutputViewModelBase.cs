using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Service.Dialogs;
using Service.Logger;
using System;
using System.Collections.Generic;
using System.Text;

namespace WPFCross.Core.ViewModels.Base
{
    public abstract class InputOutputViewModelBase<TInput, TOutput> : MvxViewModel<TInput, TOutput>
    {
        protected readonly IMvxNavigationService navigationService;
        protected readonly ILoggerService loggingService;
        protected readonly IDialogsService dialogsService;

        protected InputOutputViewModelBase(IMvxNavigationService navigationService, ILoggerService loggingService, IDialogsService dialogsService)
        {
            this.navigationService = navigationService;
            this.loggingService = loggingService;
            this.dialogsService = dialogsService;
        }

        protected async void CloseWithResult(TOutput result)
        {
            await navigationService.Close(this, result)
                .ConfigureAwait(false);
        }
    }
}
