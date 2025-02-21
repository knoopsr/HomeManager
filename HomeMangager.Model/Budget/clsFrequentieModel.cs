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

        private int _aantalDagen;
        public int  AantalDagen
        {
            get
            {
                return _aantalDagen;
            }
            set
            {
                if ( _aantalDagen != value)
                {
                    if (_aantalDagen != null)
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




                    case nameof(Frequentie):
                        if (string.IsNullOrWhiteSpace(_frequentie))
                        {
                            error = "Budget Frequentie is een verplicht veld.";
                            if (ErrorList.Contains(nameof(Frequentie)) == false)
                            {
                                ErrorList.Add(nameof(Frequentie));
                            }
                        }




                        else if (_frequentie.Length > 50)
                        {
                            error = "De voorgestelde categorie is te lang!!!";
                            if (ErrorList.Contains(nameof(Frequentie)) == false)
                            {
                                ErrorList.Add(nameof(Frequentie));

                            }
                        }

                        else
                        {
                            if (ErrorList.Contains(nameof(Frequentie)))
                            {
                                ErrorList.Remove(nameof(Frequentie));
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
