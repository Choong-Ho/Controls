using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Controls.Extensions
{
    static class MessageButtonsPanelExtension
    {
        public static MessageBoxButton? GetMessageBoxButtonInfo(MessageButtonsPanel panel)
            => (MessageBoxButton?)panel.GetValue(MessageBoxButtonInfoProperty);

        public static void SetMessageBoxButtonInfo(MessageButtonsPanel panel, MessageBoxButton? info)
            => panel.SetValue(MessageBoxButtonInfoProperty, info);

        // Using a DependencyProperty as the backing store for MessageBoxButtonInfo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MessageBoxButtonInfoProperty =
            DependencyProperty.RegisterAttached("MessageBoxButtonInfo", typeof(MessageBoxButton?), typeof(MessageButtonsPanelExtension), new FrameworkPropertyMetadata(OnMessageBoxButtonInfoChanged));

        private static void OnMessageBoxButtonInfoChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var panel = d as MessageButtonsPanel;
            var info = e.NewValue as MessageBoxButton?;

            switch (info)
            {
                case MessageBoxButton.OK:
                    panel.Children.Add(GetMessageWindowButton(panel, MessageBoxResult.OK, true));
                    break;
                case MessageBoxButton.OKCancel:
                    panel.Children.Add(GetMessageWindowButton(panel, MessageBoxResult.Cancel, isCancel: true));
                    panel.Children.Add(GetMessageWindowButton(panel, MessageBoxResult.OK, true));
                    break;
                case MessageBoxButton.YesNo:
                    panel.Children.Add(GetMessageWindowButton(panel, MessageBoxResult.No));
                    panel.Children.Add(GetMessageWindowButton(panel, MessageBoxResult.Yes, true));
                    break;
                case MessageBoxButton.YesNoCancel:
                    panel.Children.Add(GetMessageWindowButton(panel, MessageBoxResult.Cancel, isCancel: true));
                    panel.Children.Add(GetMessageWindowButton(panel, MessageBoxResult.No));
                    panel.Children.Add(GetMessageWindowButton(panel, MessageBoxResult.Yes, true));
                    break;
            }
        }

        private static MessageBoxResultButton GetMessageWindowButton(MessageButtonsPanel panel, MessageBoxResult result, bool isDefault = false, bool isCancel = false)
        {
            var btn = new MessageBoxResultButton(result) { IsDefault = isDefault, IsCancel = isCancel };
            return btn;
        }
    }
}
