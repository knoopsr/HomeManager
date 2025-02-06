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
    public class clsLockedAccountDataService : ILockedAccountDataService
    {
        ILockedAccountRepository _repo = new clsLockedAccountRepository();

        public bool Delete(clsLockedAccountModel entity)
        {
            return _repo.Delete(entity);
        }

        public clsLockedAccountModel Find()
        {
            return _repo.Find();
        }

        public ObservableCollection<clsLockedAccountModel> GetAll()
        {
            return _repo.GetAll();
        }

        public clsLockedAccountModel GetById(int id)
        {
            return _repo.GetById(id);
        }

        public clsLockedAccountModel GetFirst()
        {
            return _repo.GetFirst();
        }

        public bool Insert(clsLockedAccountModel entity)
        {
            return _repo.Insert(entity);
        }

        public bool UnLockUsers(clsLockedAccountModel AccountsIds)
        {
            return _repo.UnLockUsers(AccountsIds);
        }

        public bool Update(clsLockedAccountModel entity)
        {
            return _repo.Update(entity);
        }
    }
}
