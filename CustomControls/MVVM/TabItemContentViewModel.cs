using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Linq;

namespace MVVM
{
    public abstract class TabItemContentViewModel : NotifyAndWindowService, IViewType, IParent, ICurrent
    {
        public string ParentGUID { get; }
        public string CurrentGUID { get; }
        public Type ViewType { get; }
        public string Header { get; }
        public bool IsRemoveView { get; }
        public string ToolTip { get; set; }
        /// <summary>
        /// 클로즈 버튼이 없고 고정
        /// </summary>
        public bool IsDefault { get; }

        public TabItemContentViewModel(Type type, string header, string parentGUID, bool isRemoveView = true, bool isDefault = false)
        {
            ViewType = type;
            Header = header;
            ParentGUID = parentGUID;
            CurrentGUID = Guid.NewGuid().ToString();
            IsRemoveView = isRemoveView;
            IsDefault = isDefault;
        }
    }
}
