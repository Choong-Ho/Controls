using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Controls
{
    public class CheckedListBoxItem : ListBoxItem
    {
        protected override void OnSelected(RoutedEventArgs e)
        {
            if (Visibility != Visibility.Visible)
                e.Handled = true;
            else
                base.OnSelected(e);
        }
    }
}
