using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MVVM;

namespace Controls
{
    public class CContentControl : ContentControl
    {
        private readonly Dictionary<object, FrameworkElement> _controls
            = new Dictionary<object, FrameworkElement>();

        public object CurrentContent
        {
            get { return (object)GetValue(CurrentContentProperty); }
            set { SetValue(CurrentContentProperty, value); }
        }
        
        public static readonly DependencyProperty CurrentContentProperty =
            DependencyProperty.Register("CurrentContent", typeof(object), typeof(CContentControl), new PropertyMetadata(null, CurrentContentChanged));

        private static void CurrentContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if(d is CContentControl control)
                control.OnCurrentContentChanged(e.OldValue);
        }

        internal void OnCurrentContentChanged(object oldContent)
        {
            if (CurrentContent != null)
            {
                var view = _controls.ContainsKey(CurrentContent) ?
                    _controls[CurrentContent] :
                    _controls[CurrentContent] = CreateCurrentControl(CurrentContent);

                Content = view;
            }
            else if(oldContent != null)//CurrentContent 혹은 NewValue가 null인 경우.. 즉, 이전 콘텐츠가 있다가 null을 넣은 경우엔 기존 콘텐츠를 제거한다.??
                _controls.Remove(oldContent);
        }

        private FrameworkElement CreateCurrentControl(object selectedContent)
        {
            FrameworkElement content = null;
            if (selectedContent is IViewType vm)
            {
                content = Activator.CreateInstance(vm.ViewType) as FrameworkElement;
                content.DataContext = selectedContent;
            }

            return content;
        }
    }
}
