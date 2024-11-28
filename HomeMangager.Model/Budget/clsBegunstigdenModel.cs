using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Common;

namespace HomeManager.Model.Budget
{
    public class clsBegunstigdenModel : clsCommonModelPropertiesBase , IDataErrorInfo
    {
        private int _begunstigdeID;
        public int BegunstigdeID 
        { 
            get 
            {  
                return _begunstigdeID; 
            } 
            set
            {
                _begunstigdeID = value;
                OnPropertyChanged();
               
            }
        }

        private string _begunstigde;
        public string Begunstigde
        {
            get
            {
                return _begunstigde;
            }
            set
            {
                if (_begunstigde != value)
                {
                    if (_begunstigde != null)
                    {
                        IsDirty = true;
                    }
                    _begunstigde = value;
                    OnPropertyChanged();
                }
            }

        }

        private string _opmerking;

        public string Opmerking
        {
            get
            {
                return _opmerking;
            }
            set
            {
                if (_opmerking != value)
                {
                    if (_opmerking != null)
                    {
                        IsDirty = true;
                    }
                    _opmerking = value;
                    OnPropertyChanged();
                }
            }
        }

        public override string ToString()
        {
            return Begunstigde;
        }

        public string this[string columnName]
        {
            get


            {
                string error = string.Empty;
                switch (columnName)
                {




                    case nameof(Begunstigde):
                        if (string.IsNullOrWhiteSpace(_begunstigde))
                        {
                            error = "Budget Frequentie is een verplicht veld.";
                            if (ErrorList.Contains(nameof(Begunstigde)) == false)
                            {
                                ErrorList.Add(nameof(Begunstigde));
                            }
                        }
                        else if (_begunstigde.Length > 50)
                        {
                            error = "De voorgestelde categorie is te lang!!!";
                            if (ErrorList.Contains(nameof(Begunstigde)) == false)
                            {
                                ErrorList.Add(nameof(Begunstigde));
                            }
                        }
                        else
                        {
                            if (ErrorList.Contains(nameof(Begunstigde)))
                            {
                                ErrorList.Remove(nameof(Begunstigde));
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
