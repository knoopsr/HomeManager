using HomeManager.DataService.Exceptions;
using HomeManager.DataService.Logging;
using HomeManager.Model.Exceptions;
using HomeManager.Model.Logging;
using HomeManager.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HomeManager.Helpers
{
    public class clsCustomCommand : ICommand
    {

        clsButtonLoggingDataService MijnLoggingService;
        clsExceptionsDataService ExceptionsDataService;

        private Action<object?> _execute;
        private Predicate<object?> _canExecute;

        public clsCustomCommand(Action<object?> execute, Predicate<object?> canExecute)
        {
            this._execute = execute;
            this._canExecute = canExecute;

            MijnLoggingService = new clsButtonLoggingDataService();
            ExceptionsDataService = new clsExceptionsDataService();
        }

        public bool CanExecute(object? parameter)
        {
            bool b = _canExecute == null ? true : _canExecute(parameter);
            return b;
        }

        public void Execute(object? parameter)
        {
            try
            {
                _execute(parameter);

                //Hier word geen logging gedaan.
                //Er is geen CommandParameter meegestuurd.
                if (parameter == null)
                {
                    MessageBox.Show("Geen Logging. Er word geen gebruik gemaakt van CommandParameter in de Xaml.");
                    return;
                }

                if (_execute is Delegate del)
                {
                    string targetName = del.Target?.GetType().Name ?? "null";
                    string methodName = del.Method.Name;


                    MijnLoggingService.Insert(new clsButtonLoggingModel()
                    {
                        AccountId = clsLoginModel.Instance.AccountID,
                        ActionName = methodName,
                        ActionTarget = targetName
                    });
                }
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

        public event EventHandler? CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }
    }
}
