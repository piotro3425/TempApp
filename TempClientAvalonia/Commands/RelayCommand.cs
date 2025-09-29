using System;
using System.Windows.Input;

namespace TempClientAvalonia.Commands
{
    internal class RelayCommand(Action<object?> executeMethod, Func<object?, bool>? canExecuteMethod = null) : ICommand
    {
        Action<object?> executeMethod = executeMethod;
        Func<object?, bool>? canExecuteMethod = canExecuteMethod;

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            if (canExecuteMethod != null)
                return canExecuteMethod(parameter);
            else
                return true;
        }

        public void Execute(object? parameter)
        {
            if (executeMethod != null)
                executeMethod(parameter);
        }
    }
}
