using HomeManager.DataService.Exceptions;
using HomeManager.Model.Exceptions;
using HomeManager.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HomeManager.Behaviors
{

    public class RelayCommando<T> : ICommand
    {
        clsExceptionsDataService ExceptionsDataService;

        private readonly Action<T> _execute;
        private readonly Predicate<T> _canExecute;

        public RelayCommando(Action<T> execute, Predicate<T> canExecute = null)
        {
            ExceptionsDataService = new clsExceptionsDataService();

            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute((T)parameter);
        }

        public void Execute(object parameter)
        {
            try
            {
                _execute((T)parameter);
            }
            catch(Exception ex)
            {
                ExceptionsDataService.Insert(new clsExceptionsModel()
                {
                    AccountID = clsLoginModel.Instance.AccountID,
                    ExceptionName = ex.GetType().FullName ?? "Unknown GetType().FullName",
                    Module = ex.GetType().Module.Name ?? "Unknown Module.Name",
                    Source = ex.Source ?? "Unknown Source",
                    TargetSite = ex.TargetSite?.Name ?? "Unknown TargetSite",
                    ExceptionMessage = ex.Message ?? "Unknown Message",
                    InnerExceptionMessage = ex.InnerException?.Message ?? "Unknown InnerException.Message",
                    StackTrace = ex.StackTrace ?? "Unknown StackTrace",
                    DotNetAssembly = ex.GetType().Assembly.FullName ?? "Unknown Assembly"
                });
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}

