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
        private int _jaar;
        

        public int Jaar
        {
            get { return _jaar; }
            set
            {
                if (_jaar != value)
                {
                    _jaar = value;
                    OnPropertyChanged(nameof(Jaar));
                }
            }
        }

        private string _maand;
        
        public string Maand
        {
            get { return _maand; }
            set
            {
                if (_maand != value)
                {
                    _maand = value;
                    OnPropertyChanged(nameof(Maand));
                }
            }
        }
        private string _begunstigde;
        

        public string Begunstigde
        {
            get { return _begunstigde; }
            set
            {
                if (_begunstigde != value)
                {
                    _begunstigde = value;
                    OnPropertyChanged(nameof(Begunstigde));
                }
            }
        }
        private string _budgetCategorie;
        
        public string BudgetCategorie
        {
            get { return _budgetCategorie; }
            set
            {
                if (_budgetCategorie != value)
                {
                    _budgetCategorie = value;
                    OnPropertyChanged(nameof(BudgetCategorie));
                }
            }
        }

        private decimal _bedrag;

        public decimal Bedrag
        {
            get { return _bedrag; }
            set
            {
                if (_bedrag != value)
                {
                    _bedrag = value;
                    OnPropertyChanged(nameof(Bedrag));
                }
            }
        }

        private string _onderwerp;

        public string Onderwerp
        {
            get { return _onderwerp; }
            set
            {
                if (_onderwerp != value)
                {
                    _onderwerp = value;
                    OnPropertyChanged(nameof(Onderwerp));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

 
