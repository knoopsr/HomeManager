﻿using HomeManager.Common;
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
                if (_gemeenteID != value)
                {
                    if (_gemeenteID != 0)
                    {
                        IsDirty = true;
                    }

                }
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
                if (_functieID != value)
                {
                    if (_functieID != 0)
                    {
                        IsDirty = true;
                    }

                }
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

        private string _gemeente;

        public string Gemeente
        {
            get
            {
                return _gemeente;
            }
            set
            {
                if (_gemeente != value)
                {
                    if (_gemeente != null)
                    {
                        IsDirty = true;
                    }
                }
                _gemeente = value;
                OnPropertyChanged();
            }
        }

        private string _postCode;

        public string PostCode
        {
            get
            {
                return _postCode;
            }
            set
            {
                if (_postCode != value)
                {
                    if (_postCode != null)
                    {
                        IsDirty = true;
                    }
                }
                _postCode = value;
                OnPropertyChanged();
            }
        }


        public override string ToString()
        {
            return Straat + ", " + Nummer + " (" + Gemeente + "-" + PostCode + ")";
        }

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case nameof(Straat):
                        if (string.IsNullOrWhiteSpace(_straat))
                        {
                            error = "Straat is een verplicht veld.";
                            if (!ErrorList.Contains(nameof(Straat)))
                            {
                                ErrorList.Add(nameof(Straat));
                            }
                        }
                        else if (_straat.Length > 50)
                        {
                            error = "Straat mag niet langer zijn dan 50 tekens.";
                            if (!ErrorList.Contains(nameof(Straat)))
                            {
                                ErrorList.Add(nameof(Straat));
                            }
                        }
                        else
                        {
                            ErrorList.Remove(nameof(Straat));
                        }
                        return error;

                    case nameof(Nummer):
                        if (string.IsNullOrWhiteSpace(_nummer))
                        {
                            error = "Nummer is een verplicht veld.";
                            if (!ErrorList.Contains(nameof(Nummer)))
                            {
                                ErrorList.Add(nameof(Nummer));
                            }
                        }
                        else if (_nummer.Length > 5)
                        {
                            error = "Nummer mag niet langer zijn dan 5 tekens.";
                            if (!ErrorList.Contains(nameof(Nummer)))
                            {
                                ErrorList.Add(nameof(Nummer));
                            }
                        }
                        else
                        {
                            ErrorList.Remove(nameof(Nummer));
                        }
                        return error;
                    case nameof(GemeenteID):
                        if (GemeenteID == 0)
                        {
                            error = "Gemeente is een verplicht veld.";
                            if (ErrorList.Contains(nameof(GemeenteID)) == false)
                            {
                                ErrorList.Add(nameof(GemeenteID));
                            }
                        }

                        else
                        {
                            if (ErrorList.Contains(nameof(GemeenteID)))
                            {
                                ErrorList.Remove(nameof(GemeenteID));
                            }
                        }
                        return error;

                    default:
                        return null;
                }
            }
        }
    }
}