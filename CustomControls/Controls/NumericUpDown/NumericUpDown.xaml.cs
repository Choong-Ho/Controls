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
    /// NumericUpDown.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class NumericUpDown : UserControl
    {
        public NumericUpDown()
        {
            InitializeComponent();
        }
        
        public bool IsLimit
        {
            get { return (bool)GetValue(IsLimitProperty); }
            set { SetValue(IsLimitProperty, value); }
        }
        
        public static readonly DependencyProperty IsLimitProperty =
            DependencyProperty.Register("IsLimit", typeof(bool), typeof(NumericUpDown), new PropertyMetadata(default(bool)));
        
        public int MinimumValue
        {
            internal get { return (int)GetValue(MinimumValueProperty); }
            set { SetValue(MinimumValueProperty, value); }
        }
        
        public static readonly DependencyProperty MinimumValueProperty =
            DependencyProperty.Register("MinimumValue", typeof(int), typeof(NumericUpDown), new PropertyMetadata(0));
        
        public int MaximumValue
        {
            internal get { return (int)GetValue(MaximumValueProperty); }
            set { SetValue(MaximumValueProperty, value); }
        }
        
        public static readonly DependencyProperty MaximumValueProperty =
            DependencyProperty.Register("MaximumValue", typeof(int), typeof(NumericUpDown), new PropertyMetadata(0));
        
        public string RegexMask
        {
            internal get { return (string)GetValue(RegexMaskProperty); }
            set { SetValue(RegexMaskProperty, value); }
        }

        public static readonly DependencyProperty RegexMaskProperty =
            DependencyProperty.Register("RegexMask", typeof(string), typeof(NumericUpDown), 
                new PropertyMetadata(@"^0*([0-9]+)$"));//초기 패턴은 무조건 숫자만 받게 적용
        
        public int CurrentValue
        {
            get { return (int)GetValue(CurrentValueProperty); }
            set { SetValue(CurrentValueProperty, value); }
        }

        public static readonly DependencyProperty CurrentValueProperty =
            DependencyProperty.Register("CurrentValue", typeof(int), typeof(NumericUpDown), new PropertyMetadata(default(int)));

        private void IncreaseCurrentValue(object sender, RoutedEventArgs e)
        {
            if (IsLimit && MaximumValue > CurrentValue)
                CurrentValue += 1;
        }

        private void DecreaseCurrentValue(object sender, RoutedEventArgs e)
        {
            if (IsLimit && MinimumValue < CurrentValue)
                CurrentValue -= 1;
        }
    }
}
