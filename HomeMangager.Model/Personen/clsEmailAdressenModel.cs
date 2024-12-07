using HomeManager.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Model.Personen
{
    public class clsEmailAdressenModel : clsCommonModelPropertiesBase, IDataErrorInfo
    {
        private int _emailAdresID;
        public int EmailAdresID
        {
            get
            {
                return _emailAdresID;
            }
            set
            {
                _emailAdresID = value;
                OnPropertyChanged();
            }
        }

        private string _emailadres;

        public string Emailadres
        {
            get
            {
                return _emailadres;
            }
            set
            {
                if (_emailadres != value)
                {
                    if (_emailadres != null)
                    {
                        IsDirty = true;
                    }
                }
                _emailadres = value;
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
        public override string ToString()
        {
            return Emailadres;
        }

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case "Emailadres":
                        if (string.IsNullOrWhiteSpace(_emailadres))
                        {
                            error = "Emailadres is een verplicht veld.";
                            if (ErrorList.Contains("Emailadres") == false)
                            {
                                ErrorList.Add("Emailadres");
                            }
                        }
                        else if (_emailadres.Length > 100)
                        {
                            error = "Your text is to long!!!";
                            if (ErrorList.Contains("Emailadres") == false)
                            {
                                ErrorList.Add("Emailadres");
                            }
                        }
                        else
                        {
                            if (ErrorList.Contains("Emailadres"))
                            {
                                ErrorList.Remove("Emailadres");
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


