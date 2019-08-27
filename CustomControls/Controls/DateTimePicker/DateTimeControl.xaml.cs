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
    /// <summary>
    /// DateTimeControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DateTimeControl : Border
    {
        public DateTimeControl()
        {
            InitializeComponent();

            calendar.SelectedDatesChanged += Calendar_SelectedDatesChanged;
            chkCalendar.Checked += ChkCalendar_Checked;
        }

        private void ChkCalendar_Checked(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(CalendarOnEvent));
        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(CalendarOffEvent, calendarPopup));
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                RaiseEvent(new RoutedEventArgs(DateUpdateEvent));
            }
            else
                base.OnPreviewKeyDown(e);
        }

        public static readonly RoutedEvent DateUpdateEvent = EventManager.RegisterRoutedEvent(
            "DateUpdate", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(DateTimeControl));

        public event RoutedEventHandler DateUpdate
        {
            add { AddHandler(DateUpdateEvent, value); }
            remove { RemoveHandler(DateUpdateEvent, value); }
        }

        public static readonly RoutedEvent CalendarOnEvent = EventManager.RegisterRoutedEvent(
            "CalendarOn", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(DateTimeControl));

        public event RoutedEventHandler CalendarOn
        {
            add { AddHandler(CalendarOnEvent, value); }
            remove { RemoveHandler(CalendarOnEvent, value); }
        }

        public static readonly RoutedEvent CalendarOffEvent = EventManager.RegisterRoutedEvent(
            "CalendarOff", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(DateTimeControl));

        public event RoutedEventHandler CalendarOff
        {
            add { AddHandler(CalendarOffEvent, value); }
            remove { RemoveHandler(CalendarOffEvent, value); }
        }

        public Color FocusedColor
        {
            get { return (Color)GetValue(FocusedColorProperty); }
            set { SetValue(FocusedColorProperty, value); }
        }

        public static readonly DependencyProperty FocusedColorProperty =
            DependencyProperty.Register("FocusedColor", typeof(Color), typeof(DateTimeControl), new PropertyMetadata(Colors.Transparent));

        #region 텍스트
        public int Year
        {
            get { return (int)GetValue(YearProperty); }
            set { SetValue(YearProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Year.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty YearProperty =
            DependencyProperty.Register("Year", typeof(int), typeof(DateTimeControl), new PropertyMetadata(1));
        
        public int Month
        {
            get { return (int)GetValue(MonthProperty); }
            set { SetValue(MonthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Month.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MonthProperty =
            DependencyProperty.Register("Month", typeof(int), typeof(DateTimeControl), new PropertyMetadata(1));
        
        public int Day
        {
            get { return (int)GetValue(DayProperty); }
            set { SetValue(DayProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Day.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DayProperty =
            DependencyProperty.Register("Day", typeof(int), typeof(DateTimeControl), new PropertyMetadata(1));
        
        public int Hour
        {
            get { return (int)GetValue(HourProperty); }
            set { SetValue(HourProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Hour.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HourProperty =
            DependencyProperty.Register("Hour", typeof(int), typeof(DateTimeControl), new PropertyMetadata(0));
        
        public int Minute
        {
            get { return (int)GetValue(MinuteProperty); }
            set { SetValue(MinuteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Minute.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinuteProperty =
            DependencyProperty.Register("Minute", typeof(int), typeof(DateTimeControl), new PropertyMetadata(0));
        
        public int Second
        {
            get { return (int)GetValue(SecondProperty); }
            set { SetValue(SecondProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Second.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SecondProperty =
            DependencyProperty.Register("Second", typeof(int), typeof(DateTimeControl), new PropertyMetadata(0));
        #endregion

        #region 달력
        public DateTime DisplayDateTime
        {
            get { return (DateTime)GetValue(DisplayDateTimeProperty); }
            set { SetValue(DisplayDateTimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DisplayDateTime.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisplayDateTimeProperty =
            DependencyProperty.Register("DisplayDateTime", typeof(DateTime), typeof(DateTimeControl), new PropertyMetadata(DateTime.Now));

        public DateTime? SelectedDateTime
        {
            get { return (DateTime?)GetValue(SelectedDateTimeProperty); }
            set { SetValue(SelectedDateTimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedDateTime.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedDateTimeProperty =
            DependencyProperty.Register("SelectedDateTime", typeof(DateTime?), typeof(DateTimeControl), new PropertyMetadata(null));
        #endregion
    }
}
