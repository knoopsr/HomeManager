using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HomeManager.Common
{
    /// <summary>
    /// Basisklasse voor ViewModels die INotifyPropertyChanged implementeert voor databinding.
    /// </summary>
    public class clsBindableBase : INotifyPropertyChanged
    {
        #region PropertyChanged Event

        /// <summary>
        /// Wordt getriggerd wanneer een eigenschap verandert.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        #endregion

        #region Protected Methods

        /// <summary>
        /// Stelt de property in indien de waarde verschillend is, en roept PropertyChanged aan.
        /// </summary>
        /// <typeparam name="T">Type van de property.</typeparam>
        /// <param name="member">Referentie naar de backing field.</param>
        /// <param name="val">Nieuwe waarde.</param>
        /// <param name="propertyName">Optioneel: naam van de property (automatisch via CallerMemberName).</param>
        protected virtual void SetProperty<T>(ref T member, T val, [CallerMemberName] string propertyName = null)
        {
            if (Equals(member, val)) return;

            member = val;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Roept PropertyChanged handmatig aan.
        /// </summary>
        /// <param name="propertyName">De naam van de property.</param>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Alternatieve OnPropertyChanged met CallerMemberName.
        /// </summary>
        /// <param name="propertyName">Automatisch opgehaalde property naam.</param>
        protected virtual void OnPropertyChange([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
