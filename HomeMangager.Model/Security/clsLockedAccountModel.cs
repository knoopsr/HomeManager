using HomeManager.Common;
using HomeManager.Model.Personen;
using System.Collections.ObjectModel;

namespace HomeManager.Model.Security
{
    /// <summary>
    /// Model voor geblokkeerde accounts, met gekoppelde persoon en selectieinformatie.
    /// </summary>
    public class clsLockedAccountModel : clsCommonModelPropertiesBase
    {
        #region Properties - Relaties

        private clsPersoonModel _persoon;
        /// <summary>
        /// De persoon gekoppeld aan het account.
        /// </summary>
        public clsPersoonModel Persoon
        {
            get => _persoon;
            set
            {
                _persoon = value;
                OnPropertyChanged();
            }
        }

        private clsAccountModel _account;
        /// <summary>
        /// Het account dat geblokkeerd is.
        /// </summary>
        public clsAccountModel Account
        {
            get => _account;
            set
            {
                _account = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Properties - Selectie

        private bool _isSelected;
        /// <summary>
        /// Geeft aan of dit account geselecteerd is voor ontgrendeling.
        /// </summary>
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (_isSelected != value)
                    IsDirty = true;

                _isSelected = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Properties - Selectie Lijst

        private ObservableCollection<(int AccountID, string Wachtwoord)> _selectedItemsList;
        /// <summary>
        /// Bevat een lijst van geselecteerde accounts met hun nieuwe wachtwoorden.
        /// </summary>
        public ObservableCollection<(int AccountID, string Wachtwoord)> SelectedItemsList
        {
            get => _selectedItemsList ??= new ObservableCollection<(int, string)>();
            set
            {
                _selectedItemsList = value;
                OnPropertyChanged();
            }
        }

        private string _selectedItems;
        /// <summary>
        /// Optionele stringrepresentatie van de geselecteerde items.
        /// </summary>
        public string SelectedItems
        {
            get => _selectedItems;
            set
            {
                _selectedItems = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Methoden

        /// <summary>
        /// Voeg een item toe aan de lijst van te ontgrendelen accounts.
        /// </summary>
        public void AddSelectedItem(int accountId, string wachtwoord)
        {
            SelectedItemsList.Add((accountId, wachtwoord));
        }

        /// <summary>
        /// Wis de lijst van geselecteerde items.
        /// </summary>
        public void ClearSelectedItems()
        {
            SelectedItemsList.Clear();
        }

        #endregion
    }
}
