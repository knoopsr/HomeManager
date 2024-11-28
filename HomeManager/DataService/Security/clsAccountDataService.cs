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
    public class clsAccountDataService : IAccountDataService
    {
        IAccountRepository _repo = new clsAccountRepository();

        public bool Delete(clsAccountModel entity)
        {
            return _repo.Delete(entity);
        }

        public clsAccountModel Find()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<clsAccountModel> GetAll()
        {
            return _repo.GetAll();
        }

        public clsAccountModel GetById(int id)
        {
            return _repo.GetById(id);
        }

        public clsAccountModel GetFirst()
        {
            return _repo.GetFirst();
        }

        public bool Insert(clsAccountModel entity)
        {
            return _repo.Insert(entity);
        }

        public bool Update(clsAccountModel entity)
        {
            return _repo.Update(entity);
        }
    }
}
