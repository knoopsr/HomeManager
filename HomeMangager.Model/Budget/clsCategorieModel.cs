using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Common;

namespace HomeManager.Model.Budget
{
    public class clsCategorieModel : clsCommonModelPropertiesBase , IDataErrorInfo
    {
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
                    _budgetCategorie = value;
                    OnPropertyChanged();
                }
            }

        }

        public override string ToString()
        {
            return BudgetCategorie;
        }

        public string this[string columnName]
        {
            get


            {
                string error = string.Empty;
                switch (columnName)
                {




                    case nameof(BudgetCategorie):
                        if (string.IsNullOrWhiteSpace(_budgetCategorie))
                        {
                            error = "Budget Categorie is een verplicht veld.";
                            if (ErrorList.Contains(nameof(BudgetCategorie)) == false)
                            {
                                ErrorList.Add(nameof(BudgetCategorie));
                            }
                        }




                        else if (_budgetCategorie.Length > 50)
                        {
                            error = "De voorgestelde categorie is te lang!!!";
                            if (ErrorList.Contains(nameof(BudgetCategorie)) == false)
                            {
                                ErrorList.Add(nameof(BudgetCategorie));

                            }
                        }

                        else
                        {
                            if (ErrorList.Contains(nameof(BudgetCategorie)))
                            {
                                ErrorList.Remove(nameof(BudgetCategorie));
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
