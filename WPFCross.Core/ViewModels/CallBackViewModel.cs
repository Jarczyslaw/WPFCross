using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace WPFCross.Core.ViewModels
{
    public abstract class CallBackViewModel<T> : MvxViewModel<Action<T>>
    {
        protected Action<T> Callback { get; private set; }

        public override void Prepare(Action<T> parameter)
        {
            Callback = parameter;
        }
    }
}
