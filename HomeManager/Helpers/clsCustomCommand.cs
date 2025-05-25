using HomeManager.DataService.Exceptions;
using HomeManager.DataService.Logging;
using HomeManager.Model.Exceptions;
using HomeManager.Model.Logging;
using HomeManager.Model.Security;
using HomeManager.Services;
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

        private Action<object?> _execute;
        private Predicate<object?> _canExecute;
        private Action execute_SubmitEmail;
        private Func<bool> canExecute_SubmitEmail;

        public clsCustomCommand(Action<object?> execute, Predicate<object?> canExecute)
        {
            this._execute = execute;
            this._canExecute = canExecute;

            MijnLoggingService = new clsButtonLoggingDataService();
        }

        public clsCustomCommand(Action execute_SubmitEmail, Func<bool> canExecute_SubmitEmail)
        {
            this.execute_SubmitEmail = execute_SubmitEmail;
            this.canExecute_SubmitEmail = canExecute_SubmitEmail;
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
                clsExceptionService.InsertException(ex);
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
