using System.Windows.Input;

namespace HomeManager.Common
{
    /// <summary>
    /// Een generieke implementatie van ICommand voor MVVM-doeleinden.
    /// Hiermee kun je eenvoudig commandlogica koppelen aan ViewModel-acties.
    /// </summary>
    /// <typeparam name="T">Het type parameter dat door het command wordt gebruikt.</typeparam>
    public class clsRelayCommand<T> : ICommand
    {
        #region Fields

        private readonly Action<T> _TargetExecuteMethod;
        private readonly Func<T, bool> _TargetCanExecuteMethod;

        #endregion

        #region Constructors

        /// <summary>
        /// Maakt een nieuwe RelayCommand aan met alleen een execute-actie.
        /// </summary>
        /// <param name="executeMethod">De uit te voeren actie.</param>
        public clsRelayCommand(Action<T> executeMethod)
        {
            _TargetExecuteMethod = executeMethod ?? throw new ArgumentNullException(nameof(executeMethod));
        }

        /// <summary>
        /// Maakt een nieuwe RelayCommand aan met execute- en can-execute-logica.
        /// </summary>
        /// <param name="executeMethod">De uit te voeren actie.</param>
        /// <param name="canExecuteMethod">Een functie die bepaalt of de actie mag worden uitgevoerd.</param>
        public clsRelayCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
        {
            _TargetExecuteMethod = executeMethod ?? throw new ArgumentNullException(nameof(executeMethod));
            _TargetCanExecuteMethod = canExecuteMethod;
        }

        #endregion

        #region ICommand Implementation

        /// <summary>
        /// Wordt getriggerd wanneer de uitvoerbaarheid van het commando verandert.
        /// </summary>
        public event EventHandler? CanExecuteChanged = delegate { };

        /// <summary>
        /// Forceert een her-evaluatie van CanExecute.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Bepaalt of het commando mag worden uitgevoerd.
        /// </summary>
        /// <param name="parameter">De parameter voor de CanExecute-evaluatie.</param>
        /// <returns>True indien toegestaan, anders false.</returns>
        bool ICommand.CanExecute(object? parameter)
        {
            if (_TargetCanExecuteMethod != null && parameter is T tparm)
                return _TargetCanExecuteMethod(tparm);

            return _TargetExecuteMethod != null;
        }

        /// <summary>
        /// Voert het commando uit.
        /// </summary>
        /// <param name="parameter">De parameter die meegegeven wordt bij de uitvoering.</param>
        void ICommand.Execute(object? parameter)
        {
            if (parameter is T tparm)
            {
                _TargetExecuteMethod?.Invoke(tparm);
            }
        }

        #endregion
    }
}
