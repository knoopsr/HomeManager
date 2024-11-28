using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Common;
using HomeManager.Model.Security;

namespace HomeManager.DAL.Security
{
    public interface ILoginRepository : IRepository<clsLoginModel>
    {
       clsLoginModel GetByLogin(string login, string wachtwoord);
        bool UpdatePassWord(clsLoginModel entity, string Pass);
    }
}
