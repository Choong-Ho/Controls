using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Controls
{
    internal class ControlCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Predicate<T> _canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public ControlCommand(Action<T> action) : this(action, null) { }

        public ControlCommand(Action<T> action, Predicate<T> predicate)
        {
            if (action == null) throw new ArgumentException("action, null value");

            _execute = action;
            _canExecute = predicate;
        }

        public bool CanExecute(object parameter)
            => _canExecute == null ? true : _canExecute((T)parameter);

        public void Execute(object parameter)
            => _execute((T)parameter);
    }
}
