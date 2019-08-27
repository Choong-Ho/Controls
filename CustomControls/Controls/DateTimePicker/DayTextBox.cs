using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Controls
{
    internal class DayTextBox : DateTimeTextBox
    {
        private const string MIN_DAY = "01";
        private const string DAY_DIGITS = "D2";
        private const string DAY_DEFAULT = "0";

        #region 선택 년/월(마지막 일을 알기 위해)
        public string Year
        {
            get { return (string)GetValue(YearProperty); }
            set { SetValue(YearProperty, value); }
        }
        
        public static readonly DependencyProperty YearProperty =
            DependencyProperty.Register("Year", typeof(string), typeof(DayTextBox), new PropertyMetadata(null));
        
        public string Month
        {
            get { return (string)GetValue(MonthProperty); }
            set { SetValue(MonthProperty, value); }
        }
        
        public static readonly DependencyProperty MonthProperty =
            DependencyProperty.Register("Month", typeof(string), typeof(DayTextBox), new PropertyMetadata(null));
        #endregion

        protected override void DecreaseValue()
        {
            int currentValue = int.Parse(Text);
            currentValue = --currentValue == 0 ? GetMaxDay() : currentValue;

            SetDecreaseValue(currentValue, GetMaxDay(), DAY_DIGITS);
        }

        protected override void IncreaseValue()
        {
            int currentValue = int.Parse(Text);

            SetIncreaseValue(++currentValue, GetMaxDay(), MIN_DAY, DAY_DIGITS);
        }

        protected override void InputValue(string inputTxt, int maxValue, int minValue)
        {
            if (HasNum(inputTxt))
            {
                var mergeValue = int.Parse(Text.Remove(0, 1) + inputTxt);

                if (mergeValue <= maxValue && minValue < mergeValue)
                    Text = mergeValue.ToString(DAY_DIGITS);
                else if (mergeValue > maxValue || mergeValue == 0)
                    Text = MIN_DAY;
                else
                    Text = DAY_DEFAULT + inputTxt;
            }
        }

        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            e.Handled = true;

            InputValue(e.Text, GetMaxDay(), 1);
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            var maxDay = GetMaxDay();
            if(int.Parse(Text) > maxDay)
                Text = maxDay.ToString();

            base.OnGotFocus(e);
        }

        private int GetMaxDay()
            => DateTime.DaysInMonth(int.Parse(Year), int.Parse(Month));
    }
}
