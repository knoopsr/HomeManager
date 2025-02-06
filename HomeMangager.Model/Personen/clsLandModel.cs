using HomeManager.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Model.Personen
{
    public class clsLandModel : clsCommonModelPropertiesBase, IDataErrorInfo
    {
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

        private string _land;
        public string Land
        {
            get
            {
                return _land;
            }
            set
            {
                if (_land != value)
                {
                    if (_land != null)
                    {
                        IsDirty = true;
                    }
                }
                _land = value;
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

        private byte[] _vlag;

        public byte[] Vlag
        {
            get
            {
                return _vlag;
            }
            set
            {

                _vlag = value;
                OnPropertyChanged();
            }
        }

        public override string ToString()
        {
            return Land + ", " + LandCode;
        }

        public string LandDisplayName
        {
            get
            {
                return $"{Land} ({LandCode})";
            }
        }
        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case nameof(Land):
                        if (string.IsNullOrWhiteSpace(Land))
                        {
                            error = "Land is een verplicht veld.";
                            if (ErrorList.Contains("Land") == false)
                            {
                                ErrorList.Add("Land");
                            }
                        }
                        else if (Land.Length > 50)
                        {
                            error = "Your text is to long!!!";
                            if (ErrorList.Contains("Land") == false)
                            {
                                ErrorList.Add("Land");
                            }
                        }
                        else
                        {
                            if (ErrorList.Contains("Land"))
                            {
                                ErrorList.Remove("Land");
                            }
                        }
                        return error;

                    case nameof(LandCode):
                        if (string.IsNullOrWhiteSpace(LandCode))
                        {
                            error = "LandCode is een verplicht veld.";
                            if (ErrorList.Contains("LandCode") == false)
                            {
                                ErrorList.Add("LandCode");
                            }
                        }
                        else if (LandCode.Length > 5)
                        {
                            error = "LandCode text is to long!!!";
                            if (ErrorList.Contains("LandCode") == false)
                            {
                                ErrorList.Add("LandCode");
                            }
                        }
                        else
                        {
                            if (ErrorList.Contains("LandCode"))
                            {
                                ErrorList.Remove("LandCode");
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
