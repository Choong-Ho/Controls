using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Windows.Interop;
using System.Windows.Input;
using System.Globalization;
using MVVM;
using Controls.Config;

namespace Controls
{
    [TemplatePart(Name = "CloseButtonElement", Type = typeof(PathButton))]
    [TemplatePart(Name = "_statusIconElement", Type = typeof(System.Windows.Controls.Image))]
    [TemplatePart(Name = "_contentElement", Type = typeof(TextBlock))]
    [TemplatePart(Name = "_buttonsPanelElement", Type = typeof(MessageButtonsPanel))]
    [TemplatePart(Name = "TitleGridElement", Type = typeof(Grid))]
    public class MessageBoxWindow : Window, IMessageBoxWindow
    {
        private System.Windows.Controls.Image _statusIconElement;
        private TextBlock _contentElement;
        private MessageButtonsPanel _buttonsPanelElement;
        
        private string _msg;
        private MessageBoxImage _msgIcon;

        private MessageBoxResult _result;
        public MessageBoxResult Result
            => _result;

        private PathButton _closeButtonElement;
        private PathButton CloseButtonElement
        {
            get => _closeButtonElement;
            set
            {
                RemoveClickEvent(_closeButtonElement, CloseWindow);
                if (value != null)
                {
                    _closeButtonElement = value;
                    _closeButtonElement.Click += CloseWindow;
                }
            }
        }

        private Grid _titleGridElement;
        private Grid TitleGridElement
        {
            get => _titleGridElement;
            set
            {
                RemoveLeftMouseButtonDown(_titleGridElement, MoveWindow);
                if (value != null)
                {
                    _titleGridElement = value;
                    _titleGridElement.MouseLeftButtonDown += MoveWindow;
                }
            }
        }
        
        public MessageBoxButton? MessageBoxButtonInfo
        {
            get { return (MessageBoxButton?)GetValue(MessageBoxButtonInfoProperty); }
            set { SetValue(MessageBoxButtonInfoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MessageBoxButtonInfo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MessageBoxButtonInfoProperty =
            DependencyProperty.Register("MessageBoxButtonInfo", typeof(MessageBoxButton?), typeof(MessageBoxWindow), new PropertyMetadata(default(MessageBoxButton?)));

        private void MoveWindow(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        static MessageBoxWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MessageBoxWindow),
                new FrameworkPropertyMetadata(typeof(MessageBoxWindow)));
        }

        public new MessageBoxResult ShowDialog()
        {
            base.ShowDialog();
            return _result;
        }

        public MessageBoxWindow()
        { }

        public MessageBoxWindow(string messageBoxText, string caption = "",
                                MessageBoxButton button = MessageBoxButton.OK,
                                MessageBoxImage icon = MessageBoxImage.None,
                                MessageBoxResult defaultResult = MessageBoxResult.None)
        {
            InitializeMessageBox(messageBoxText, caption, button, icon, defaultResult);
        }

        public MessageBoxWindow(Window owner, string messageBoxText, string caption = "",
                                MessageBoxButton button = MessageBoxButton.OK,
                                MessageBoxImage icon = MessageBoxImage.None,
                                MessageBoxResult defaultResult = MessageBoxResult.None)
        {
            InitializeMessageBox(owner, messageBoxText, caption, button, icon, defaultResult);
        }

        private MessageBoxResult Show(string messageBoxText, string caption = "", MessageBoxButton button = MessageBoxButton.OK, MessageBoxImage icon = MessageBoxImage.None, MessageBoxResult defaultResult = MessageBoxResult.None)
        {
            InitializeMessageBox(messageBoxText, caption, button, icon, defaultResult);
            return ShowDialog();
        }

        public MessageBoxResult ShowError(string messageBoxText, MessageBoxButton button = MessageBoxButton.OK, MessageBoxResult defaultResult = MessageBoxResult.None)
        {
            return this.Show(messageBoxText, MessageTitleConfig.Error, button, MessageBoxImage.Error, defaultResult);
        }

        public MessageBoxResult ShowWarning(string messageBoxText, MessageBoxButton button = MessageBoxButton.OK, MessageBoxResult defaultResult = MessageBoxResult.None)
        {
            return this.Show(messageBoxText, MessageTitleConfig.Warning, button, MessageBoxImage.Warning, defaultResult);
        }

        public MessageBoxResult ShowQuestion(string messageBoxText, MessageBoxButton button = MessageBoxButton.OKCancel, MessageBoxResult defaultResult = MessageBoxResult.None)
        {
            return this.Show(messageBoxText, MessageTitleConfig.Question, button, MessageBoxImage.Question, defaultResult);
        }

        public MessageBoxResult ShowInformation(string messageBoxText, MessageBoxButton button = MessageBoxButton.OK, MessageBoxResult defaultResult = MessageBoxResult.None)
        {
            return this.Show(messageBoxText, MessageTitleConfig.Information, button, MessageBoxImage.Information, defaultResult);
        }


        public MessageBoxResult Show(Window owner, string messageBoxText, string caption = "", MessageBoxButton button = MessageBoxButton.OK, MessageBoxImage icon = MessageBoxImage.None, MessageBoxResult defaultResult = MessageBoxResult.None)
        {
            InitializeMessageBox(owner, messageBoxText, caption, button, icon, defaultResult);
            return ShowDialog();
        }

        private void InitializeMessageBox(string messageBoxText, string caption,
                                MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult)
        {
            Topmost = true;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            MessageBoxButtonInfo = button;
            Title = caption;
            _msg = messageBoxText;
            _msgIcon = icon;
            _result = defaultResult;
        }

        private void InitializeMessageBox(Window owner, string messageBoxText, string caption,
                                MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult)
        {
            Owner = owner;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            MessageBoxButtonInfo = button;
            Title = caption;
            _msg = messageBoxText;
            _msgIcon = icon;
            _result = defaultResult;
        }

        public override void OnApplyTemplate()
        {
            SetMessageWindowFormat();

            base.OnApplyTemplate();
        }

        private void RemoveClickEvent(ButtonBase button, RoutedEventHandler handler)
        {
            if (button != null)
                button.Click -= handler;
        }

        private void RemoveLeftMouseButtonDown(FrameworkElement frameWork, MouseButtonEventHandler handler)
        {
            if (frameWork != null)
                frameWork.MouseLeftButtonDown -= handler;
        }

        private void CloseWindow(object sender, RoutedEventArgs e) => this.Cancel();

        private void SetMessageWindowFormat()
        {
            TitleGridElement = GetTemplateChild("TitleGrid") as Grid;
            CloseButtonElement = GetTemplateChild("CloseButton") as PathButton;
            SetMessageIcon();
            SetContent();
            SetMessageWindowButtons();
        }

        private void SetContent()
        {
            _contentElement = GetTemplateChild("Content") as TextBlock;
            if (_contentElement == null)
                throw new MissingFieldException("XAML 코드에 Content(TextBlock)가 있는지 확인하세요!");

            _contentElement.Text = _msg;
        }

        private void SetMessageIcon()
        {
            _statusIconElement = GetTemplateChild("StatusIcon") as System.Windows.Controls.Image;
            if (_statusIconElement == null)
                throw new MissingFieldException("XAML 코드에 StatusIcon(Image)이 있는지 확인하세요!");
            
            switch (_msgIcon)
            {
                case MessageBoxImage.None:
                    _statusIconElement.Visibility = Visibility.Collapsed;
                    break;
                case MessageBoxImage.Error://Hand and Stop
                    _statusIconElement.Source = GetIconSource(SystemIcons.Error);
                    break;
                case MessageBoxImage.Question:
                    _statusIconElement.Source = GetIconSource(SystemIcons.Question);
                    break;
                case MessageBoxImage.Warning://Exclamation
                    _statusIconElement.Source = GetIconSource(SystemIcons.Warning);
                    break;
                case MessageBoxImage.Information://Asterisk
                    _statusIconElement.Source = GetIconSource(SystemIcons.Information);
                    break;
            }
        }

        private BitmapSource GetIconSource(Icon icon)
        {
            BitmapSource source = Imaging.CreateBitmapSourceFromHIcon(icon.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            source.Freeze();

            return source;
        }

        private void SetMessageWindowButtons()
        {
            _buttonsPanelElement = GetTemplateChild("ButtonsPanel") as MessageButtonsPanel;
            if (_buttonsPanelElement == null)
                throw new InvalidCastException("ButtonsPanel(MessageButtonsPanel)이 확인 되지 않습니다,  XAML코드를 확인하세요!");

            _buttonsPanelElement.MessageButtonResultChanged += (result) => _result = result;
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);

            if ((e.Key == Key.System && e.SystemKey == Key.F4)
                && (MessageBoxButtonInfo == MessageBoxButton.OKCancel || MessageBoxButtonInfo == MessageBoxButton.YesNoCancel))
                Cancel();
        }

        private void Cancel()
        {
            _result = MessageBoxResult.Cancel;
            Close();
        }
    }

    public static class MessageBox
    {
        [SecurityCritical]
        public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult)
        {
            var w = new MessageBoxWindow(messageBoxText, caption, button, icon, defaultResult);

            return w.ShowDialog();
        }

        [SecurityCritical]
        public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon)
        {
            var w = new MessageBoxWindow(messageBoxText, caption, button, icon);

            return w.ShowDialog();
        }

        [SecurityCritical]
        public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button)
        {
            var w = new MessageBoxWindow(messageBoxText, caption, button);

            return w.ShowDialog();
        }

        [SecurityCritical]
        public static MessageBoxResult Show(string messageBoxText, string caption)
        {
            var w = new MessageBoxWindow(messageBoxText, caption);

            return w.ShowDialog();
        }

        [SecurityCritical]
        public static MessageBoxResult Show(string messageBoxText)
        {
            var w = new MessageBoxWindow(messageBoxText);

            return w.ShowDialog();
        }

        [SecurityCritical]
        public static MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult)
        {
            var w = new MessageBoxWindow(owner, messageBoxText, caption, button, icon, defaultResult);

            return w.ShowDialog();
        }

        [SecurityCritical]
        public static MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon)
        {
            var w = new MessageBoxWindow(owner, messageBoxText, caption, button, icon);

            return w.ShowDialog();
        }

        [SecurityCritical]
        public static MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button)
        {
            var w = new MessageBoxWindow(owner, messageBoxText, caption, button);

            return w.ShowDialog();
        }

        [SecurityCritical]
        public static MessageBoxResult Show(Window owner, string messageBoxText, string caption)
        {
            var w = new MessageBoxWindow(owner, messageBoxText, caption);

            return w.ShowDialog();
        }

        [SecurityCritical]
        public static MessageBoxResult Show(Window owner, string messageBoxText)
        {
            var w = new MessageBoxWindow(owner, messageBoxText);

            return w.ShowDialog();
        }
    }
}
