using HomeManager.Common;
using System.ComponentModel;

namespace HomeManager.Model.Security
{
    public class clsRollenModel : clsCommonModelPropertiesBase, IDataErrorInfo
    {
        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case nameof(RolName):
                        if (string.IsNullOrWhiteSpace(RolName))
                        {
                            error = "RolName is verplicht veld.";
                            if (ErrorList.Contains(nameof(RolName)) == false)
                            {
                                ErrorList.Add(nameof(RolName));
                            }
                        }
                        else if (RolName.Length > 50)
                        {
                            error = "RolName mag niet langer zijn dan 50 karakters.";
                            if (ErrorList.Contains(nameof(RolName)) == false)
                            {
                                ErrorList.Add(nameof(RolName));
                            }
                        }
                        else
                        {
                            if (ErrorList.Contains(nameof(RolName)))
                            {
                                ErrorList.Remove(nameof(RolName));
                            }
                        }
                        return error;

                    default:
                        error = null;
                        return error;
                }

            }

        }

        private int _rolID;
        public int RolID
        {
            get { return _rolID; }
            set
            {

                _rolID = value;
                OnPropertyChanged();

            }
        }

        private string _rolName;
        public string RolName
        {
            get { return _rolName; }
            set
            {
                if (_rolName != value)
                {
                    if (_rolName != null)
                    {
                        IsDirty = true;
                    }
                }
                _rolName = value;
                OnPropertyChanged();
            }
        }

        private string _rechten;
        public string Rechten
        {
            get { return _rechten; }
            set
            {
                if (_rechten != value)
                {
                    if (_rechten != null)
                    {
                        IsDirty = true;
                    }
                }
                _rechten = value;
                OnPropertyChanged();
            }
        }

        public override string ToString()
        {
            return RolName;
        }

        private bool _isTextBoxEnabled;
        public bool IsTextBoxEnabled
        {
            get { return _isTextBoxEnabled; }
            set
            {
                _isTextBoxEnabled = value;
                OnPropertyChanged();
            }
        }


    }
}
