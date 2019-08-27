using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Controls
{
    public enum TimeInfo { Hour, Minute, Second }

    internal sealed class TimeTextBox : DateTimeTextBox
    {
        private const string TIME_DIGITS = "D2";
        private const string TIME_DEFAULT = "0";
        private const string TIME_MIN = "00";

        private const int MAX_HOUR = 23;
        private const int MAX_MINUTE_AND_SECOND = 59;
        
        public TimeInfo? SelectedTimeInfo
        {
            get { return (TimeInfo?)GetValue(SelectedTimeInfoProperty); }
            set { SetValue(SelectedTimeInfoProperty, value); }
        }

        public static readonly DependencyProperty SelectedTimeInfoProperty =
            DependencyProperty.Register("SelectedTimeInfo", typeof(TimeInfo?), typeof(TimeTextBox), new PropertyMetadata(null));
        
        protected override void DecreaseValue()
        {
            int currentValue = int.Parse(Text);

            if (SelectedTimeInfo == TimeInfo.Hour)
                SetDecreaseValue(--currentValue, MAX_HOUR, TIME_DIGITS);
            else
                SetDecreaseValue(--currentValue, MAX_MINUTE_AND_SECOND, TIME_DIGITS);
        }

        protected override void IncreaseValue()
        {
            int currentValue = int.Parse(Text);

            if (SelectedTimeInfo == TimeInfo.Hour)
                SetIncreaseValue(++currentValue, MAX_HOUR, TIME_MIN, TIME_DIGITS);
            else
                SetIncreaseValue(++currentValue, MAX_MINUTE_AND_SECOND, TIME_MIN, TIME_DIGITS);
        }

        protected override void InputValue(string inputTxt, int maxValue, int minValue)
        {
            if (HasNum(inputTxt))
            {
                var mergeValue = Text.Length < MaxLength ? int.Parse(Text + inputTxt) : int.Parse(Text.Remove(0, 1) + inputTxt);
                if (mergeValue <= maxValue)
                    Text = mergeValue.ToString(TIME_DIGITS);
                else
                    Text = TIME_DEFAULT + inputTxt;
            }
        }

        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            e.Handled = true;
            if (SelectedTimeInfo == TimeInfo.Hour)
                InputValue(e.Text, MAX_HOUR, 0);
            else
                InputValue(e.Text, MAX_MINUTE_AND_SECOND, 0);
        }
    }
}
