using System.Windows;
using System.Windows.Input;

namespace MVVM
{
    public interface IDialogWindow
    {
        MessageBoxResult ShowDialog(string title, MessageBoxButton msgButton, DialogBaseViewModel vm, 
            ResizeMode mode = ResizeMode.NoResize);

        MessageBoxResult ShowDialog(Window owner, string title, MessageBoxButton msgButton, DialogBaseViewModel vm, 
            ResizeMode mode = ResizeMode.NoResize);

        void CloseDialog();
    }
}
