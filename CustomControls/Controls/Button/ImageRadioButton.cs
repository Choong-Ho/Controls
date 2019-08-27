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
    public class ImageRadioButton : RadioButton
    {
        static ImageRadioButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ImageRadioButton), new FrameworkPropertyMetadata(typeof(ImageRadioButton)));
        }
        
        public ImageSource ContentImage
        {
            get { return (ImageSource)GetValue(ContentImageProperty); }
            set { SetValue(ContentImageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ContentImage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentImageProperty =
            DependencyProperty.Register("ContentImage", typeof(ImageSource), typeof(ImageRadioButton), new PropertyMetadata(default(ImageSource)));
        
        public Brush MouseOverColor
        {
            get { return (Brush)GetValue(MouseOverColorProperty); }
            set { SetValue(MouseOverColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MouseOverColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseOverColorProperty =
            DependencyProperty.Register("MouseOverColor", typeof(Brush), typeof(ImageRadioButton), new PropertyMetadata(default(Brush)));
    }
}
