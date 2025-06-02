using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HomeManager.Common
{
    /// <summary>
    /// Basisklasse voor observable objecten.
    /// Combineert <see cref="INotifyPropertyChanged"/> met <see cref="clsBindableBase"/> zodat afgeleide klassen automatisch UI-updates ondersteunen.
    /// </summary>
    public class clsObservable : clsBindableBase, INotifyPropertyChanged
    {
        #region Events

        /// <summary>
        /// Wordt afgevuurd wanneer een property verandert.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Protected Methods

        /// <summary>
        /// Roept het <see cref="PropertyChanged"/> event aan voor de opgegeven property.
        /// </summary>
        /// <param name="propertyName">De naam van de property (automatisch ingevuld via CallerMemberName).</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
