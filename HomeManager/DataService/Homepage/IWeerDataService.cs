using HomeManager.Common;
using HomeManager.Model.Homepage;
using HomeManager.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.DataService.Homepage
{
    public interface IWeerDataService : IDataService<clsWeerModel>
    {
        clsWeerModel GetByAccountId(int accountId);
    }
}
