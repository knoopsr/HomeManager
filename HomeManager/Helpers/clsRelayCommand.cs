using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

/* ik maak deze commandclass omdat ik niet bij elke button van Dagboek een CanExecute wil maken
 * deze zijn altijd true
 */

namespace HomeManager.Helpers
{
    public class clsRelayCommand : ICommand
    {
        private readonly Action<object?> _execute;
        private readonly Predicate<object?>? _canExecute;

        // Constructor that takes the execute and canExecute delegates
        public clsRelayCommand(Action<object?> execute, Predicate<object?>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        // CanExecute method checks if the command can be executed
        public bool CanExecute(object? parameter)
        {
            return _canExecute?.Invoke(parameter) ?? true; // Default to true if no canExecute is provided
        }

        // Execute method runs the provided action
        public void Execute(object? parameter)
        {
            _execute(parameter);
        }

        // Event to notify when CanExecute state changes
        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
