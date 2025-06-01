using System.Windows.Input;

namespace HomeManager.Common
{
    /// <summary>
    /// Abstracte basisimplementatie van ICommand voor herbruikbare commandologiek in MVVM.
    /// </summary>
    public abstract class clsCommonCommandBase : ICommand
    {
        #region ICommand Members

        /// <summary>
        /// Event dat afgevuurd wordt wanneer de uitvoerbaarheid van het command verandert.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Bepaalt of het command kan worden uitgevoerd.
        /// Standaard altijd true, overschrijf indien nodig.
        /// </summary>
        /// <param name="parameter">Het parameter object.</param>
        /// <returns>True indien uitvoerbaar; anders false.</returns>
        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Voert de logica van het command uit. Moet geïmplementeerd worden door afgeleide klassen.
        /// </summary>
        /// <param name="parameter">Het parameter object.</param>
        public abstract void Execute(object parameter);

        #endregion

        #region Helper Methods

        /// <summary>
        /// Roept het CanExecuteChanged-event aan om WPF te laten weten dat CanExecute opnieuw geëvalueerd moet worden.
        /// </summary>
        protected void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }
}
