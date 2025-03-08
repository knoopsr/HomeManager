using HomeManager.Common;
using HomeManager.Model.Budget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.DataService.Budget
{
    public interface ITransactieDataService : IDataService<clsTransactieModel>    
    {
        clsTransactieModel Insert2(clsTransactieModel entity);

    }
}
