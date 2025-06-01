using HomeManager.Common;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace HomeManager.Model.Security
{
    public class clsRechtenCatogorieModel : clsCommonModelPropertiesBase, IDataErrorInfo
    {
        #region Constructor

        public clsRechtenCatogorieModel()
        {
            Rechten = new ObservableCollection<clsRechtenModel>();
        }

        #endregion

        #region Properties

        private int _rechtenCatogorieID;
        public int RechtenCatogorieID
        {
            get => _rechtenCatogorieID;
            set
            {
                _rechtenCatogorieID = value;
                OnPropertyChanged();
            }
        }

        private string _catogorieNaam;
        public string CatogorieNaam
        {
            get => _catogorieNaam;
            set
            {
                _catogorieNaam = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<clsRechtenModel> _rechten;
        public ObservableCollection<clsRechtenModel> Rechten
        {
            get => _rechten;
            set
            {
                _rechten = value;
                OnPropertyChanged();
            }
        }

        private bool? _isChecked;
        public bool? IsChecked
        {
            get => _isChecked;
            set
            {
                _isChecked = value;
                OnPropertyChanged();

                if (_isChecked.HasValue)
                {
                    foreach (var item in Rechten)
                        item.IsChecked = _isChecked.Value;
                }
            }
        }

        #endregion

        #region IDataErrorInfo

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                string error = null;

                switch (columnName)
                {
                    case nameof(CatogorieNaam):
                        if (string.IsNullOrWhiteSpace(CatogorieNaam))
                        {
                            error = "Naam is verplicht.";
                            if (!ErrorList.Contains(nameof(CatogorieNaam)))
                                ErrorList.Add(nameof(CatogorieNaam));
                        }
                        else
                        {
                            ErrorList.Remove(nameof(CatogorieNaam));
                        }
                        break;
                }

                return error;
            }
        }

        #endregion
    }
}
