using HomeManager.Common;
using System.ComponentModel;

namespace HomeManager.Model.Security
{
    public class clsRechtenModel : clsCommonModelPropertiesBase, IDataErrorInfo
    {
        #region Constructor

        public clsRechtenModel() { }

        #endregion

        #region Properties

        private int _rechtenID;
        public int RechtenID
        {
            get => _rechtenID;
            set
            {
                _rechtenID = value;
                OnPropertyChanged();
            }
        }

        private string _rechtenName;
        public string RechtenName
        {
            get => _rechtenName;
            set
            {
                _rechtenName = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(DisplayName));
            }
        }

        private int _rechtenCode;
        public int RechtenCode
        {
            get => _rechtenCode;
            set
            {
                _rechtenCode = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(DisplayName));
            }
        }

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

        private bool _isChecked;
        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                _isChecked = value;
                OnPropertyChanged();
            }
        }

        // Read-only property to use in UI
        public string DisplayName => $"{RechtenName} ({RechtenCode})";

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
                    case nameof(RechtenName):
                        if (string.IsNullOrWhiteSpace(RechtenName))
                        {
                            error = "Naam is verplicht.";
                            if (!ErrorList.Contains(nameof(RechtenName)))
                                ErrorList.Add(nameof(RechtenName));
                        }
                        else
                        {
                            ErrorList.Remove(nameof(RechtenName));
                        }
                        break;

                    case nameof(RechtenCode):
                        if (RechtenCode <= 0)
                        {
                            error = "Code moet groter zijn dan 0.";
                            if (!ErrorList.Contains(nameof(RechtenCode)))
                                ErrorList.Add(nameof(RechtenCode));
                        }
                        else
                        {
                            ErrorList.Remove(nameof(RechtenCode));
                        }
                        break;
                }

                return error;
            }
        }

        #endregion
    }
}
