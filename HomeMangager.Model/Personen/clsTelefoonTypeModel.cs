using HomeManager.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HomeManager.Model.Personen
{
    public class clsTelefoonTypeModel : clsCommonModelPropertiesBase, IDataErrorInfo
    {
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
            return TelefoonType;
        }


        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case "TelefoonType":
                        if (string.IsNullOrWhiteSpace(_telefoonType))
                        {
                            error = "TelefoonType is een verplicht veld.";
                            if (ErrorList.Contains("TelefoonType") == false)
                            {
                                ErrorList.Add("TelefoonType");
                            }
                        }
                        else if (_telefoonType.Length > 50)
                        {
                            error = "Your text is to long!!!";
                            if (ErrorList.Contains("TelefoonType") == false)
                            {
                                ErrorList.Add("TelefoonType");
                            }
                        }
                        else
                        {
                            if (ErrorList.Contains("TelefoonType"))
                            {
                                ErrorList.Remove("TelefoonType");
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
