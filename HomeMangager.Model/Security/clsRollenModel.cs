using HomeManager.Common;
using System.ComponentModel;

namespace HomeManager.Model.Security
{
    public class clsRollenModel : clsCommonModelPropertiesBase, IDataErrorInfo
    {
        #region Constructor

        public clsRollenModel() { }

        #endregion

        #region Properties

        private int _rolID;
        public int RolID
        {
            get => _rolID;
            set
            {
                _rolID = value;
                OnPropertyChanged();
            }
        }

        private string _rolName;
        public string RolName
        {
            get => _rolName;
            set
            {
                if (_rolName != value && _rolName != null)
                {
                    IsDirty = true;
                }
                _rolName = value;
                OnPropertyChanged();
            }
        }

        private string _rechten;
        public string Rechten
        {
            get => _rechten;
            set
            {
                if (_rechten != value && _rechten != null)
                {
                    IsDirty = true;
                }
                _rechten = value;
                OnPropertyChanged();
            }
        }

        private bool _isTextBoxEnabled;
        public bool IsTextBoxEnabled
        {
            get => _isTextBoxEnabled;
            set
            {
                _isTextBoxEnabled = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Overrides

        public override string ToString()
        {
            return string.IsNullOrWhiteSpace(RolName) ? base.ToString() : RolName;
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
                    case nameof(RolName):
                        if (string.IsNullOrWhiteSpace(RolName))
                        {
                            error = "RolName is verplicht veld.";
                            if (!ErrorList.Contains(nameof(RolName)))
                                ErrorList.Add(nameof(RolName));
                        }
                        else if (RolName.Length > 50)
                        {
                            error = "RolName mag niet langer zijn dan 50 karakters.";
                            if (!ErrorList.Contains(nameof(RolName)))
                                ErrorList.Add(nameof(RolName));
                        }
                        else
                        {
                            ErrorList.Remove(nameof(RolName));
                        }
                        break;
                }

                return error;
            }
        }

        #endregion
    }
}
