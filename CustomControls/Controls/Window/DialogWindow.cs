using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Shell;
using MVVM;


namespace Controls
{
    [TemplatePart(Name = "ContentPanelElement", Type = typeof(Panel))]
    [TemplatePart(Name = "ResultButtonsPanelElement", Type = typeof(MessageButtonsPanel))]
    public sealed class DialogWindow : CWindow, IDialogWindow
    {
        static DialogWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DialogWindow), new FrameworkPropertyMetadata(typeof(DialogWindow)));
        }

        private MessageBoxResult _result;
        
        public ICommand ConfirmCommand
        {
            get { return (ICommand)GetValue(ConfirmCommandProperty); }
            set { SetValue(ConfirmCommandProperty, value); }
        }

        public static readonly DependencyProperty ConfirmCommandProperty =
            DependencyProperty.Register("ConfirmCommand", typeof(ICommand), typeof(CWindow), new PropertyMetadata(default(ICommand)));
        
        public ICommand CancelCommand
        {
            get { return (ICommand)GetValue(CancelCommandProperty); }
            set { SetValue(CancelCommandProperty, value); }
        }

        public static readonly DependencyProperty CancelCommandProperty =
            DependencyProperty.Register("CancelCommand", typeof(ICommand), typeof(CWindow), new PropertyMetadata(default(ICommand)));
       
        public Panel ContentPanelElement { get; private set; }
        public MessageButtonsPanel ResultButtonsPanelElement { get; private set; }
        
        public MessageBoxButton MessageBoxButtonInfo
        {
            get { return (MessageBoxButton)GetValue(MessageBoxButtonInfoProperty); }
            set { SetValue(MessageBoxButtonInfoProperty, value); }
        }
        
        public static readonly DependencyProperty MessageBoxButtonInfoProperty =
            DependencyProperty.Register("MessageBoxButtonInfo", typeof(MessageBoxButton), typeof(DialogWindow), new PropertyMetadata(default(MessageBoxButton)));

        public DialogWindow()
        {
            AllowsTransparency = true;
        }

        public override void OnApplyTemplate()
        {
            SetTemplate(DataContext as DialogBaseViewModel);
            base.OnApplyTemplate();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            //넓이 높이의 최솟값을 지정한다.(ResizeMode가 CanResize일 때를 위해..)
            MinWidth = ActualWidth;
            MinHeight = ActualHeight;

            SizeToContent = SizeToContent.Manual;
            //WidthAndHeight으로 하면 사이즈 변경이 발생하지 않은 상태서 최대화를 하면 사이즈에 변화가 없다!
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (_result == MessageBoxResult.OK || _result == MessageBoxResult.Yes)
                ConfirmCommand?.Execute(e);
            else if (_result == MessageBoxResult.Cancel)//alt +f4 && close button && cancel button 고로 취소 버튼 외의 것 에서 취소를 할 때에 대한 확인 작업 필요
                CancelCommand?.Execute(e);

            base.OnClosing(e);
        }

        protected override void CloseWindow(object sender, RoutedEventArgs e)
        {
            _result = MessageBoxResult.Cancel;
            
            base.CloseWindow(sender, e);
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);

            if ((e.Key == Key.System && e.SystemKey == Key.F4)
                && (MessageBoxButtonInfo == MessageBoxButton.OKCancel || MessageBoxButtonInfo == MessageBoxButton.YesNoCancel))
            {
                _result = MessageBoxResult.Cancel;
                Close();
            }
        }

        private void SetTemplate(DialogBaseViewModel vm)
        {
            if (vm == null)
                throw new InvalidCastException("DataContext는 DialogBaseViewModel에 의해 상속된 Class여야 합니다!");

            if (vm.ViewType == null)
                throw new NullReferenceException("ViewType은 Null일 수 없습니다, ViewType을 정의하세요!");

            ContentPanelElement = GetTemplateChild("ContentPanel") as Panel;
            if (ContentPanelElement == null)
                throw new InvalidCastException("ContentPanel이 확인 되지 않습니다,  XAML코드를 확인하세요!");

            ResultButtonsPanelElement = GetTemplateChild("ButtonsPanel") as MessageButtonsPanel;
            if (ResultButtonsPanelElement == null)
                throw new InvalidCastException("ButtonsPanel(MessageButtonsPanel)이 확인 되지 않습니다,  XAML코드를 확인하세요!");

            ResultButtonsPanelElement.MessageButtonResultChanged += result => _result = result;

            SetContent(vm);
        }

        private void SetContent(DialogBaseViewModel vm)
        {
            var content = Activator.CreateInstance(vm.ViewType) as FrameworkElement;
            content.DataContext = vm;

            ContentPanelElement.Children.Add(content);
        }

        private void SetMember(Window owner, string title, MessageBoxButton msgButton, DialogBaseViewModel vm, ResizeMode mode)
        {
            if (owner != null)
            {
                this.Owner = owner;
                this.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            }
            else
            {
                this.Topmost = true;
                this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }

            WindowChrome.SetWindowChrome(this, new WindowChrome
            {
                NonClientFrameEdges = NonClientFrameEdges.None,
                CaptionHeight = 26,
                UseAeroCaptionButtons = false,
                ResizeBorderThickness =  new Thickness(mode == ResizeMode.NoResize ? 0  : 5)
            });

            this.Title = title;
            this.ResizeMode = mode;
            this.DataContext = vm;
            this.MessageBoxButtonInfo = msgButton;
            this.ConfirmCommand = vm.ConfirmCommand;
            this.CancelCommand = vm.CancelCommand;
        }

        public MessageBoxResult ShowDialog(string title, MessageBoxButton msgButton, DialogBaseViewModel vm, 
            ResizeMode mode = ResizeMode.NoResize)
        {
            SetMember(null, title, msgButton, vm, mode);
            ShowDialog();
            
            return _result;
        }

        public MessageBoxResult ShowDialog(Window owner, string title, MessageBoxButton msgButton, DialogBaseViewModel vm, 
            ResizeMode mode = ResizeMode.NoResize)
        {
            SetMember(owner, title, msgButton, vm, mode);
            ShowDialog();

            return _result;
        }

        public void CloseDialog()
        {
            _result = MessageBoxResult.None;
            Close();
        }
    }
}
