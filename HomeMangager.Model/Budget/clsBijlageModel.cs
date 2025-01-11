using HomeManager.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Model.Budget
{
    public class clsBijlageModel : clsCommonModelPropertiesBase /*IDataErrorInfo*/
    {
        private bool _isNew;

        public bool IsNew
        {
            get
            {
                return _isNew;
            }

            set
            {
                _isNew = value; 
                OnPropertyChanged();
            }
        }

        

        private int _budgetBijlageID;

        public int BudgetBijlageID
        {
            get
            {
                return _budgetBijlageID;
            }
            set
            {
                _budgetBijlageID = value;
                OnPropertyChanged();
            }
        }
        
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

        

        private string _bijlageNaam;

        public string BijlageNaam
        {

            get
            {
                return _bijlageNaam;
            }

            set
            {
                
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
                
                _bijlage = value;
                OnPropertyChanged();
            }

        }

        //public string this[string columnName]
        //{
        //    get
        //    {
        //        string error = string.Empty;
        //        switch (columnName)
        //        {
                    

        //            default:
        //                error = null;
        //                return error;

        //        }
        //    }
        //}

    }

 }
