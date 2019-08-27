using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Controls
{
    public class WindowStatePathToggleButton : ToggleButton
    {
        static WindowStatePathToggleButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WindowStatePathToggleButton),
                new FrameworkPropertyMetadata(typeof(WindowStatePathToggleButton)));
        }

        public Geometry NormalData
        {
            get { return (Geometry)GetValue(NormalDataProperty); }
            set { SetValue(NormalDataProperty, value); }
        }

        public static readonly DependencyProperty NormalDataProperty =
            DependencyProperty.Register("NormalData", typeof(Geometry), typeof(WindowStatePathToggleButton), new PropertyMetadata(default(Geometry)));

        public Geometry MaxData
        {
            get { return (Geometry)GetValue(MaxDataProperty); }
            set { SetValue(MaxDataProperty, value); }
        }

        public static readonly DependencyProperty MaxDataProperty =
            DependencyProperty.Register("MaxData", typeof(Geometry), typeof(WindowStatePathToggleButton), new PropertyMetadata(default(Geometry)));
        
        public Brush NormalColor
        {
            get { return (Brush)GetValue(NormalColorProperty); }
            set { SetValue(NormalColorProperty, value); }
        }

        public static readonly DependencyProperty NormalColorProperty =
            DependencyProperty.Register("NormalColor", typeof(Brush), typeof(WindowStatePathToggleButton), new PropertyMetadata(default(Brush)));

        //public Brush MouseOverColor
        //{
        //    get { return (Brush)GetValue(MouseOverColorProperty); }
        //    set { SetValue(MouseOverColorProperty, value); }
        //}

        //public static readonly DependencyProperty MouseOverColorProperty =
        //    DependencyProperty.Register("MouseOverColor", typeof(Brush), typeof(WindowStatePathToggleButton), new PropertyMetadata(default(Brush)));

        public Brush PressColor
        {
            get { return (Brush)GetValue(PressColorProperty); }
            set { SetValue(PressColorProperty, value); }
        }

        public static readonly DependencyProperty PressColorProperty =
            DependencyProperty.Register("PressColor", typeof(Brush), typeof(WindowStatePathToggleButton), new PropertyMetadata(default(Brush)));
        
        //public double PathWidth
        //{
        //    get { return (double)GetValue(PathWidthProperty); }
        //    set { SetValue(PathWidthProperty, value); }
        //}
        
        //public static readonly DependencyProperty PathWidthProperty =
        //    DependencyProperty.Register("PathWidth", typeof(double), typeof(WindowStatePathToggleButton), new PropertyMetadata(0));
        
        //public double PathHeight
        //{
        //    get { return (double)GetValue(PathHeightProperty); }
        //    set { SetValue(PathHeightProperty, value); }
        //}
        
        //public static readonly DependencyProperty PathHeightProperty =
        //    DependencyProperty.Register("PathHeight", typeof(double), typeof(WindowStatePathToggleButton), new PropertyMetadata(0));
    }
}