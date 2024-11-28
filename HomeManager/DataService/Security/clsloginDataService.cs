using HomeManager.DAL.Security;
using HomeManager.Model.Security;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.DataService.Security
{
    public class clsloginDataService : IloginDataService
    {
        ILoginRepository _repo = new clsLoginRepository();
        public clsloginDataService() { }
        public bool Delete(clsLoginModel entity)
        {
            throw new NotImplementedException();
        }

        public clsLoginModel Find()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<clsLoginModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public clsLoginModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public clsLoginModel GetByLogin(string login, string wachtwoord)
        {
            return _repo.GetByLogin(login, wachtwoord);
        }

        public clsLoginModel GetFirst()
        {
            throw new NotImplementedException();
        }

        public bool Insert(clsLoginModel entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(clsLoginModel entity)
        {
            throw new NotImplementedException();
        }

        public bool UpdatePassWord(clsLoginModel entity, string Pass)
        {
            return _repo.UpdatePassWord(entity, Pass);
        }
    }
}
