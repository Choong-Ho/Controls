using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Controls
{
    [TemplatePart(Name = "RemoveButtonElement", Type = typeof(Button))]
    public class RemoveItem : ListBoxItem
    {
        static RemoveItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RemoveItem), new FrameworkPropertyMetadata(typeof(RemoveItem)));
        }

        public static readonly RoutedEvent RemoveClickEvent = EventManager.RegisterRoutedEvent(
                "RemoveClick", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(RemoveItem));

        public event RoutedEventHandler RemoveClick
        {
            add { AddHandler(RemoveClickEvent, value); }
            remove { RemoveHandler(RemoveClickEvent, value); }
        }

        private Button _removeButtonElement;
        private Button RemoveButtonElement
        {
            get => _removeButtonElement;
            set
            {
                RemoveOldHandler(_removeButtonElement, Remove);
                if (value != null)
                {
                    _removeButtonElement = value;
                    _removeButtonElement.Click += Remove;
                }
            }
        }

        public override void OnApplyTemplate()
        {
            RemoveButtonElement = GetTemplateChild("RemoveButton") as Button;

            base.OnApplyTemplate();
        }

        private void Remove(object sender, RoutedEventArgs e)
            => RaiseEvent(new RoutedEventArgs(RemoveClickEvent));
        
        private void RemoveOldHandler(Button button, RoutedEventHandler handler)
        {
            if (button != null)
                button.Click -= handler;
        }
    }
}
