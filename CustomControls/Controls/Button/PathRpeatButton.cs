using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Controls
{
    public class PathRpeatButton : RepeatButton
    {
        static PathRpeatButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PathRpeatButton),
                new FrameworkPropertyMetadata(typeof(PathRpeatButton)));
        }

        public Geometry Data
        {
            get { return (Geometry)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(Geometry), typeof(PathRpeatButton), new PropertyMetadata(null));

        public Brush PathColor
        {
            get { return (Brush)GetValue(PathColorProperty); }
            set { SetValue(PathColorProperty, value); }
        }

        public static readonly DependencyProperty PathColorProperty =
            DependencyProperty.Register("PathColor", typeof(Brush), typeof(PathRpeatButton), new PropertyMetadata(null));
        
        public Brush MouseOverColor
        {
            get { return (Brush)GetValue(MouseOverColorProperty); }
            set { SetValue(MouseOverColorProperty, value); }
        }
        
        public static readonly DependencyProperty MouseOverColorProperty =
            DependencyProperty.Register("MouseOverColor", typeof(Brush), typeof(PathRpeatButton), new PropertyMetadata(null));
        
        public Brush MousePressColor
        {
            get { return (Brush)GetValue(MousePressColorProperty); }
            set { SetValue(MousePressColorProperty, value); }
        }
        
        public static readonly DependencyProperty MousePressColorProperty =
            DependencyProperty.Register("MousePressColor", typeof(Brush), typeof(PathRpeatButton), new PropertyMetadata(null));
        
        public double PathBgOpacity
        {
            get { return (double)GetValue(PathBgOpacityProperty); }
            set { SetValue(PathBgOpacityProperty, value); }
        }
        
        public static readonly DependencyProperty PathBgOpacityProperty =
            DependencyProperty.Register("PathBgOpacity", typeof(double), typeof(PathRpeatButton), new PropertyMetadata(1.0d));



        public Stretch PathStretch
        {
            get { return (Stretch)GetValue(PathStretchProperty); }
            set { SetValue(PathStretchProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PathStretch.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PathStretchProperty =
            DependencyProperty.Register("PathStretch", typeof(Stretch), typeof(PathRpeatButton), new PropertyMetadata(Stretch.Uniform));
        
        public Thickness PathMargin
        {
            get { return (Thickness)GetValue(PathMarginProperty); }
            set { SetValue(PathMarginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PathMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PathMarginProperty =
            DependencyProperty.Register("PathMargin", typeof(Thickness), typeof(PathRpeatButton), new PropertyMetadata(new Thickness(3.0d)));
    }
}
