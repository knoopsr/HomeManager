using HomeManager.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Model.Personen
{
    public class clsProvincieM : clsCommonModelPropertiesBase, IDataErrorInfo
    {
        private int _provincieID;
        public int ProvincieID
        {
            get
            {
                return _provincieID;
            }
            set
            {
                _provincieID = value;
                OnPropertyChanged();
            }
        }

        private string _provincie;
        public string Provincie
        {
            get
            {
                return _provincie;
            }
            set
            {
                if (_provincie != value)
                {
                    if (_provincie != null)
                    {
                        IsDirty = true;
                    }
                }
                _provincie = value;
                OnPropertyChanged();
            }
        }

        private int _landID;
        public int LandID
        {
            get
            {
                return _landID;
            }
            set
            {
                _landID = value;
                OnPropertyChanged();
            }
        }

        private string _landCode;
        public string LandCode
        {
            get
            {
                return _landCode;
            }
            set
            {
                if (_landCode != value)
                {
                    if (_landCode != null)
                    {
                        IsDirty = true;
                    }
                }
                _landCode = value;
                OnPropertyChanged();
            }
        }

        public override string ToString()
        {
            return Provincie +" (" + LandCode + ")";
        }



        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case nameof(Provincie):
                        if (string.IsNullOrWhiteSpace(Provincie))
                        {
                            error = "Provincie is een verplicht veld.";
                            if (ErrorList.Contains("Provincie") == false)
                            {
                                ErrorList.Add("Provincie");
                            }
                        }
                        else if (Provincie.Length > 100)
                        {
                            error = "Your text is to long!!!";
                            if (ErrorList.Contains("Provincie") == false)
                            {
                                ErrorList.Add("Provincie");
                            }
                        }
                        else
                        {
                            if (ErrorList.Contains("Provincie"))
                            {
                                ErrorList.Remove("Provincie");
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
