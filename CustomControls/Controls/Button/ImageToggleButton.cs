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
    public class ImageToggleButton : ToggleButton
    {
        static ImageToggleButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ImageToggleButton), new FrameworkPropertyMetadata(typeof(ImageToggleButton)));
        }
        
        public ImageSource OnImage
        {
            get { return (ImageSource)GetValue(OnImageProperty); }
            set { SetValue(OnImageProperty, value); }
        }

        public static readonly DependencyProperty OnImageProperty =
            DependencyProperty.Register("OnImage", typeof(ImageSource), typeof(ImageToggleButton), new PropertyMetadata(default(ImageSource)));

        public ImageSource OffImage
        {
            get { return (ImageSource)GetValue(OffImageProperty); }
            set { SetValue(OffImageProperty, value); }
        }

        public static readonly DependencyProperty OffImageProperty =
            DependencyProperty.Register("OffImage", typeof(ImageSource), typeof(ImageToggleButton), new PropertyMetadata(default(ImageSource)));
        
        public string OnToolTip
        {
            get { return (string)GetValue(OnToolTipProperty); }
            set { SetValue(OnToolTipProperty, value); }
        }
        
        public static readonly DependencyProperty OnToolTipProperty =
            DependencyProperty.Register("OnToolTip", typeof(string), typeof(ImageToggleButton), new PropertyMetadata("Close"));
        
        public string OffToolTip
        {
            get { return (string)GetValue(OffToolTipProperty); }
            set { SetValue(OffToolTipProperty, value); }
        }
        
        public static readonly DependencyProperty OffToolTipProperty =
            DependencyProperty.Register("OffToolTip", typeof(string), typeof(ImageToggleButton), new PropertyMetadata("Open"));
    }
}
