using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Common;

namespace HomeManager.Model.Security
{
    public class clsWachtWoordGroepModel : clsCommonModelPropertiesBase, IDataErrorInfo
    {
        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case nameof(WachtwoordGroep):
                        if (string.IsNullOrWhiteSpace(WachtwoordGroep))
                        {
                            error = "WachtwoordGroep is verplicht veld.";
                            if (ErrorList.Contains(nameof(WachtwoordGroep)) == false)
                            {
                                ErrorList.Add(nameof(WachtwoordGroep));
                            }
                        }
                        else if (WachtwoordGroep.Length > 50)
                        {
                            error = "WachtwoordGroep mag niet langer zijn dan 50 karakters.";
                            if (ErrorList.Contains(nameof(WachtwoordGroep)) == false)
                            {
                                ErrorList.Add(nameof(WachtwoordGroep));
                            }
                        }
                        else
                        {
                            if (ErrorList.Contains(nameof(WachtwoordGroep)))
                            {
                                ErrorList.Remove(nameof(WachtwoordGroep));
                            }
                        }
                        return error;

                    default:
                        error = null;
                        return error;
                }
            }
        }


        private int _wachtwoordGroepID;
        public int WachtwoordGroepID
        {
            get { return _wachtwoordGroepID; }
            set
            {
                _wachtwoordGroepID = value;
                OnPropertyChanged();
            }
        }

        private string _wachtwoordGroep;
        public string WachtwoordGroep
        {
            get { return _wachtwoordGroep; }
            set
            {
                if (_wachtwoordGroep != value)
                {
                    if (_wachtwoordGroep != null)
                    {
                        IsDirty = true;
                    }
                }
                _wachtwoordGroep = value;
                OnPropertyChanged();
            }
        }
        public override string ToString()
        {
            return WachtwoordGroep.ToString();
        }

    }
}
