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
            {  _domicilieringID = value; 
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

        private int _budgetCategorieID;
        public int BudgetCategorieID
        {
            get
            {
                return _budgetCategorieID;
            }
            set
            {
                _budgetCategorieID = value;
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

                    case nameof(FrequentieID):
                        if (_frequentieID <= 1)
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

                    case nameof(BegunstigdeID):
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

                    case nameof(BudgetCategorieID):
                        if (_budgetCategorieID <= 0)
                        {
                            error = "Budget Categorie is een verplicht veld.";
                            if (ErrorList.Contains("BudgetCategorie") == false)
                            {
                                ErrorList.Add("BudgetCategorie");
                            }
                        }

                        else
                        {
                            if (ErrorList.Contains("BudgetCategorie"))
                            {
                                ErrorList.Remove("BudgetCategorie");
                            }
                        }
                        return error;

                    case nameof(VanDatum):
                        if (_vanDatum == DateOnly.MinValue)
                        {
                            error = "VanDatum is een verplicht veld.";
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

                    case nameof(TotDatum):
                        if (_totDatum == DateOnly.MinValue)
                        {
                            error = "TotDatum is een verplicht veld.";
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

                    default:
                        error = null;
                        return error;

                }
            }
        }



    }
}
