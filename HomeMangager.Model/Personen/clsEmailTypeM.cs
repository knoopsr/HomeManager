using HomeManager.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Model.Personen
{
    public class clsEmailTypeM : clsCommonModelPropertiesBase, IDataErrorInfo
    {
        private int _emailTypeID;
        public int EmailTypeID
        {
            get
            {
                return _emailTypeID;
            }
            set
            {
                _emailTypeID = value;
                OnPropertyChanged();
            }
        }

        private string _emailType;

        public string EmailType
        {
            get
            {
                return _emailType;
            }
            set
            {
                if (_emailType != value)
                {
                    if (_emailType != null)
                    {
                        IsDirty = true;
                    }
                }
                _emailType = value;
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
            return EmailType;
        }

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case "EmailType":
                        if (string.IsNullOrWhiteSpace(_emailType))
                        {
                            error = "EmailType is een verplicht veld.";
                            if (ErrorList.Contains("EmailType") == false)
                            {
                                ErrorList.Add("EmailType");
                            }
                        }
                        else if (_emailType.Length > 75)
                        {
                            error = "Your text is to long!!!";
                            if (ErrorList.Contains("EmailType") == false)
                            {
                                ErrorList.Add("EmailType");
                            }
                        }
                        else
                        {
                            if (ErrorList.Contains("EmailType"))
                            {
                                ErrorList.Remove("EmailType");
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
                        else if (_omschrijving.Length > 1000)
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


