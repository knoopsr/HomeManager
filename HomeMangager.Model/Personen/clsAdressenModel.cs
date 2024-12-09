using HomeManager.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Model.Personen
{
    public class clsAdressenModel : clsCommonModelPropertiesBase, IDataErrorInfo
    {
        private int _adresID;
        public int AdresID
        {
            get
            {
                return _adresID;
            }
            set
            {
                _adresID = value;
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

        private int _gemeenteID;
        public int GemeenteID
        {
            get
            {
                return _gemeenteID;
            }
            set
            {
                _gemeenteID = value;
                OnPropertyChanged();
            }
        }

        private int _functieID;
        public int FunctieID
        {
            get
            {
                return _functieID;
            }
            set
            {
                _functieID = value;
                OnPropertyChanged();
            }
        }

        private string _straat;

        public string Straat
        {
            get
            {
                return _straat;
            }
            set
            {
                if (_straat != value)
                {
                    if (_straat != null)
                    {
                        IsDirty = true;
                    }
                }
                _straat = value;
                OnPropertyChanged();
            }
        }
        
        private string _nummer;

        public string Nummer
        {
            get
            {
                return _nummer;
            }
            set
            {
                if (_nummer != value)
                {
                    if (_nummer != null)
                    {
                        IsDirty = true;
                    }
                }
                _nummer = value;
                OnPropertyChanged();
            }
        }

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case "Straat":
                        if (string.IsNullOrWhiteSpace(_straat))
                        {
                            error = "Straat is een verplicht veld.";
                            if (ErrorList.Contains("Straat") == false)
                            {
                                ErrorList.Add("Straat");
                            }
                        }
                        else if (_straat.Length > 50)
                        {
                            error = "Your text is to long!!!";
                            if (ErrorList.Contains("Straat") == false)
                            {
                                ErrorList.Add("Straat");
                            }
                        }
                        else
                        {
                            if (ErrorList.Contains("Straat"))
                            {
                                ErrorList.Remove("Straat");
                            }
                        }
                        return error;

                    case "Nummer":
                        if (string.IsNullOrWhiteSpace(_nummer))
                        {
                            error = "Nummer is een verplicht veld.";
                            if (ErrorList.Contains("Nummer") == false)
                            {
                                ErrorList.Add("Nummer");
                            }
                        }
                        else if (_nummer.Length > 5)
                        {
                            error = "Your text is to long!!!";
                            if (ErrorList.Contains("Nummer") == false)
                            {
                                ErrorList.Add("Straat");
                            }
                        }
                        else
                        {
                            if (ErrorList.Contains("Nummer"))
                            {
                                ErrorList.Remove("Straat");
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
