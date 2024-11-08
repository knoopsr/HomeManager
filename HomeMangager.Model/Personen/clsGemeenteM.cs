﻿using HomeManager.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Model.Personen
{
    public class clsGemeenteM : clsCommonModelPropertiesBase, IDataErrorInfo
    {
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

        private int _provincieID;
        public int ProvincieID
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

        public override string ToString()
        {
            return Gemeente + " (" + PostCode + ")";
        }


        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case nameof(Gemeente):
                        if (string.IsNullOrWhiteSpace(Gemeente))
                        {
                            error = "Gemeente is een verplicht veld.";
                            if (ErrorList.Contains("Gemeente") == false)
                            {
                                ErrorList.Add("Gemeente");
                            }
                        }
                        else if (Gemeente.Length > 50)
                        {
                            error = "Your text is to long!!!";
                            if (ErrorList.Contains("Gemeente") == false)
                            {
                                ErrorList.Add("Gemeente");
                            }
                        }
                        else
                        {
                            if (ErrorList.Contains("Gemeente"))
                            {
                                ErrorList.Remove("Gemeente");
                            }
                        }
                        return error;

                    case nameof(PostCode):
                        if (string.IsNullOrWhiteSpace(PostCode))
                        {
                            error = "PostCode is een verplicht veld.";
                            if (ErrorList.Contains("PostCode") == false)
                            {
                                ErrorList.Add("PostCode");
                            }
                        }
                        else if (PostCode.Length > 12)
                        {
                            error = "Your text is to long!!!";
                            if (ErrorList.Contains("PostCode") == false)
                            {
                                ErrorList.Add("PostCode");
                            }
                        }
                        else
                        {
                            if (ErrorList.Contains("PostalCode"))
                            {
                                ErrorList.Remove("PostalCode");
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

