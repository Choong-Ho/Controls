using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    internal abstract class DateTimeTextBox : TextBox
    {
        protected readonly Regex _regex = new Regex("[^0-9]+");

        private ICommand _downKeyCommand;
        public ICommand DownKeyCommand
            => _downKeyCommand ?? (_downKeyCommand = new ControlCommand<object>(_ => DecreaseValue()));

        private ICommand _upKeyCommand;
        public ICommand UpKeyCommand
            => _upKeyCommand ?? (_upKeyCommand = new ControlCommand<object>(_ => IncreaseValue()));

        private ICommand _leftOrRightKeyCommand;
        public ICommand LeftAndRightKeyCommand
            => _leftOrRightKeyCommand ?? (_leftOrRightKeyCommand = new ControlCommand<DateTimeTextBox>(MoveToDifferentTextBox));

        protected abstract void InputValue(string inputTxt, int maxValue, int minValue);
        protected abstract void DecreaseValue();
        protected abstract void IncreaseValue();

        protected void SetDecreaseValue(int value, int maxValue, string digits)
        {
            if (value >= 0)
                Text = value.ToString(digits);
            else
                Text = maxValue.ToString();
        }

        protected void SetIncreaseValue(int value, int maxValue, string minText, string digits)
        {
            if (value <= maxValue)
                Text = value.ToString(digits);
            else
                Text = minText;
        }

        private void MoveToDifferentTextBox(DateTimeTextBox textBox)
        {
            textBox.Focus();
            textBox.CaretIndex = textBox.Text.Length;
        }

        protected bool HasNum(string text)
            => !_regex.IsMatch(text);

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Back || e.Key == Key.Delete)
                e.Handled = true;

            base.OnPreviewKeyDown(e);
        }

        protected override void OnGotMouseCapture(MouseEventArgs e)
        {
            base.OnGotMouseCapture(e);

            CaretIndex = Text.Length;
        }
    }
}
