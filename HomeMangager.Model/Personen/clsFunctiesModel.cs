using HomeManager.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Model.Personen
{
    public class clsFunctiesModel : clsCommonModelPropertiesBase, IDataErrorInfo
    {
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

        private string _functie;

        public string Functie
        {
            get
            {
                return _functie;
            }
            set
            {
                if (_functie != value)
                {
                    if (_functie != null)
                    {
                        IsDirty = true;
                    }
                }
                _functie = value;
                OnPropertyChanged();
            }
        }

        private string _omschrijving;

        public string Omschrijving
        {
            get
            {
                return _omschrijving;
            }
            set
            {
                if (_omschrijving != value)
                {
                    if (_omschrijving != null)
                    {
                        IsDirty = true;
                    }
                }
                _omschrijving = value;
                OnPropertyChanged();
            }
        }
        public override string ToString()
        {
            return Functie;
        }

        
        public string FunctieDisplayName
        {
            get
            {
                return Functie + Omschrijving;
            }
        }


        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case "Functie":
                        if (string.IsNullOrWhiteSpace(_functie))
                        {
                            error = "Functie is een verplicht veld.";
                            if (ErrorList.Contains("Functie") == false)
                            {
                                ErrorList.Add("Functie");
                            }
                        }
                        else if (_functie.Length > 50)
                        {
                            error = "Your text is to long!!!";
                            if (ErrorList.Contains("Functie") == false)
                            {
                                ErrorList.Add("Functie");
                            }
                        }
                        else
                        {
                            if (ErrorList.Contains("Functie"))
                            {
                                ErrorList.Remove("Functie");
                            }
                        }
                        return error;
                    case "Omschrijving":
                        if (string.IsNullOrWhiteSpace(_omschrijving))
                        {
                            error = "Omschrijving is een verplicht veld.";
                            if (ErrorList.Contains("Omschrijving") == false)
                            {
                                ErrorList.Add("Omschrijving");
                            }
                        }
                        else if (_omschrijving.Length > 500)
                        {
                            error = "Your text is to long!!!";
                            if (ErrorList.Contains("Omschrijving") == false)
                            {
                                ErrorList.Add("Omschrijving");
                            }
                        }
                        else
                        {
                            if
                            (ErrorList.Contains("Omschrijving"))
                            {
                                ErrorList.Remove("Omschrijving");
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
