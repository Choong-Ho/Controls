using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Controls
{
    public enum DateInfo { Year, Month }

    internal sealed class YearMonthTextBox : DateTimeTextBox
    {
        private const int MAX_YEAR = 9999;
        private const int MIN_YEAR = 1;
        private const int MAX_MONTH = 12;
        private const int MIN_MONTH = 1;

        private const string YEAR_DIGITS = "D4";
        private const string YEAR_MIN = "0001";

        private const string MONTH_DIGITS = "D2";
        private const string MONTH_MIN = "01";
        private const string MONTH_DEFAULT = "0";

        public DateInfo? SeletedDateInfo
        {
            get { return (DateInfo?)GetValue(SeletedDateInfoProperty); }
            set { SetValue(SeletedDateInfoProperty, value); }
        }
        
        public static readonly DependencyProperty SeletedDateInfoProperty =
            DependencyProperty.Register("SeletedDateInfo", typeof(DateInfo?), typeof(YearMonthTextBox), new PropertyMetadata(null));
        
        protected override void DecreaseValue()
        {
            int currentValue = int.Parse(Text);

            if (SeletedDateInfo == DateInfo.Year)
            {
                currentValue = --currentValue == 0 ? MAX_YEAR : currentValue;
                SetDecreaseValue(currentValue, MAX_YEAR, YEAR_DIGITS);
            }
            else
            {
                currentValue = --currentValue == 0 ? MAX_MONTH : currentValue;
                SetDecreaseValue(currentValue, MAX_MONTH, MONTH_DIGITS);
            }
        }

        protected override void IncreaseValue()
        {
            int currentValue = int.Parse(Text);

            if (SeletedDateInfo == DateInfo.Year)
                SetIncreaseValue(++currentValue, MAX_YEAR, YEAR_MIN, YEAR_DIGITS);
            else
                SetIncreaseValue(++currentValue, MAX_MONTH, MONTH_MIN, MONTH_DIGITS);
        }

        protected override void InputValue(string inputTxt, int maxValue, int minValue)
        {
            if (HasNum(inputTxt))
            {
                var mergeValue = int.Parse(Text.Remove(0, 1) + inputTxt);// Text.Length < MaxLength ? int.Parse(Text + inputTxt) : int.Parse(Text.Remove(0, 1) + inputTxt);

                if (mergeValue <= maxValue && minValue < mergeValue)
                    Text = mergeValue.ToString(SeletedDateInfo == DateInfo.Year ? YEAR_DIGITS : MONTH_DIGITS);
                else if(mergeValue > maxValue || mergeValue == 0)
                    Text = (SeletedDateInfo == DateInfo.Year ? YEAR_MIN : MONTH_MIN);
                else
                    Text = (SeletedDateInfo == DateInfo.Year ? YEAR_MIN : MONTH_DEFAULT + inputTxt);
            }
        }

        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            e.Handled = true;
            if (SeletedDateInfo == DateInfo.Year)
                InputValue(e.Text, MAX_YEAR, MIN_YEAR);
            else
                InputValue(e.Text, MAX_MONTH, MIN_MONTH);
        }
    }
}
