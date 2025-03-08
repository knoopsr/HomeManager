using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using HomeManager.Common;
using System.Data.SqlTypes;

namespace HomeManager.Model.Budget
{
    public class clsDomicilieringModel : clsCommonModelPropertiesBase, IDataErrorInfo
    {

        
        private int _domicilieringID;

        public int DomicilieringID 
        { 
            get 
            {  
                return _domicilieringID; 
            } 
            set 
            {  
                _domicilieringID = value; 
                OnPropertyChanged(); 
            } 
        }

        private bool? _isUitgaven;

        public bool? IsUitgaven
        {
            get
            {
                return _isUitgaven;
            }
            set
            {
                if (_isUitgaven != null)
                {
                    if (_isUitgaven != value)
                    {
                        IsDirty = true;
                    }
                }

                _isUitgaven = value;
                OnPropertyChanged();
            }
        }

        private decimal? _bedrag;

        public decimal? Bedrag
        {
            get
            {
                return _bedrag.HasValue ? Math.Round(_bedrag.Value, 2) : (decimal?)null;
                
            }
            set
            {
                if(_bedrag != value)
                {
                    if(_bedrag != null)
                    {
                        IsDirty = true;
                    }
                }
                _bedrag = value;
                OnPropertyChanged();
            }
        }

        private DateOnly _vanDatum;

        public DateOnly VanDatum
        {
            get
            {
                return _vanDatum;
            }
            set
            {
                
                if (_vanDatum != DateOnly.MinValue)
                {
                    if (_vanDatum != value)
                    {
                        IsDirty = true;
                    }
                }
                _vanDatum = value;
                OnPropertyChanged();
            }
        }

        private DateOnly _totDatum;

        public DateOnly TotDatum
        {
            get
            {
                return _totDatum;
            }
            set
            {
                if (_totDatum != DateOnly.MinValue)
                {
                    if (_totDatum != value)
                    {
                        IsDirty = true;
                    }
                }
                _totDatum = value;
                OnPropertyChanged();
            }
        }

        private string _onderwerp;

        public string Onderwerp
        {
            get
            {
                return _onderwerp;
            }
            set
            {
                if (_onderwerp != value)
                {
                    if (_onderwerp != null)
                    {
                        IsDirty = true;
                    }
                }
                _onderwerp = value;
                OnPropertyChanged();
            }
        }

        private int _frequentieID;
        public int FrequentieID
        {
            get
            {
                return _frequentieID;
            }
            set
            {
                if (_frequentieID != value)
                {
                    if (_frequentieID != 0)
                    {
                        IsDirty = true;
                    }

                }
                _frequentieID = value;
                OnPropertyChanged();
            }
        }

        private int _begunstigdeID;
        public int BegunstigdeID
        {
            get
            {
                return _begunstigdeID;
            }
            set
            {
                if (_begunstigdeID != value)
                {
                    if (_begunstigdeID != 0)
                    {
                        IsDirty = true;
                    }

                }
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

                }
                _begunstigde = value;
                OnPropertyChanged();
            }

        }

        private int _budgetCategorieID;
        public int BudgetCategorieID
        {
            get
            {
                return _budgetCategorieID;
            }
            set
            {
                if (_budgetCategorieID != value)
                {
                    if (_budgetCategorieID != 0)
                    {
                        IsDirty = true;
                    }

                }
                _budgetCategorieID = value;
                OnPropertyChanged();
            }
        }

        private string _budgetCategorie;

        public string BudgetCategorie
        {

            get 
            {
                return _budgetCategorie;
            }

            set
            {
                if ( _budgetCategorie != value)
                {
                    if(_budgetCategorie != null)
                    {
                        IsDirty = true;
                    }
                }

                _budgetCategorie = value;
                OnPropertyChanged();
            }

           
        }

        

        public string this[string columnName]
        {
            get


            {
                string error = string.Empty;
                switch (columnName)
                {
                    case "Bedrag":
                    if (!_bedrag.HasValue || _bedrag <= 0)
                        {
                            error = "Bedrag kan niet leeg of 0 zijn";
                            if (ErrorList.Contains("Bedrag") == false)
                            {
                                ErrorList.Add("Bedrag");
                            }
                        }
                        else
                        {
                            if (ErrorList.Contains("Bedrag"))
                            {
                                ErrorList.Remove("Bedrag");
                            }
                        }
                        return error;

                    case "VanDatum":
                    if (_vanDatum == DateOnly.MinValue)
                        {
                            error = "Van Datum is verplicht om in te vullen";
                            if (ErrorList.Contains("VanDatum") == false)
                            {
                                ErrorList.Add("VanDatum");
                            }
                        }
                        else
                        {
                            if (ErrorList.Contains("VanDatum"))
                            {
                                ErrorList.Remove("VanDatum");
                            }
                        }
                    return error;
                    
                    case "TotDatum":
                    if (_totDatum == DateOnly.MinValue)
                        {
                            error = "Tot Datum is verplicht om in te vullen";
                            if (ErrorList.Contains("TotDatum") == false)
                            {
                                ErrorList.Add("TotDatum");
                            }
                        }
                        else
                        {
                            if (ErrorList.Contains("TotDatum"))
                            {
                                ErrorList.Remove("TotDatum");
                            }
                        }
                    return error;

                    case "Onderwerp":
                        if (_onderwerp.Length > 100)
                        {
                         
                            error = "Your text is to long!!!";
                            if (ErrorList.Contains("Onderwerp") == false)
                            {
                                ErrorList.Add("Onderwerp");
                            }
                        }
                        else
                        {
                            if (ErrorList.Contains("Onderwerp"))
                            {
                                ErrorList.Remove("Onderwerp");
                            }
                        }
                        return error;

                    case "FrequentieID":
                        if (_frequentieID <= 0)
                        {
                            error = "Frequentie is een verplicht veld.";
                            if (ErrorList.Contains("Frequentie") == false)

                            {
                                ErrorList.Add("Frequentie");
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

                    case "BegunstigdeID":
                        if (_begunstigdeID <= 0)
                        {
                            error = "Begunstigde is een verplicht veld.";
                            if (ErrorList.Contains("Begunstigde") == false)

                            {
                                ErrorList.Add("Begunstigde");
                            }
                        }
                        else
                        {
                            if (ErrorList.Contains("Begunstigde"))
                            {
                                ErrorList.Remove("Begunstigde");
                            }
                        }
                        return error;

                    case "BudgetCategorieID":
                        if (_budgetCategorieID <= 0)
                        {
                            error = "Begunstigde is een verplicht veld.";
                            if (ErrorList.Contains("BudgetCategorieID") == false)

                            {
                                ErrorList.Add("BudgetCategorieID");
                            }
                        }
                        else
                        {
                            if (ErrorList.Contains("BudgetCategorieID"))
                            {
                                ErrorList.Remove("BudgetCategorieID");
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
