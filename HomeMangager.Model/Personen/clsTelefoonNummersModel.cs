using HomeManager.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Model.Personen
{
    public class clsTelefoonNummersModel : clsCommonModelPropertiesBase, IDataErrorInfo
    {
        private int _telefoonNummerID;
        public int TelefoonNummerID
        {
            get
            {
                return _telefoonNummerID;
            }
            set
            {
                _telefoonNummerID = value;
                OnPropertyChanged();
            }
        }

        private int _persoonID;
        public int PersoonID
        {
            get
            {
                return _persoonID;
            }
            set
            {
                _persoonID = value;
                OnPropertyChanged();
            }
        }

        private int _telefoonTypeID;
        public int TelefoonTypeID
        {
            get
            {
                return _telefoonTypeID;
            }
            set
            {
                _telefoonTypeID = value;
                OnPropertyChanged();
            }
        }

        private string _telefoonNummer;
        public string TelefoonNummer
        {
            get
            {
                return _telefoonNummer;
            }
            set
            {
                if (_telefoonNummer != value)
                {
                    if (_telefoonNummer != null)
                    {
                        IsDirty = true;
                    }
                }
                _telefoonNummer = value;
                OnPropertyChanged();
            }
        }

        public override string ToString()
        {
            return TelefoonNummer;
        }

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case "TelefoonNummer":
                        if (string.IsNullOrWhiteSpace(_telefoonNummer))
                        {
                            error = "Telefoonnummer is een verplicht veld.";
                            if (ErrorList.Contains("TelefoonNummer") == false)
                            {
                                ErrorList.Add("TelefoonNummer");
                            }
                        }
                        else if (_telefoonNummer.Length > 50)
                        {
                            error = "Your telefoonnummer is to long!!!";
                            if (ErrorList.Contains("TelefoonNummer") == false)
                            {
                                ErrorList.Add("TelefoonNummer");
                            }
                        }
                        else
                        {
                            if (ErrorList.Contains("TelefoonNummer"))
                            {
                                ErrorList.Remove("TelefoonNummer");
                            }
                        }
                        return error;
                    default:
                        error = null;
                        return error;
                }
            }
        }
    }
}
