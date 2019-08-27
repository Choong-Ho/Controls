using System.Windows;
using System.Windows.Controls;

namespace Controls
{
    public class StartEndHorizontalScrollViewer : ScrollViewer
    {
        static StartEndHorizontalScrollViewer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StartEndHorizontalScrollViewer),
                new FrameworkPropertyMetadata(typeof(StartEndHorizontalScrollViewer)));
        }
    }
}
