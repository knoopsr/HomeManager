using HomeManager.DataService.Logging;
using HomeManager.Model.Logging;
using HomeManager.Model.Security;
using HomeManager.Services;
using System.Windows;
using System.Windows.Input;

namespace HomeManager.Helpers
{
    /// <summary>
    /// Een aangepaste ICommand-implementatie die logging uitvoert bij het uitvoeren van acties.
    /// Logt automatisch welke methode werd aangeroepen en door wie, op basis van de delegate.
    /// </summary>
    public class clsCustomCommand : ICommand
    {
        private readonly clsButtonLoggingDataService MijnLoggingService;

        private readonly Action<object?> _execute;
        private readonly Predicate<object?> _canExecute;

        /// <summary>
        /// Initialiseert een nieuwe instantie van <see cref="clsCustomCommand"/>.
        /// </summary>
        /// <param name="execute">De uit te voeren actie.</param>
        /// <param name="canExecute">De voorwaarde waaronder de actie mag worden uitgevoerd.</param>
        public clsCustomCommand(Action<object?> execute, Predicate<object?> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
            MijnLoggingService = new clsButtonLoggingDataService();
        }

        /// <summary>
        /// Bepaalt of het commando kan worden uitgevoerd met de opgegeven parameter.
        /// </summary>
        /// <param name="parameter">De parameter die van invloed is op de uitvoerbaarheid.</param>
        /// <returns><c>true</c> als het commando kan worden uitgevoerd; anders <c>false</c>.</returns>
        public bool CanExecute(object? parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        /// <summary>
        /// Voert de bijhorende actie uit en logt de actie als een commandparameter aanwezig is.
        /// </summary>
        /// <param name="parameter">De parameter die wordt doorgegeven aan de actie.</param>
        public void Execute(object? parameter)
        {
            try
            {
                _execute(parameter);

                if (parameter == null)
                {
                    MessageBox.Show("Geen Logging. Er wordt geen gebruik gemaakt van CommandParameter in de XAML.");
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
            catch (Exception ex)
            {
                clsExceptionService.InsertException(ex);
            }
        }

        /// <summary>
        /// Wordt aangeroepen wanneer de uitvoerbaarheid van het commando verandert.
        /// </summary>
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
