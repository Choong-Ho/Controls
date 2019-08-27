using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Controls
{
    public class Calendar : System.Windows.Controls.Calendar
    {
        static Calendar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Calendar),
                new FrameworkPropertyMetadata(typeof(Calendar)));
        }
        
        public Brush TopBackground
        {
            get { return (Brush)GetValue(TopBackgroundProperty); }
            set { SetValue(TopBackgroundProperty, value); }
        }
        
        public static readonly DependencyProperty TopBackgroundProperty =
            DependencyProperty.Register("TopBackground", typeof(Brush), typeof(Calendar), new PropertyMetadata(default(Brush)));
    }
}
