using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Controls
{
    public class MessageButtonsPanel : DockPanel
    {
        public ICommand ConfirmButtonCommand
        {
            get { return (ICommand)GetValue(ConfirmButtonCommandProperty); }
            set { SetValue(ConfirmButtonCommandProperty, value); }
        }

        public static readonly DependencyProperty ConfirmButtonCommandProperty =
            DependencyProperty.Register("ConfirmButtonCommand", typeof(ICommand), typeof(MessageButtonsPanel), new PropertyMetadata(default(ICommand)));
        
        public ICommand CancelButtonCommand
        {
            get { return (ICommand)GetValue(CancelButtonCommandProperty); }
            set { SetValue(CancelButtonCommandProperty, value); }
        }

        public static readonly DependencyProperty CancelButtonCommandProperty =
            DependencyProperty.Register("CancelButtonCommand", typeof(ICommand), typeof(MessageButtonsPanel), new PropertyMetadata(default(ICommand)));

        public Window CurrentWindow
        {
            get { return (Window)GetValue(CurrentWindowProperty); }
            set { SetValue(CurrentWindowProperty, value); }
        }
        
        public static readonly DependencyProperty CurrentWindowProperty =
            DependencyProperty.Register("CurrentWindow", typeof(Window), typeof(MessageButtonsPanel), new PropertyMetadata(null));

        public event Action<MessageBoxResult> MessageButtonResultChanged;
        public void SetMessageButtonResult(MessageBoxResult result)
            => MessageButtonResultChanged?.Invoke(result);

        public MessageButtonsPanel()
        {
            
        }

        protected override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved)
        {
            if(visualAdded is MessageBoxResultButton addBtn)
            {
                if(addBtn.IsDefault)
                    addBtn.Command = ConfirmButtonCommand;
                else if(addBtn.IsCancel)
                    addBtn.Command = CancelButtonCommand;

                addBtn.Click += Btn_Click;
            }
            if (visualRemoved is MessageBoxResultButton removeBtn)
            {
                removeBtn.Click -= Btn_Click;
            }
            base.OnVisualChildrenChanged(visualAdded, visualRemoved);
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as MessageBoxResultButton;
            SetMessageButtonResult(btn.MessageBoxResult);

            CurrentWindow.Close();
        }
    }
}
