using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace MVVM
{
    public abstract class DialogBaseViewModel : NotifyAndWindowService, IViewType
    {
        public string Title { get; }

        public Type ViewType { get; }

        public abstract ICommand ConfirmCommand { get; }
        
        public virtual ICommand CancelCommand { get; }

        public DialogBaseViewModel(Type viewType, string title = null)
        {
            ViewType = viewType;
            Title = title;
        }
    }
}
