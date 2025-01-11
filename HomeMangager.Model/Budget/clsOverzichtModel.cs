using HomeManager.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Model.Budget
{
    public class clsOverzichtModel : clsCommonModelPropertiesBase
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

        private byte[] _bijlage;

        public byte[] Bijlage
        {
            get
            {
                return _bijlage;
            }
            set
            {
                _bijlage = value;
                OnPropertyChanged();
            }

        }

       

    }

 }
