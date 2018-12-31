using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace WPFCross.Core.ViewModels
{
    public abstract class CallBackViewModel<T> : MvxViewModel<Action<T>>
    {
        protected T CallbackValue { get; set; }
        protected bool CallOnDestroy { get; set; } = true;
        private Action<T> callback;

        public override void Prepare(Action<T> parameter)
        {
            callback = parameter;
        }

        public override void ViewDestroy(bool viewFinishing = true)
        {
            if (CallOnDestroy)
                callback?.Invoke(CallbackValue);
            base.ViewDestroy(viewFinishing);
        }
    }
}
