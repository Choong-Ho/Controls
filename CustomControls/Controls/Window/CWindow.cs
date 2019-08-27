using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Controls
{
    [TemplatePart(Name = "HideButtonElement", Type = typeof(PathButton))]
    [TemplatePart(Name = "StateButtonElement", Type = typeof(WindowStatePathToggleButton))]
    [TemplatePart(Name = "CloseButtonElement", Type = typeof(PathButton))]
    public class CWindow : Window
    {
        private HwndSource _hwndSource;

        public Brush TitleBackground
        {
            get { return (Brush)GetValue(TitleBackgroundProperty); }
            set { SetValue(TitleBackgroundProperty, value); }
        }
        
        public static readonly DependencyProperty TitleBackgroundProperty =
            DependencyProperty.Register("TitleBackground", typeof(Brush), typeof(CWindow), new PropertyMetadata(default(Brush)));
        
        static CWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CWindow), new FrameworkPropertyMetadata(typeof(CWindow)));
        }

        public CWindow()
        {
            WindowStyle = WindowStyle.None;
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            if (ResizeMode == ResizeMode.CanResize)
            {
                _hwndSource = HwndSource.FromHwnd(new WindowInteropHelper(this).Handle);
                _hwndSource.AddHook(new HwndSourceHook(WindowExtension.WindowProc));
            }

            base.OnSourceInitialized(e);
        }

        public override void OnApplyTemplate()
        {
            HideButtonElement = GetTemplateChild("HideButton") as PathButton;
            StateButtonElement = GetTemplateChild("StateButton") as WindowStatePathToggleButton;
            CloseButtonElement = GetTemplateChild("CloseButton") as PathButton;

            base.OnApplyTemplate();
        }

        protected override void OnClosed(EventArgs e)
        {
            if(_hwndSource != null)
            {
                _hwndSource.RemoveHook(WindowExtension.WindowProc);
                _hwndSource.Dispose();
                _hwndSource = null;
            }

            base.OnClosed(e);
        }

        private PathButton _hideButtonElement;
        private PathButton HideButtonElement
        {
            get => _hideButtonElement;
            set
            {
                RemoveClickEvent(_hideButtonElement, HideWindow);
                if (value != null)
                {
                    _hideButtonElement = value;
                    _hideButtonElement.Click += HideWindow;
                }
            }
        }

        private WindowStatePathToggleButton _stateButtonElement;
        private WindowStatePathToggleButton StateButtonElement
        {
            get => _stateButtonElement;
            set
            {
                RemoveClickEvent(_stateButtonElement, WindowStateChanged);
                if (value != null)
                {
                    _stateButtonElement = value;
                    _stateButtonElement.Click += WindowStateChanged;
                }
            }
        }

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

        private void HideWindow(object sender, RoutedEventArgs e) => WindowState = System.Windows.WindowState.Minimized;

        private void WindowStateChanged(object sender, RoutedEventArgs e)
            => WindowState = WindowState == System.Windows.WindowState.Normal ? System.Windows.WindowState.Maximized : System.Windows.WindowState.Normal;

        protected virtual void CloseWindow(object sender, RoutedEventArgs e) => this.Close();

        private void RemoveClickEvent(ButtonBase button, RoutedEventHandler handler)
        {
            if (button != null)
                button.Click -= handler;
        }
        
        public string OptionName
        {
            get { return (string)GetValue(OptionNameProperty); }
            set { SetValue(OptionNameProperty, value); }
        }
        
        public static readonly DependencyProperty OptionNameProperty =
            DependencyProperty.Register("OptionName", typeof(string), typeof(CWindow), new PropertyMetadata(null));
        
        public string OptionValue
        {
            get { return (string)GetValue(OptionValueProperty); }
            set { SetValue(OptionValueProperty, value); }
        }
        
        public static readonly DependencyProperty OptionValueProperty =
            DependencyProperty.Register("OptionValue", typeof(string), typeof(CWindow), new PropertyMetadata(null));
    }
}
