using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Controls 
{
    public sealed class CheckedListBox : ListBox
    {
        public bool IsAll
        {
            get { return (bool)GetValue(IsAllProperty); }
            set { SetValue(IsAllProperty, value); }
        }
        
        public static readonly DependencyProperty IsAllProperty =
            DependencyProperty.Register("IsAll", typeof(bool), typeof(CheckedListBox), new PropertyMetadata(false));

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            IsAll = SelectionMode != SelectionMode.Single && Items.Count == 0 ? false : SelectedItems.Count == Items.Count;

            base.OnSelectionChanged(e);
        }

        protected override DependencyObject GetContainerForItemOverride()
            => new CheckedListBoxItem();

        protected override bool IsItemItsOwnContainerOverride(object item)
            => item is CheckedListBoxItem;
    }
}
