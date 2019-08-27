using MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Controls
{
    [TemplatePart(Name = "RemoveButtonElement", Type = typeof(Button))]
    public class CTabItem : TabItem
    {
        static CTabItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CTabItem), new FrameworkPropertyMetadata(typeof(CTabItem)));
        }

        private Button _removeButtonElement;
        private Button RemoveButtonElement
        {
            get => _removeButtonElement;
            set
            {
                RemoveOldHandler(_removeButtonElement, CloseTabItem);
                if (value != null)
                {
                    _removeButtonElement = value;
                    _removeButtonElement.Click += CloseTabItem;
                }
            }
        }

        public bool IsButtonVisible
        {
            get { return (bool)GetValue(IsButtonVisibleProperty); }
            set { SetValue(IsButtonVisibleProperty, value); }
        }

        public static readonly DependencyProperty IsButtonVisibleProperty =
            DependencyProperty.Register("IsButtonVisible", typeof(bool), typeof(CTabItem), new PropertyMetadata(true));

        /// <summary>
        /// 버튼의 템플릿을 정의한다.
        /// </summary>
        public ControlTemplate ButtonTemplate
        {
            get { return (ControlTemplate)GetValue(ButtonTemplateProperty); }
            set
            {
                if (IsButtonVisible)
                    SetValue(ButtonTemplateProperty, value);
            }
        }

        public static readonly DependencyProperty ButtonTemplateProperty =
            DependencyProperty.Register("ButtonTemplate", typeof(ControlTemplate), typeof(CTabItem), new PropertyMetadata(default(ControlTemplate)));

        public object ButtonContent
        {
            get { return (object)GetValue(ButtonContentProperty); }
            set { SetValue(ButtonContentProperty, value); }
        }

        public static readonly DependencyProperty ButtonContentProperty =
            DependencyProperty.Register("ButtonContent", typeof(object), typeof(CTabItem), new PropertyMetadata(default(object)));

        private void CloseTabItem(object sender, RoutedEventArgs e)
            => RaiseEvent(new RoutedEventArgs(RemoveClickEvent));


        public static readonly RoutedEvent RemoveClickEvent = EventManager.RegisterRoutedEvent(
            "RemoveClick", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(CTabItem));

        /// <summary>
        /// IsButtonVisible 이 True인 경우에만 이벤트가 실행된다.
        /// </summary>
        public event RoutedEventHandler RemoveClick
        {
            add { AddHandler(RemoveClickEvent, value); }
            remove { RemoveHandler(RemoveClickEvent, value); }
        }

        public override void OnApplyTemplate()
        {   
            if (IsButtonVisible)
            {
                RemoveButtonElement = GetTemplateChild("RemoveButton") as Button;
                if (ButtonTemplate != null)
                    RemoveButtonElement.Template = ButtonTemplate;
            }
            
            base.OnApplyTemplate();
        }

        private void RemoveOldHandler(Button button, RoutedEventHandler handler)
        {
            if (button != null)
                button.Click -= handler;
        }
    }
}
