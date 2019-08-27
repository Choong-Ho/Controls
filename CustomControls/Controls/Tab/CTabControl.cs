using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Resources;
using MVVM;

namespace Controls
{
    [TemplatePart(Name = "_contentGridElement", Type = typeof(Grid))]
    public class CTabControl : TabControl
    {
        private Grid _contentGridElement;
        private readonly Dictionary<object, FrameworkElement> _contentItems 
            = new Dictionary<object, FrameworkElement>();

        static CTabControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CTabControl), new FrameworkPropertyMetadata(typeof(CTabControl)));
        }
        
        public int SafetySelectedIndex
        {
            get { return (int)GetValue(SafetySelectedIndexProperty); }
            set { SetValue(SafetySelectedIndexProperty, value); }
        }

        public static readonly DependencyProperty SafetySelectedIndexProperty =
            DependencyProperty.Register("SafetySelectedIndex", typeof(int), typeof(CTabControl), new PropertyMetadata(-1, SafetySelectedIndexCallBack));

        private static void SafetySelectedIndexCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if(d is CTabControl tab && tab.IsApplyTemplateCompelted)
                Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ContextIdle, tab.SelectedAction);
        }

        internal readonly Action SelectedAction;

        public CTabControl()
        {
            SelectedAction = () => SelectedIndex = SafetySelectedIndex;
        }

        internal bool IsApplyTemplateCompelted { get; set; }
        public override void OnApplyTemplate()
        {
            IsApplyTemplateCompelted = true;

            _contentGridElement = GetTemplateChild("ContentGrid") as Grid;
            base.OnApplyTemplate();

            if (SafetySelectedIndex != SelectedIndex)
                Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ContextIdle, SelectedAction);
        }
        
        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);

            _contentGridElement?.Children.Clear();
            if (SelectedContent != null)
            {
                var view = _contentItems.ContainsKey(SelectedContent) ?
                _contentItems[SelectedContent] :
                _contentItems[SelectedContent] = CreateTapContent(SelectedContent);

                _contentGridElement.Children.Add(view);
            }
            if(SafetySelectedIndex != SelectedIndex)
                SafetySelectedIndex = SelectedIndex;
        }

        private FrameworkElement CreateTapContent(object selectedContent)
        {
            FrameworkElement content = null;
            if (selectedContent is IViewType vm)
            {
                content = Activator.CreateInstance(vm.ViewType) as FrameworkElement;
                content.DataContext = selectedContent;
            }

            return content;
        }

        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var vm in e.OldItems)
                    _contentItems.Remove(vm);
            }
            else if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                _contentGridElement?.Children.Clear();
                _contentItems.Clear();
            }
            else if (e.Action == NotifyCollectionChangedAction.Move)//드래그앤 드롭(이동 인덱스 확인.. collection.Move(currentIndex, moveIndex))
            {

            }
            base.OnItemsChanged(e);
        }

        public static readonly RoutedEvent TabItemRemoveClickEvent = EventManager.RegisterRoutedEvent(
            "TabItemRemoveClick", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(CTabControl));

        /// <summary>
        /// IsButtonVisible 이 True인 경우에만 이벤트가 실행된다.
        /// </summary>
        public event RoutedEventHandler TabItemRemoveClick
        {
            add { AddHandler(TabItemRemoveClickEvent, value); }
            remove { RemoveHandler(TabItemRemoveClickEvent, value); }
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            var item = new CTabItem();
            item.RemoveClick += (sender, e) =>
            {
                if (this.ItemsSource != null && sender is CTabItem tab)
                {
                    if (MessageBox.Show($"'{tab.Header}'를 지우시겠습니까?", "Delete", MessageBoxButton.OKCancel, MessageBoxImage.Question) != MessageBoxResult.OK)
                        return;

                    var vm = tab.DataContext;
                    (this.ItemsSource as IList).Remove(vm);
                    this.RaiseEvent(new RoutedEventArgs(TabItemRemoveClickEvent, vm));

                    (vm as IDisposable)?.Dispose();
                }
                else
                    this.Items.Remove(sender);

            };
            return item;
        }

        protected override bool IsItemItsOwnContainerOverride(object item) => item is CTabItem;
    }
}
