using System;
using System.Windows;
using System.Windows.Controls;
using System.Globalization;

namespace Controls
{
    public sealed class DatePrinter : Control
    {
        static DatePrinter()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DatePrinter), new FrameworkPropertyMetadata(typeof(DatePrinter)));
        }

        CultureInfo _cultureInfo;
        string _monthAndDayFormat;
        public override void OnApplyTemplate()
        {   
            _cultureInfo = string.IsNullOrEmpty(CultureTag) ? CultureInfo.CurrentCulture : new CultureInfo(CultureTag);
            _monthAndDayFormat = (CultureTag = _cultureInfo.IetfLanguageTag) == "ko-KR" ? "M월 d일(ddd)" : "ddd, MMM dd";
            base.OnApplyTemplate();
        }
        
        public string CultureTag
        {
            get { return (string)GetValue(CultureTagProperty); }
            set { SetValue(CultureTagProperty, value); }
        }
        
        public static readonly DependencyProperty CultureTagProperty =
            DependencyProperty.Register("CultureTag", typeof(string), typeof(DatePrinter), new PropertyMetadata(default(string)));

        public DateTime DisplayDate
        {
            get { return (DateTime)GetValue(DisplayDateProperty); }
            set { SetValue(DisplayDateProperty, value); }
        }

        public static readonly DependencyProperty DisplayDateProperty =
            DependencyProperty.Register("DisplayDate", typeof(DateTime), typeof(DatePrinter), new PropertyMetadata(default(DateTime), DisplayDateChanged));

        private static void DisplayDateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            => (d as DatePrinter)?.UpdateDate();

        public void UpdateDate()
        {
            Year = DisplayDate.ToString("yyyy", _cultureInfo);
            MonthAndDay = DisplayDate.ToString(_monthAndDayFormat, _cultureInfo);
        }

        public string Year
        {
            get { return (string)GetValue(YearProperty); }
            set { SetValue(YearProperty, value); }
        }

        public static readonly DependencyProperty YearProperty =
            DependencyProperty.Register("Year", typeof(string), typeof(DatePrinter), new PropertyMetadata(default(string)));

        public string MonthAndDay
        {
            get { return (string)GetValue(MonthAndDayProperty); }
            set { SetValue(MonthAndDayProperty, value); }
        }

        public static readonly DependencyProperty MonthAndDayProperty =
            DependencyProperty.Register("MonthAndDay", typeof(string), typeof(DatePrinter), new PropertyMetadata(default(string)));
    }
}
