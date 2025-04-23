using HomeManager.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Model.Budget
{
    public class clsTransactieModel : clsCommonModelPropertiesBase, IDataErrorInfo
    {
        private int _budgetTransactionID;

        public int BudgetTransactionID
        {
            get
            {
                return _budgetTransactionID;
            }
            set
            {
                _budgetTransactionID = value;
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
                if (_bedrag != value)
                {
                    if (_bedrag != null)
                    {
                        IsDirty = true;
                    }
                }
                _bedrag = value;
                OnPropertyChanged();
            }
        }

        private DateOnly _datum;

        public DateOnly Datum
        {
            get
            {
                return _datum;
            }
            set
            {

                if (_datum != DateOnly.MinValue)
                {
                    if (_datum != value)
                    {
                        IsDirty = true;
                    }
                }
                _datum = value;
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
                if (_budgetCategorie != value)
                {
                    if (_budgetCategorie != null)
                    {
                        IsDirty = true;
                    }
                }

                _budgetCategorie = value;
                OnPropertyChanged();
            }
        }

        private string _bijlageNaam;

        public string BijlageNaam
        {

            get
            {
                return _bijlageNaam;
            }

            set
            {
                if (_bijlageNaam != value)
                {
                    if (_bijlageNaam != null)
                    {
                        IsDirty = true;
                    }
                }

                _bijlageNaam = value;
                OnPropertyChanged();
            }
        }

        private byte[] _bijlage;

        public byte[] Bijlage
        {
            get
            {
                return _bijlage;
            }
            set
            {
                if (_bijlage != value)
                {
                    if (_bijlage != null)
                    {
                        IsDirty = true;
                    }
                }
                _bijlage = value;
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

                    case "Datum":
                        if (_datum == DateOnly.MinValue)
                        {
                            error = "Van Datum is verplicht";
                            if (ErrorList.Contains("Datum") == false)
                            {
                                ErrorList.Add("Datum");
                            }
                        }
                        else
                        {
                            if (ErrorList.Contains("Datum"))
                            {
                                ErrorList.Remove("Datum");
                            }
                        }
                        return error;

                    

                    case "Onderwerp":
                        if (_onderwerp.Length > 100)
                        {

                            error = "Tekst is langer dan 100 karakters";
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

                    

                    case "BegunstigdeID":
                        if (_begunstigdeID <= 0)
                        {
                            error = "Begunstigde is verplicht";
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
                            error = "Categorie is verplicht";
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
