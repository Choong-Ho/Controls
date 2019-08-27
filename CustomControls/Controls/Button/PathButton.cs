using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Controls
{
    public class PathButton : Button
    {
        static PathButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PathButton), 
                new FrameworkPropertyMetadata(typeof(PathButton)));
        }

        public Geometry NormalData
        {
            get { return (Geometry)GetValue(NormalDataProperty); }
            set { SetValue(NormalDataProperty, value); }
        }
        
        public static readonly DependencyProperty NormalDataProperty =
            DependencyProperty.Register("NormalData", typeof(Geometry), typeof(PathButton), new PropertyMetadata(default(Geometry)));
        
        public Brush NormalColor
        {
            get { return (Brush)GetValue(NormalColorProperty); }
            set { SetValue(NormalColorProperty, value); }
        }

        public static readonly DependencyProperty NormalColorProperty =
            DependencyProperty.Register("NormalColor", typeof(Brush), typeof(PathButton), new PropertyMetadata(default(Brush)));

        public Brush PressColor
        {
            get { return (Brush)GetValue(PressColorProperty); }
            set { SetValue(PressColorProperty, value); }
        }

        public static readonly DependencyProperty PressColorProperty =
            DependencyProperty.Register("PressColor", typeof(Brush), typeof(PathButton), new PropertyMetadata(default(Brush)));

        public double BgStrokeThickness
        {
            get { return (double)GetValue(BgStrokeThicknessProperty); }
            set { SetValue(BgStrokeThicknessProperty, value); }
        }

        public static readonly DependencyProperty BgStrokeThicknessProperty =
            DependencyProperty.Register("BgStrokeThickness", typeof(double), typeof(PathButton), new PropertyMetadata(1d));


    }
}
