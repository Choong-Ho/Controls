using System;
using System.Collections;
using System.Windows;
using System.Linq;

namespace MVVM
{
    public abstract class BaseItemViewModel : NotifyAndWindowService, IParent, ICurrent
    {
        public string ParentGUID { get; }
        public string CurrentGUID { get; }

        public BaseItemViewModel(string parentGUID)
        {
            ParentGUID = parentGUID;
            CurrentGUID = Guid.NewGuid().ToString();
        }
    }
}
