using HomeManager.Common;
using HomeManager.Model.Personen;

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

        private string _selectedItems;
        public string SelectedItems
        {
            get
            {
                return _selectedItems;
            }
            set
            {
                _selectedItems = value;
                OnPropertyChanged();
            }
        }

    }
}
