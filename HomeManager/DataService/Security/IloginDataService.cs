using HomeManager.Common;
using HomeManager.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.DataService.Security
{
    public interface IloginDataService : IDataService<clsLoginModel>
    {
        clsLoginModel GetByLogin(string login, string wachtwoord);
        bool UpdatePassWord(clsLoginModel entity, string Pass);
    }
}
