using System.Windows;

namespace MVVM
{
    public interface IMessageBoxWindow
    {
        MessageBoxResult ShowError(string messageBoxText, MessageBoxButton button = MessageBoxButton.OK, MessageBoxResult defaultResult = MessageBoxResult.None);
        MessageBoxResult ShowWarning(string messageBoxText, MessageBoxButton button = MessageBoxButton.OK, MessageBoxResult defaultResult = MessageBoxResult.None);
        MessageBoxResult ShowQuestion(string messageBoxText, MessageBoxButton button = MessageBoxButton.OKCancel, MessageBoxResult defaultResult = MessageBoxResult.None);
        MessageBoxResult ShowInformation(string messageBoxText, MessageBoxButton button = MessageBoxButton.OK, MessageBoxResult defaultResult = MessageBoxResult.None);
        MessageBoxResult Show(Window owner, string messageBoxText, string caption = "", MessageBoxButton button = MessageBoxButton.OK, MessageBoxImage icon = MessageBoxImage.None, MessageBoxResult defaultResult = MessageBoxResult.None);
    }
}
