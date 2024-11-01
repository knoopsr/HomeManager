using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Model.Budget
{
    public class clsBudgetCategorie
    {
        private int _BudgetCategorieID;
        public int BudgetCategorieID 
        { 
            get 
            {  
                return _BudgetCategorieID; 
            } 
            set
            {
                _BudgetCategorieID = value;
               
            }
        }

        private string _BudgetCategorie;
        public string BudgetCategorie
        {
            get
            {
                return _BudgetCategorie;
            }
            set
            {
                if (_BudgetCategorie != value)
                {
                    if (_BudgetCategorie != null)
                    {
                        //IsDirty = true;
                    }
                    _BudgetCategorie = value;
                }
            }

        }
    }
}
