using HomeManager.Common;
using HomeManager.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.DataService.Security
{
    public interface IRechtenDataService : IDataService<clsRechtenModel>
    {
        clsRechtenModel GetByCatogorieID(int id);
    }
}
