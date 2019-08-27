using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Controls
{
    [TemplatePart(Name = "ContentButtonElement", Type = typeof(Button))]
    public class ButtonComboBox : ComboBox
    {
        static ButtonComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ButtonComboBox), new FrameworkPropertyMetadata(typeof(ButtonComboBox)));
        }

        private Button _contentButtonElement;
        private Button ContentButtonElement
        {
            get => _contentButtonElement;
            set
            {
                RemoveOldHandler(_contentButtonElement, Content_Click);
                if (value != null)
                {
                    _contentButtonElement = value;
                    _contentButtonElement.Click += Content_Click;
                }
            }
        }
        
        public string DefaultContent
        {
            get { return (string)GetValue(DefaultContentProperty); }
            set { SetValue(DefaultContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DefaultContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DefaultContentProperty =
            DependencyProperty.Register("DefaultContent", typeof(string), typeof(ButtonComboBox), new PropertyMetadata(default(string)));
        
        public static readonly RoutedEvent ContentClickEvent = EventManager.RegisterRoutedEvent(
            "ContentClick", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(ButtonComboBox));
        
        public event RoutedEventHandler ContentClick
        {
            add { AddHandler(ContentClickEvent, value); }
            remove { RemoveHandler(ContentClickEvent, value); }
        }

        private void Content_Click(object sender, RoutedEventArgs e)
        {
            if (ContentButtonElement.Content as string != DefaultContent)
                this.RaiseEvent(new RoutedEventArgs(ContentClickEvent));
            else
                IsDropDownOpen = !IsDropDownOpen;
        }

        public override void OnApplyTemplate()
        {
            ContentButtonElement = GetTemplateChild("ContentButton") as Button;
            base.OnApplyTemplate();
        }

        private void RemoveOldHandler(Button button, RoutedEventHandler handler)
        {
            if (button != null)
                button.Click -= handler;
        }
    }
}
