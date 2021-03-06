﻿using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WPFCross.Extensions
{
    public static class IMvxNavigationServiceExtensions
    {
        public static async Task<bool> NavigateWithCallback<TViewModel, TCallbackType>(this IMvxNavigationService navigationService, Action<TCallbackType> callback)
            where TViewModel : MvxViewModel<Action<TCallbackType>>
        {
            return await navigationService.Navigate<TViewModel, Action<TCallbackType>>(callback)
                .ConfigureAwait(false);
        }

        public static async Task<TInputType> NavigateWithInput<TViewModel, TInputType>(this IMvxNavigationService navigationService, TInputType input)
            where TViewModel : MvxViewModel<TInputType, TInputType>
        {
            return await navigationService.Navigate<TViewModel, TInputType, TInputType>(input)
                .ConfigureAwait(false);
        }
    }
}
