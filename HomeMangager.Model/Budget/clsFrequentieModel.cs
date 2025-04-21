using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Common;

namespace HomeManager.Model.Budget
{
    public class clsFrequentieModel : clsCommonModelPropertiesBase , IDataErrorInfo
    {
        private int _frequentieID;
        public int FrequentieID 
        { 
            get 
            {  
                return _frequentieID; 
            } 
            set
            {
                _frequentieID = value;
                OnPropertyChanged();
               
            }
        }

        private string _frequentie;
        public string Frequentie
        {
            get
            {
                return _frequentie;
            }
            set
            {
                if (_frequentie != value)
                {
                    if (_frequentie != null)
                    {
                        IsDirty = true;
                    }
                    _frequentie = value;
                    OnPropertyChanged();
                }
            }
        }

        private int? _aantalDagen;
        public int? AantalDagen
        {
            get
            {
                return _aantalDagen;
            }
            set
            {
                if ( _aantalDagen != value)
                {
                    if (_aantalDagen != value)
                    {
                        //IsDirty = true;
                    }
                    
                }
                _aantalDagen = value;
                OnPropertyChanged();
            }
        }

        public override string ToString()
        {
            return Frequentie;
        }

        public string this[string columnName]
        {
            get


            {
                string error = string.Empty;
                switch (columnName)
                {

                    case "Frequentie":
                        if (string.IsNullOrWhiteSpace(_frequentie))
                        {
                            error = "Frequentie is verplicht";
                            if (ErrorList.Contains("Frequentie") == false)
                            {
                                ErrorList.Add(nameof(Frequentie));
                            }
                        }
                        else
                        {
                            if (ErrorList.Contains("Frequentie"))
                            {
                                ErrorList.Remove("Frequentie");
                            }
                        }
                        return error;

                    case "AantalDagen":

                        if (!_aantalDagen.HasValue || _aantalDagen <= 0)
                        {
                            error = "Bedrag kan niet leeg of 0 zijn";
                            if (ErrorList.Contains("AantalDagen") == false)
                            {
                                ErrorList.Add("AantalDagen");
                            }
                        }
                        else
                        {
                            if (ErrorList.Contains("AantalDagen"))
                            {
                                ErrorList.Remove("AantalDagen");
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
