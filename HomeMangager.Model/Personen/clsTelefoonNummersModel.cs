using HomeManager.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        private string _telefoonType;

        public string TelefoonType
        {
            get
            {
                return _telefoonType;
            }
            set
            {
                if (_telefoonType != value)
                {
                    if (_telefoonType != null)
                    {
                        IsDirty = true;
                    }
                }
                _telefoonType = value;
                OnPropertyChanged();
            }
        }

        public override string ToString()
        {
            return TelefoonNummer + " (" + TelefoonType + ")" ;
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
                            if (!ErrorList.Contains("TelefoonNummer"))
                            {
                                ErrorList.Add("TelefoonNummer");
                            }
                        }
                        else if (_telefoonNummer.Length > 50)
                        {
                            error = "Telefoonnummer is te lang.";
                            if (!ErrorList.Contains("TelefoonNummer"))
                            {
                                ErrorList.Add("TelefoonNummer");
                            }
                        }
                        else if (!Regex.IsMatch(_telefoonNummer, @"^\+?[0-9\s\-()]{7,15}$"))
                        {
                            // Regel = begint met 0 of '+', gevolgd door 7-15 cijfers of andere toegestane tekens
                            error = "Telefoonnummer is niet geldig.";
                            if (!ErrorList.Contains("TelefoonNummer"))
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
                    case nameof(TelefoonTypeID):
                        if (TelefoonTypeID == 0)
                        {
                            error = "TelefoonType is een verplicht veld.";
                            if (ErrorList.Contains(nameof(TelefoonTypeID)) == false)
                            {
                                ErrorList.Add(nameof(TelefoonTypeID));
                            }
                        }

                        else
                        {
                            if (ErrorList.Contains(nameof(TelefoonTypeID)))
                            {
                                ErrorList.Remove(nameof(TelefoonTypeID));
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
