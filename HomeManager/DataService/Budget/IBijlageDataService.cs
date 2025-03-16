using HomeManager.Common;
using HomeManager.Model.Budget;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.DataService.Budget
{
    public interface IBijlageDataService : IDataService<clsBijlageModel>    
    {
        ObservableCollection<clsBijlageModel> GetAll(int BudgetTransactionID);

    }
}
