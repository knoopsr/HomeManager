using HomeManager.Services;
using System.Windows.Input;

namespace HomeManager.Behaviors
{
    /// <summary>
    /// Een generieke ICommand-implementatie voor het binden van commando's in MVVM.
    /// </summary>
    /// <typeparam name="T">Het type parameter dat doorgegeven wordt aan het commando.</typeparam>
    public class RelayCommando<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Predicate<T> _canExecute;

        /// <summary>
        /// Initialiseert een nieuwe instantie van <see cref="RelayCommando{T}"/>.
        /// </summary>
        /// <param name="execute">De actie die uitgevoerd wordt wanneer het commando wordt geactiveerd.</param>
        /// <param name="canExecute">De voorwaarde waarmee bepaald wordt of het commando kan worden uitgevoerd.</param>
        /// <exception cref="ArgumentNullException">Wordt gegooid als <paramref name="execute"/> null is.</exception>
        public RelayCommando(Action<T> execute, Predicate<T> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        /// <summary>
        /// Bepaalt of het commando kan worden uitgevoerd met de opgegeven parameter.
        /// </summary>
        /// <param name="parameter">De parameter waarmee de CanExecute-voorwaarde wordt geëvalueerd.</param>
        /// <returns><c>true</c> als het commando kan worden uitgevoerd; anders <c>false</c>.</returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute((T)parameter);
        }

        /// <summary>
        /// Voert het commando uit met de opgegeven parameter.
        /// Fouten worden automatisch gelogd via <see cref="clsExceptionService"/>.
        /// </summary>
        /// <param name="parameter">De parameter die aan het commando wordt doorgegeven.</param>
        public void Execute(object parameter)
        {
            try
            {
                _execute((T)parameter);
            }
            catch (Exception ex)
            {
                clsExceptionService.InsertException(ex);
            }
        }

        /// <summary>
        /// Wordt opgeroepen wanneer de beschikbaarheid van het commando verandert.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
