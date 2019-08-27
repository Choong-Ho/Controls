using System;
using System.Collections;
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
    public class RemoveListBox : ListBox
    {
        static RemoveListBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RemoveListBox), new FrameworkPropertyMetadata(typeof(RemoveListBox)));
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            var item = new RemoveItem();
            item.RemoveClick += (sender, e) =>
            {
                if (this.ItemsSource != null && sender is RemoveItem chooseItem)
                {
                    if (MessageBox.Show($"'{chooseItem.Content}'를 지우시겠습니까?", "Delete", MessageBoxButton.OKCancel, MessageBoxImage.Question) != MessageBoxResult.OK)
                        return;

                    var vm = chooseItem.DataContext;
                    (this.ItemsSource as IList).Remove(vm);
                    (vm as IDisposable)?.Dispose();
                }
                else
                    this.Items.Remove(sender);
            };

            return item;
        }

        protected override bool IsItemItsOwnContainerOverride(object item) => item is RemoveItem;
    }
}
