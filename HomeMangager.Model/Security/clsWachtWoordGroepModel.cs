using HomeManager.Common;
using System.ComponentModel;

namespace HomeManager.Model.Security
{
    public class clsWachtWoordGroepModel : clsCommonModelPropertiesBase, IDataErrorInfo
    {
        #region Properties

        private int _wachtwoordGroepID;
        public int WachtwoordGroepID
        {
            get => _wachtwoordGroepID;
            set
            {
                _wachtwoordGroepID = value;
                OnPropertyChanged();
            }
        }

        private string _wachtwoordGroep;
        public string WachtwoordGroep
        {
            get => _wachtwoordGroep;
            set
            {
                if (_wachtwoordGroep != value && _wachtwoordGroep != null)
                {
                    IsDirty = true;
                }
                _wachtwoordGroep = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Overrides

        public override string ToString()
        {
            return WachtwoordGroep;
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
                    case nameof(WachtwoordGroep):
                        if (string.IsNullOrWhiteSpace(WachtwoordGroep))
                        {
                            error = "WachtwoordGroep is verplicht veld.";
                            if (!ErrorList.Contains(nameof(WachtwoordGroep)))
                                ErrorList.Add(nameof(WachtwoordGroep));
                        }
                        else if (WachtwoordGroep.Length > 50)
                        {
                            error = "WachtwoordGroep mag niet langer zijn dan 50 karakters.";
                            if (!ErrorList.Contains(nameof(WachtwoordGroep)))
                                ErrorList.Add(nameof(WachtwoordGroep));
                        }
                        else
                        {
                            ErrorList.Remove(nameof(WachtwoordGroep));
                        }
                        break;
                }

                return error;
            }
        }

        #endregion
    }
}
