using HomeManager.Common;
using HomeManager.Model.Personen;
using System.Collections.ObjectModel;

namespace HomeManager.Model.Security
{
    public class clsLockedAccountModel : clsCommonModelPropertiesBase
    {


        private clsPersoonModel _persoon;
        public clsPersoonModel Persoon
        {
            get
            {
                return _persoon;
            }
            set
            {
                _persoon = value;
                OnPropertyChanged();
            }
        }


        private clsAccountModel _account;
        public clsAccountModel Account
        {
            get
            {
                return _account;
            }
            set
            {
                _account = value;
                OnPropertyChanged();
            }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                if (_isSelected != value)
                {         
                        IsDirty = true;            
                }
                _isSelected = value;
                OnPropertyChanged();
            }
        }

        // Nieuwe collectie om AccountID en Wachtwoord-paren op te slaan
        private ObservableCollection<(int AccountID, string Wachtwoord)> _selectedItemsList;
        public ObservableCollection<(int AccountID, string Wachtwoord)> SelectedItemsList
        {
            get => _selectedItemsList ??= new ObservableCollection<(int AccountID, string Wachtwoord)>();
            set
            {
                _selectedItemsList = value;
                OnPropertyChanged();
            }
        }

        // Optioneel: Houdt een stringweergave bij van geselecteerde items
        private string _selectedItems;
        public string SelectedItems
        {
            get => _selectedItems;
            set
            {
                _selectedItems = value;
                OnPropertyChanged();
            }
        }

        // Hulpmethode om een item aan de lijst toe te voegen
        public void AddSelectedItem(int accountId, string wachtwoord)
        {
            SelectedItemsList.Add((accountId, wachtwoord));
        }

        // Hulpmethode om de lijst te resetten
        public void ClearSelectedItems()
        {
            SelectedItemsList.Clear();
        }

    }
}
